using Microsoft.EntityFrameworkCore;
using RentalAgency.CustomExceptions.NotFoundExceptions;
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
        var item = EnsureFound(await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id), id);
        
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
        var itemToUpdate = EnsureFound(await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id), id);
        
        _rentalAgencyDbContext.Entry(itemToUpdate).CurrentValues.SetValues(item);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return itemToUpdate;
    }

    public async Task<Item> DeleteAsync(int id)
    {
        var itemToDelete = EnsureFound(await _rentalAgencyDbContext.Items.FirstOrDefaultAsync(i => i.Id == id), id);
        
        _rentalAgencyDbContext.Items.Remove(itemToDelete);
        await _rentalAgencyDbContext.SaveChangesAsync();
        return itemToDelete;
    }
    
    private static Item EnsureFound(Item? entity, int id)
    {
        if (entity is null)
            throw new ItemNotFoundException(id);

        return entity;
    }
}