namespace RentalAgency.CustomExceptions.NotFoundExceptions;

public class PaymentNotFoundExceptions : Exception
{
    public PaymentNotFoundExceptions(int paymentId) : base($"Payment with id {paymentId} was not found") { }
}