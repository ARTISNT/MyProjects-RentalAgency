using RentalAgency.Models;

namespace RentalAgency.Interfaces.Repositories;

public interface IRentalOrderRepository
{
    public Task<IReadOnlyCollection<RentalOrder>> GetAllAsync();
    public Task<RentalOrder?> GetByIdAsync(int id);
    public Task<RentalOrder> CreateAsync(RentalOrder rentalOrder);
    public Task<RentalOrder> UpdateAsync(RentalOrder rentalOrder);
    public Task<bool> DeleteAsync(int id);
}