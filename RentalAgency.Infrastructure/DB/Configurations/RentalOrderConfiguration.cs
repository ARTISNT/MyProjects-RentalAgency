using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.DB.Configurations;

public class RentalOrderConfiguration : IEntityTypeConfiguration<RentalOrder>
{
    public void Configure(EntityTypeBuilder<RentalOrder> builder)
    {
        builder.HasKey(o => o.Id);
        
        builder
            .HasOne(o => o.Item)
            .WithMany(i => i.RentalOrders)
            .HasForeignKey(o => o.ItemId);

        builder
            .HasMany(o => o.Payments)
            .WithOne(o => o.Order)
            .HasForeignKey(p => p.OrderId);
    }
}