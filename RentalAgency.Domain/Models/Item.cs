using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace RentalAgency.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal PricePerDay { get; set; }
    
    [Column(TypeName = "item_status")]
    public ItemStatus Status { get; set; } = ItemStatus.Available;

    public string OwnerId { get; set; } =  null!;
    public User Owner { get; set; } = null!;

    public ICollection<RentalOrder> RentalOrders { get; set; } = new List<RentalOrder>();
}

[PgName("item_status")]
public enum ItemStatus
{
    Available,
    Rented,
    Maintenance
}