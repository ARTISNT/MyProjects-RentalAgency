namespace RentalAgency.CustomExceptions.ValidtionExceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException(string message) : base(message)
    {
    }
}