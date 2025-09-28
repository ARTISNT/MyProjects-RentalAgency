using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalAgency.Api.ExceptionHandling;
using RentalAgency.Infrastructure.DB.Context;
using RentalAgency.Infrastructure.DB.DbSeeder;
using RentalAgency.Infrastructure.Repositories;
using RentalAgency.Interfaces.Repositories;
using RentalAgency.Models;
using UseCase.Abstractions.Interfaces;
using UseCase.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddControllers(options => 
    options.Filters.Add<GlobalExceptionFilter>());

builder.Services.AddDbContext<RentalAgencyDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.MapEnum<ItemStatus>("item_status")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<RentalAgencyDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var  roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DbSeeder.SeedAsync(roleManager);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}


if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
}

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();

