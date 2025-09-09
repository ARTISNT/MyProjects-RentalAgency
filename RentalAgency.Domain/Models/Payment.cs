namespace RentalAgency.Models;

public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public RentalOrder Order { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed
}