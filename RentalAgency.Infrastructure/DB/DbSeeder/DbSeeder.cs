using Microsoft.AspNetCore.Identity;
using RentalAgency.Infrastructure.DB.Context;

namespace RentalAgency.Infrastructure.DB.DbSeeder;

public class DbSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "Client", "LandLord"};

        foreach (var role in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}