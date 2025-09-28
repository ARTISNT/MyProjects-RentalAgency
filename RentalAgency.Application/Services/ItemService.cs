using RentalAgency.CustomExceptions.NotFoundExceptions;
using RentalAgency.CustomExceptions.ValidtionExceptions;
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
        return await GetRequiredItemAsync(id);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        CheckItemForNull(item);
        return await _itemRepository.CreateAsync(item);
    }

    public async Task<Item> UpdateAsync(int id, Item item)
    {
        CheckItemForNull(item);
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
                          ?? throw new ItemNotFoundException(id);
        
        return updatedItem.Status;
    }
    
    private async Task<Item> GetRequiredItemAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);

        if (item == null)
            throw new ItemNotFoundException(id);
        
        return item;
    }

    private void CheckItemForNull(Item item)
    {
        if (item == null)
        {
            throw new EntityValidationException("Item cannot be null");
        }
    }
}