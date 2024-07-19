using E_Commerce.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ColorController : ControllerBase
{
    private readonly IColorService _service;
    
    public ColorController(IColorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    // [HttpGet("{id}")]
    // public async Task<ColorDto?> Get(string id)
    // {
    //     var color = await _service.GetAsync(id);
    //     if (color == null)
    //     {
    //         return null;
    //     }
    //     return _mapper.Map<Color, ColorDto>(color);
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult<Color>> Create(CreateColorDto newModel)
    // {
    //     Color color = _mapper.Map<CreateColorDto, Color>(newModel);
    //     return await _service.CreateAsync(color);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<Color> Update(string id, UpdateColorDto updatedModel)
    // {
    //     Color color = _mapper.Map<UpdateColorDto, Color>(updatedModel);
    //     return await _service.UpdateAsync(id, color);
    // }

    [HttpDelete("{id}")]
    public async Task<bool> Delete(Guid id) => await _service.DeleteAsync(id);
}