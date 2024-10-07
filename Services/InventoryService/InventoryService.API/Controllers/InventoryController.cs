using Microsoft.AspNetCore.Mvc;
using InventoryService.Application.Abstractions;
using InventoryService.Application.DTOs;

namespace InventoryService.API.Controllers;

[Route("[controller]")]
[ApiController]
public class InventoryController : Controller
{
    private readonly IInventoryService _service;
    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var products = await _service.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateInventoryDto inputModel)
    {
        var model = await _service.CreateAsync(inputModel);
        return Ok(model);
    }
}
