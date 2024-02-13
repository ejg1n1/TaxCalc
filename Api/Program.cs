using System.Security.Claims;
using System.Text;
using Serilog;
using API.Middleware;
using API.SerilogEnrichers;
using Application.Configuration;
using Application.Exceptions;
using Application.Ioc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using HangfireBasicAuthenticationFilter;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Api.Extensions;
using System.ComponentModel.DataAnnotations;

const string DEVELOPMENT_CORS_POLICY = "DevelopmentCorsPolicy";

// Add services to the container.
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var athenaConnectionString = configuration.GetConnectionString("ApplicationDatabase");
builder.Services.AddEndpointsApiExplorer();
var requireAuthUserGloballyPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthUserGloballyPolicy));
})
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
    .AddProblemDetailsConventions();

//Add Project Layers
builder.Services.AddCorsDevelopmentServices(DEVELOPMENT_CORS_POLICY);
builder.Services.AddSwaggerServices();
builder.Services.AddFluentValidationServices();
builder.Services.AddApplication(configuration);
builder.Services.AddDbContextServices(configuration);

//Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.DefaultApiVersion = ApiVersion.Default;
});

//Authentication
var authenticationOptions = new AuthOptions();
configuration.GetSection(nameof(AuthOptions))
    .Bind(authenticationOptions);
builder.Services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Administrator"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Athena",
            ValidAudience = "Athena",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.Secret))
        };
    });

// Logging
builder.Services.AddHttpContextAccessor();
SerilogHostBuilderExtensions.UseSerilog(builder.Host);

//Hangfire
builder.Services.AddHangfireServer();
builder.Services.AddHangfire(hangfireConfig =>
{
    hangfireConfig
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(athenaConnectionString, new PostgreSqlStorageOptions()
        {
            QueuePollInterval = TimeSpan.FromSeconds(15.0),
            // Note: If job execution exceeds [InvisibilityTimeout],
            // method will be called again without a retry count.
            InvisibilityTimeout = TimeSpan.FromMinutes(60),
            DistributedLockTimeout = TimeSpan.FromMinutes(10.0),
            TransactionSynchronisationTimeout = TimeSpan.FromMilliseconds(500.0),
            JobExpirationCheckInterval = TimeSpan.FromHours(1.0),
            SchemaName = "hangfire",
            UseNativeDatabaseTransactions = true,
            PrepareSchemaIfNecessary = true,
            DeleteExpiredBatchSize = 1000
        })
        .UseSimpleAssemblyNameTypeSerializer()
        .UseSerilogLogProvider()
        .UseConsole();
});

//Exception handling 
builder.Services.AddProblemDetails(opts =>
{
    opts.IncludeExceptionDetails = (context, ex) => !environment.IsProduction();
    opts.RethrowAll();
    opts.MapToStatusCode<ValidationException>(422);
    opts.Map<BaseException>(e => new ExtendedProblemDetail(e.StatusCode)
    {
        Title = e.GetType().Name,
        Status = e.StatusCode,
        Detail = e.Message,
        Errors = e.Errors
    });
    opts.Map<Exception>(e => new ProblemDetails()
    {
        Title = e.GetType().Name,
        Detail = e.Message,
    });
});

Console.WriteLine($"Starting Migrations");
var app = builder.Build();

//Migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
Log.Warning("Migrations Completed");

//Middleware
app.Use((context, next) =>
{
    //Enables request body traversal (Logging)
    context.Request.EnableBuffering();
    return next();
});
app.UseMiddleware<CustomExceptionLoggerMiddleware>();

app.UseRouting();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseProblemDetails();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseMiddleware<GlobalOptionsMiddleware>();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
});


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter { User = "AthanAdmin", Pass = "E3noWPK5y8$Hz9%u"}
    }
});

//app.UseRecurringJobs();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

Log.Warning("The API is starting");

app.Run();