using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;
using UseCase.Abstractions.Interfaces;

namespace UseCase.Services;

public class RentalOrderService : IRentalOrderService
{
    private readonly IRentalOrderRepository _rentalOrderRepository;
    private readonly IItemService _itemService;
    private readonly IPaymentService _paymentService;

    public RentalOrderService(IRentalOrderRepository rentalOrderRepository, IItemService itemService,  IPaymentService paymentService)
    {
        _itemService =  itemService;
        _rentalOrderRepository = rentalOrderRepository;
        _paymentService = paymentService;
    }
    
    public async Task<IReadOnlyCollection<RentalOrder>> GetAllAsync()
    {
        return await _rentalOrderRepository.GetAllAsync();
    }

    public async Task<RentalOrder?> GetByIdAsync(int id)
    {
        return await _rentalOrderRepository.GetByIdAsync(id);
    }

    public async Task<RentalOrder> CreateAsync(RentalOrder rentalOrder)
    {
        if (rentalOrder == null) throw new ArgumentNullException(nameof(rentalOrder));
        rentalOrder.TotalCost = CountTotalCost(rentalOrder);
        return await _rentalOrderRepository.CreateAsync(rentalOrder);
    }

    public async Task<RentalOrder> UpdateAsync(int id, RentalOrder rentalOrder)
    {
        if (rentalOrder == null) throw new ArgumentNullException(nameof(rentalOrder));
        
        if (rentalOrder.Id != id)
            throw new ArgumentException($"Id {id} does not match expected {rentalOrder.Id}");
        
        return await _rentalOrderRepository.UpdateAsync(id, rentalOrder);
    }

    public async Task<RentalOrder?> DeleteAsync(int id)
    {
        return await _rentalOrderRepository.DeleteAsync(id);
    }

    public async Task<OrderStatus> GetStatusAsync(int id)
    {
        var rentalOrder = await GetRequiredRentalOrderAsync(id);
        
        return rentalOrder.Status;
    }

    public async Task<OrderStatus> UpdateStatusAsync(int id, OrderStatus status)
    {
        var rentalOrder = await GetRequiredRentalOrderAsync(id);
        
        rentalOrder.Status = status;

        var updatedRentalOrder = await _rentalOrderRepository.UpdateAsync(id, rentalOrder) 
                          ?? throw new KeyNotFoundException("RentalOrder not found");
        
        return updatedRentalOrder.Status;
    }

    public async Task<RentalOrder> SetAsCompletedAsync(int id)
    {
        var rentalOrder = await GetRequiredRentalOrderAsync(id);
        var payments = rentalOrder.Payments;
        
        decimal tolerance = 0.01m;
        decimal paidAmount = rentalOrder.Payments.Select(p => p.Amount).Sum();
        decimal difference = paidAmount - rentalOrder.TotalCost;

        if (Math.Abs(difference) <= tolerance)
        {
            await _itemService.UpdateStatusAsync(rentalOrder.Item.Id, ItemStatus.Available);
            await Task.WhenAll(payments.Select(p => _paymentService.UpdateStatusAsync(p.Id, PaymentStatus.Paid)));
            
            rentalOrder.Status = OrderStatus.Completed;
        }
        else if (difference < 0)
        {
            throw new Exception("Total cost is greater than payments");
        }
        else
        {
            throw new Exception("Total cost is less than payments");
        }
        var updatedRentalOrder = await _rentalOrderRepository.UpdateAsync(id, rentalOrder);

        return updatedRentalOrder;
    }

    private decimal CountTotalCost(RentalOrder rentalOrder)
    {
        if (rentalOrder.EndDate < rentalOrder.StartDate)
            throw new ArgumentException("End date cannot be earlier than start date");

        var pricePerDay = rentalOrder.Item.PricePerDay;
        var plannedDays = (rentalOrder.EndDate - rentalOrder.StartDate).Days;
        var overdueDays = Math.Max(0, (rentalOrder.ReturnDate - rentalOrder.EndDate).Days);

        return pricePerDay * (plannedDays + overdueDays);
    }

    private async Task<RentalOrder> GetRequiredRentalOrderAsync(int id)
    {
        var rentalOrder = await _rentalOrderRepository.GetByIdAsync(id);
        
        if (rentalOrder == null)
            throw new KeyNotFoundException("RentalOrder not found");
        
        return rentalOrder;
    }
}