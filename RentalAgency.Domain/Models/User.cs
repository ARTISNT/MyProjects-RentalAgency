using Microsoft.AspNetCore.Identity;

namespace RentalAgency.Models;

public class User : IdentityUser
{
    private decimal _walletBalance;
    public decimal WalletBalance
    {
        get { return _walletBalance; }
        set
        {
            if (value < 0)
            {
                throw new InvalidOperationException("Cannot set WalletBalance to negative");
            }
            _walletBalance = value;
        }
    }
    
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public ICollection<RentalOrder> RentalOrders { get; set; } = new List<RentalOrder>();
}