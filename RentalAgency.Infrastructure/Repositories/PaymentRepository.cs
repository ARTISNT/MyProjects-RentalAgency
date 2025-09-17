using Microsoft.EntityFrameworkCore;
using RentalAgency.Infrastructure.DB.Context;
using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;

namespace RentalAgency.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    protected readonly RentalAgencyDbContext _rentalAgencyDbContext;

    public PaymentRepository(RentalAgencyDbContext rentalAgencyDbContext)
    {
        _rentalAgencyDbContext = rentalAgencyDbContext;
    }
    
    public async Task<IReadOnlyCollection<Payment>> GetAllAsync()
    {
        return await _rentalAgencyDbContext.Payments.ToListAsync();
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        var payment = await _rentalAgencyDbContext.Payments.FirstOrDefaultAsync(p => p.Id == id);
        
        if (payment == null)
            throw new KeyNotFoundException($"Payment with id {id} not found");
        
        return payment;
    }

    public async Task<Payment> CreateAsync(Payment payment)
    {
        await _rentalAgencyDbContext.Payments.AddAsync(payment);
        await _rentalAgencyDbContext.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> UpdateAsync(int id, Payment payment)
    {
        var  paymentToUpdate = await _rentalAgencyDbContext.Payments.FirstOrDefaultAsync(p => p.Id == id);
        
        if (paymentToUpdate == null)
            throw new KeyNotFoundException($"Payment with id {id} not found");
        
        _rentalAgencyDbContext.Entry(paymentToUpdate).CurrentValues.SetValues(payment);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return payment;
    }

    public async Task<Payment> DeleteAsync(int id)
    {
        var paymentToDelete = _rentalAgencyDbContext.Payments.FirstOrDefault(p => p.Id == id);
        
        if (paymentToDelete == null)
            throw new KeyNotFoundException($"Payment with id {id} not found");
        
        _rentalAgencyDbContext.Payments.Remove(paymentToDelete);
        await _rentalAgencyDbContext.SaveChangesAsync();
        
        return paymentToDelete;
    }
}