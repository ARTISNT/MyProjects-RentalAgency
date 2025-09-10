using RentalAgency.Models;

namespace RentalAgency.Interfaces.Repositories;

public interface IPaymentRepository
{
    public Task<IReadOnlyCollection<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(int id);
    public Task<Payment> CreateAsync(Payment payment);
    public Task<Payment> UpdateAsync(Payment payment);
    public Task<bool> DeleteAsync(int id);
}