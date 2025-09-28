namespace RentalAgency.CustomExceptions.NotFoundExceptions;

public class ItemNotFoundException  : Exception
{
   public ItemNotFoundException(int itemId) :  base($"Item with id {itemId} was not found"){ }
}