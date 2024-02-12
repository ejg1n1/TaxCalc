using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using Serilog.Core;
using Serilog.Events;

namespace API.SerilogEnrichers;

public class HttpContextEnricher : ILogEventEnricher
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextEnricher() : this(new HttpContextAccessor())
    {
    }

    public HttpContextEnricher(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        Guid? userId = null;
        if (Guid.TryParse(_contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out var uId))
        {
            userId = uId;
        }


        string requestBody = "";

        // Only process requests below 1Mb
        if (_contextAccessor.HttpContext?.Request.ContentLength < 1000000)
        {
            if ((bool)_contextAccessor.HttpContext?.Request?.Body?.CanSeek!)
            {
                try
                {
                    _contextAccessor.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
                    if (_contextAccessor.HttpContext?.Request?.Body != null)
                    {
                        using StreamReader reader
                            = new StreamReader(_contextAccessor.HttpContext?.Request?.Body!,
                                Encoding.UTF8, true, 1024, true);
                        requestBody = reader.ReadToEnd();
                    }

                    _contextAccessor.HttpContext?.Request?.Body?.Seek(0, SeekOrigin.Begin);

                    //Password sanitizer
                    var routeData = _contextAccessor?.HttpContext?.GetRouteData();
                    var actionName = routeData?.Values["action"]?.ToString();
                    if (actionName == "Authenticate")
                    {
                        var bodyJson = JObject.Parse(requestBody);
                        if (bodyJson["password"] != null)
                        {
                            bodyJson["password"] = "*****";
                        }
                        requestBody = bodyJson.ToString();
                    }

                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("UserId", userId));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("RequestJson", requestBody));
        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("TraceId", Activity.Current?.Id ?? _contextAccessor?.HttpContext?.TraceIdentifier));
    }
}