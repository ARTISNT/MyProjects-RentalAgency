namespace RentalAgency.CustomExceptions.NotFoundExceptions;

public class RentalOrderNotFoundException : Exception
{
    public RentalOrderNotFoundException(int rentalOrderId) : base($"Rental order with id {rentalOrderId} was not found"){ } 
}