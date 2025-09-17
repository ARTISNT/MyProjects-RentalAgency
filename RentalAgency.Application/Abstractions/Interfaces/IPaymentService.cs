using RentalAgency.Models;

namespace UseCase.Abstractions.Interfaces;

public interface IPaymentService
{
    public Task<IEnumerable<Payment>> GetAllAsync();
    public Task<Payment?> GetByIdAsync(int id);
    public Task<Payment> CreateAsync(Payment payment);
    public Task<Payment> UpdateAsync(int id, Payment payment);
    public Task<Payment?> DeleteAsync(int id);
    public Task<PaymentStatus> GetStatusAsync(int id);
}