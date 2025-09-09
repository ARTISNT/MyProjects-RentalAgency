namespace RentalAgency.Models;

public class RentalOrder
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public decimal TotalCost { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Active;

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();    
}

public enum OrderStatus
{
    Active,
    Completed,
    Overdue,
    Cancelled
}