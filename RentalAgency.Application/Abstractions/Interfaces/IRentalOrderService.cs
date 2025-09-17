using RentalAgency.Models;

namespace UseCase.Abstractions.Interfaces;

public interface IRentalOrderService
{
    public Task<IEnumerable<RentalOrder>> GetAllAsync();
    public Task<RentalOrder?> GetByIdAsync(int id);
    public Task<RentalOrder> CreateAsync(RentalOrder rentalOrder);
    public Task<RentalOrder> UpdateAsync(int id, RentalOrder rentalOrder);
    public Task<RentalOrder?> DeleteAsync(int id);
    public Task<OrderStatus> GetStatusAsync(int id);
}