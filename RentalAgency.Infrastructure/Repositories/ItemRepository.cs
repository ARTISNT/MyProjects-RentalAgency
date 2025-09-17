using Microsoft.EntityFrameworkCore;
using RentalAgency.Infrastructure.DB.Context;
using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly RentalAgencyDbContext _rentalAgencyDbContext;

    public ItemRepository(RentalAgencyDbContext rentalAgencyDbContext)
    {
        _rentalAgencyDbContext = rentalAgencyDbContext;
    }
    
    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        return await _rentalAgencyDbContext.Items.ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var item = await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        
        if (item == null)
            throw new KeyNotFoundException($"Item with id {id} not found");
        
        return item;
    }

    public async Task<Item> CreateAsync(Item item)
    {
        await _rentalAgencyDbContext.Items.AddAsync(item);
        await _rentalAgencyDbContext.SaveChangesAsync();
        return item;
    }

    public async Task<Item> UpdateAsync(int id, Item item)
    {
        var itemToUpdate = await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        
        if (itemToUpdate == null)
            throw new KeyNotFoundException($"Item with id {id} not found");
        
        _rentalAgencyDbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return itemToUpdate;
    }

    public async Task<Item> DeleteAsync(int id)
    {
        var itemToDelete = await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (itemToDelete == null)
            throw new KeyNotFoundException("Item with id {id} not found");
        
        _rentalAgencyDbContext.Items.Remove(itemToDelete);
        await _rentalAgencyDbContext.SaveChangesAsync();
        return itemToDelete;
    }
}