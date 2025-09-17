using RentalAgency.Models;

namespace RentalAgency.Interfaces.Repositories;

public interface IItemRepository
{
    public Task<IReadOnlyCollection<Item>> GetAllAsync();
    public Task<Item?> GetByIdAsync(int id);
    public Task<Item> CreateAsync(Item item);
    public Task<Item> UpdateAsync(int id, Item item);
    public Task<Item> DeleteAsync(int id);
}