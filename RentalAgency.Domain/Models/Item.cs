namespace RentalAgency.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal PricePerDay { get; set; }
    public ItemStatus Status { get; set; } = ItemStatus.Available;

    public string OwnerId { get; set; } =  null!;
    public User Owner { get; set; } = null!;

    public ICollection<RentalOrder> RentalOrders { get; set; } = new List<RentalOrder>();
}

public enum ItemStatus
{
    Available,
    Rented,
    Maintenance
}