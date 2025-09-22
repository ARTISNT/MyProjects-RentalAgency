using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;
using UseCase.Abstractions.Interfaces;

namespace UseCase.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    
    public async Task<IReadOnlyCollection<Payment>> GetAllAsync()
    {
        return await _paymentRepository.GetAllAsync();
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await GetRequiredPayment(id);
    }

    public async Task<Payment> CreateAsync(Payment payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));
        
        return await _paymentRepository.CreateAsync(payment);
    }

    public async Task<Payment> UpdateAsync(int id, Payment payment)
    {
        if(payment == null) throw new ArgumentNullException(nameof(payment));
        
        if(payment.Id != id)
            throw new ArgumentException($"Id does not match expected value {payment.Id}");
        
        return await _paymentRepository.UpdateAsync(id, payment);
    }

    public async Task<Payment?> DeleteAsync(int id)
    {
        return await _paymentRepository.DeleteAsync(id);
    }

    public async Task<PaymentStatus> GetStatusAsync(int id)
    {
        var payment = await GetRequiredPayment(id);
        
        return payment.Status;
    }

    public async Task<PaymentStatus> UpdateStatusAsync(int id, PaymentStatus paymentStatus)
    {
        var payment = await GetRequiredPayment(id);
        
        payment.Status = paymentStatus;
        var updatedPayment = await UpdateAsync(id, payment) ?? throw new KeyNotFoundException("Payment not found");
        
        return updatedPayment.Status;
    }

    private async Task<Payment> GetRequiredPayment(int id)
    {
        var payment = await _paymentRepository.GetByIdAsync(id);
        
        if (payment == null)
            throw new KeyNotFoundException($"Payment with id {id} not found");
        
        return payment;
    }
}