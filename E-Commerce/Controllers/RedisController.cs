using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace E_Commerce.Controllers;

[Route("api/[controller]")]
public class RedisController : Controller
{
    private readonly IDistributedCache _distributedCache;
    public RedisController(IDistributedCache distributedCache) => _distributedCache = distributedCache;

    [HttpGet]
    public async Task<string> Get()
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