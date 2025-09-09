using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace RentalAgency.Models;

public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public RentalOrder Order { get; set; } = null!;

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "payment_status")]
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
}

[PgName("payment_status")]
public enum PaymentStatus
{
    Pending,
    Paid,
    Failed
}