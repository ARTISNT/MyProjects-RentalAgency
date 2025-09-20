using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;
using UseCase.Abstractions.Interfaces;

namespace UseCase.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }
    
    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        return await _itemRepository.GetAllAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _itemRepository.GetByIdAsync(id);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        return await _itemRepository.CreateAsync(item);
    }

    public async Task<Item> UpdateAsync(int id, Item item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        return await _itemRepository.UpdateAsync(id, item);
    }

    public async Task<Item?> DeleteAsync(int id)
    {
        return await _itemRepository.DeleteAsync(id);
    }

    public async Task<ItemStatus> GetStatusAsync(int id)
    {
        var item = await GetRequiredItemAsync(id); 
        
        return item.Status;
    }

    public async Task<ItemStatus> UpdateStatusAsync(int id, ItemStatus status)
    {
        var item = await GetRequiredItemAsync(id);
        
        item.Status = status;

        var updatedItem = await _itemRepository.UpdateAsync(id, item) 
                          ?? throw new KeyNotFoundException("Item not found");
        
        return updatedItem.Status;
    }
    
    private async Task<Item> GetRequiredItemAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        
        if (item == null)
            throw new KeyNotFoundException("Item not found");
        
        return item;
    }
}