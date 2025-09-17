using RentalAgency.Models;

namespace RentalAgency.Interfaces.Repositories;

public interface IPaymentRepository
{
    public Task<IReadOnlyCollection<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(int id);
    public Task<Payment> CreateAsync(Payment payment);
    public Task<Payment> UpdateAsync(int id, Payment payment);
    public Task<Payment> DeleteAsync(int id);
}