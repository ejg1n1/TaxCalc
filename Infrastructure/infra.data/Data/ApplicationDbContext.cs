using Application.Models.REST.Request;
using Athena.Core.Entities;
using Core.Entities;
using Domain.Entities.RequestModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser,
    ApplicationRole,
    Guid,
    ApplicationUserClaim,
    ApplicationUserRole,
    ApplicationUserLogin, 
    ApplicationRoleClaim,
    ApplicationUserToken>
{
    public DbSet<BookingDetails> BookingDetails { get; set; } = default!;

    public DbSet<BookingSchedule> BookingSchedules { get; set; } = default!;
    public DbSet<BookingStatus> BookingStatuses { get; set; } = default!;
    public DbSet<BookingType> BookingTypes { get; set; } = default!;
    public DbSet<History> History { get; set; } = default!;
    public DbSet<Booking> Booking { get; set; } = default!;


    public DbSet<UserDetails> UserDetails { get; set; } = default!;

    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<AddressStatus> AddressStatuses => Set<AddressStatus>();
    


    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
}
