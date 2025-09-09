using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalAgency.Infrastructure.DB.Configurations;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.DB.Context;

public class RentalAgencyDbContext : IdentityDbContext<User>
{
    public RentalAgencyDbContext(DbContextOptions<RentalAgencyDbContext> options) : base(options)
    {
    }
    
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<RentalOrder> Orders { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<User> Users { get; set; } =  null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasPostgresEnum<ItemStatus>();
        builder.HasPostgresEnum<OrderStatus>();
        builder.HasPostgresEnum<PaymentStatus>();

        builder.ApplyConfiguration(new ItemConfiguration());
        builder.ApplyConfiguration(new RentalOrderConfiguration());
        builder.ApplyConfiguration(new PaymentConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        
        builder.Entity<User>(entity =>
        {
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_User_WalletBalance", "\"WalletBalance\" >= 0");
            });
        });
    }
}