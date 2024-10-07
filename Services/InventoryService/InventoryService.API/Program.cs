using InventoryService.Application.Abstractions;
using InventoryService.Application.Mappers;
using InventoryService.Domain.Abstractions.Repositories;
using InventoryService.Infrastructure.Context;
using InventoryService.Infrastructure.Repositories;
using InventoryService.Infrastructure.UoW;
using Microsoft.AspNetCore.Hosting;
using MainService = InventoryService.Application.Services.InventoryService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInventoryService, MainService>();
//builder.Services.Scan(scan => scan
//        .FromAssemblies(builder.GetType().Assembly)
//        .AddClasses(classes => classes.Where(t => t.Name.StartsWith("I")))
//        .AsImplementedInterfaces()
//        .WithScopedLifetime());

builder.Services.AddAutoMapper(typeof(InventoryProfile).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
