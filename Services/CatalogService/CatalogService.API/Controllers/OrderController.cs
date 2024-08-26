using CatalogService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using CatalogService.Infrastructure;
using CatalogService.Application.Interfaces;
using CatalogService.Common.DTOs.Orders;

namespace CatalogService.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class OrderController : Controller
{
    private readonly StoreDbContext _context; 
    private readonly IMessageProducer _messagePublisher;

    public OrderController(StoreDbContext context, IMessageProducer messagePublisher)
    {
        _context = context;
        _messagePublisher = messagePublisher;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto input)
    {
        Order order = new()
        {
            Id = input.Id,
            Note = input.Note
        }; 
        await _messagePublisher.SendMessageAsync(order);
        return Ok(new { id = order.Id });
    }
}