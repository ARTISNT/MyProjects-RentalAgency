using Microsoft.EntityFrameworkCore;
using RentalAgency.Infrastructure.DB.Context;
using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.Repositories;

public class RentalOrderRepository : IRentalOrderRepository
{
    private readonly RentalAgencyDbContext _rentalAgencyDbContext;

    public RentalOrderRepository(RentalAgencyDbContext rentalAgencyDbContext)
    {
        _rentalAgencyDbContext = rentalAgencyDbContext;
    }
    
    public async Task<IReadOnlyCollection<RentalOrder>> GetAllAsync()
    {
        return await _rentalAgencyDbContext.Orders.ToListAsync();
    }

    public async Task<RentalOrder?> GetByIdAsync(int id)
    {
        var rentalOrder = await _rentalAgencyDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        
        if (rentalOrder == null)
            throw new KeyNotFoundException("Rental order with id {id} not found");
        
        return rentalOrder;
    }

    public async Task<RentalOrder> CreateAsync(RentalOrder rentalOrder)
    {
        await _rentalAgencyDbContext.AddAsync(rentalOrder);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return rentalOrder;
    }

    public async Task<RentalOrder> UpdateAsync(int id, RentalOrder rentalOrder)
    {
        var  rentalOrderToUpdate = await _rentalAgencyDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        
        if (rentalOrderToUpdate == null)
            throw new KeyNotFoundException("Rental order with id {id} not found");
        
        _rentalAgencyDbContext.Entry(rentalOrderToUpdate).CurrentValues.SetValues(rentalOrder);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return rentalOrder;
    }

    public async Task<RentalOrder> DeleteAsync(int id)
    {
        var rentalOrderToDelete = await _rentalAgencyDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        
        if (rentalOrderToDelete == null)
            throw new KeyNotFoundException("Rental order with id {id} not found");
        
        _rentalAgencyDbContext.Remove(rentalOrderToDelete);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return rentalOrderToDelete;
    }
}