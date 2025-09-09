using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalAgency.Infrastructure.DB.Context;
using RentalAgency.Infrastructure.DB.DbSeeder;
using RentalAgency.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DbSeeder.SeedAsync(roleManager);
}

app.Run();
