using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace infra.data.Data;

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
    public DbSet<UserDetails> UserDetails { get; set; } = default!;

    public DbSet<Address> Addresses { get; set; } = default!;
    public DbSet<AddressStatus> AddressStatuses => Set<AddressStatus>();
    


    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }
}
