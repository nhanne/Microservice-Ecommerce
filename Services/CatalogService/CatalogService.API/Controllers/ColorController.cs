using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Globalization;

namespace CatalogService.API.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class ColorController : ControllerBase
{
    private readonly IColorService _service;
    private readonly IDistributedCache _distributedCache;

    public ColorController(IColorService service, IDistributedCache distributedCache)
    {
        _service = service;
        _distributedCache = distributedCache;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        return Ok(await _service.GetAllAsync());
    }

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
    public async Task<bool> Delete(Guid id)
    {
        return await _service.DeleteAsync(id);
    }

    [HttpGet("RedisColor")]
    public async Task<string> GetColorRedis()
    {
        var cacheKey = "The Time";
        var currentTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        var cachedTime = await _distributedCache.GetStringAsync(cacheKey);
        if (string.IsNullOrEmpty(cachedTime))
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
            await _distributedCache.SetStringAsync(cacheKey, currentTime, options);
            cachedTime = await _distributedCache.GetStringAsync(cacheKey);
        }

        var result = $"Current Time: {currentTime} \nCached Time: {cachedTime}";
        return result;
    }
}