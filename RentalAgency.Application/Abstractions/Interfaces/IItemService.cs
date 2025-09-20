using RentalAgency.Models;

namespace UseCase.Abstractions.Interfaces;

public interface IItemService
{
    public Task<IReadOnlyCollection<Item>> GetAllAsync();
    public Task<Item?> GetByIdAsync(int id);
    public Task<Item> CreateAsync(Item item);
    public Task<Item> UpdateAsync(int id, Item item);
    public Task<Item?> DeleteAsync(int id);
    public Task<ItemStatus> GetStatusAsync(int id);
    public Task<ItemStatus> UpdateStatusAsync(int id, ItemStatus status);
}