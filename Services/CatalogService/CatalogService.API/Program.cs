using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Application.RabbitMQ;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services;
using Microsoft.EntityFrameworkCore;
using CatalogService.Infrastructure;
using CatalogService.Application;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(
                 builder.Configuration.GetConnectionString("SqlDB")), ServiceLifetime.Scoped);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisInstance";
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IRepository<Color>, ColorRepository>();
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

