using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.DB.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder
            .HasMany(u => u.RentalOrders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
        
        builder
            .HasMany(u => u.Items)
            .WithOne(i => i.Owner)
            .HasForeignKey(i => i.OwnerId);
    }
}