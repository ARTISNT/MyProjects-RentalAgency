using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.DB.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.Id);
        
        builder
            .HasOne(i => i.Owner)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.OwnerId)
            .IsRequired();

        builder
            .HasMany(i => i.RentalOrders)
            .WithOne(o => o.Item)
            .HasForeignKey(o => o.ItemId);
    }
}