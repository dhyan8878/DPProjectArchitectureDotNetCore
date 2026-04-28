using DP.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace DP.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService>  logger  )
    {
        _db = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            _logger.LogInformation("Cache MISS for key: {CacheKey}", key);
            return default;
        }
        
        _logger.LogInformation("Cache HIT for key: {CacheKey}", key);

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);

        if (expiry.HasValue)
        {
            await _db.StringSetAsync(key, json, expiry.Value);
        }
        else
        {
            await _db.StringSetAsync(key, json);
        }
        _logger.LogInformation("Cache SET for key: {CacheKey} with expiry: {Expiry}",
                           key,
                           expiry?.ToString() ?? "none");
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
        _logger.LogInformation("Cache REMOVE for key: {CacheKey}", key);
    }
    public async Task RemoveByPrefixAsync(string prefix)
    {
        var endpoints = _db.Multiplexer.GetEndPoints();
        var server = _db.Multiplexer.GetServer(endpoints.First());

        var keys = server.Keys(pattern: $"{prefix}*");

        foreach (var key in keys)
        {
            await _db.KeyDeleteAsync(key);
        }

        _logger.LogInformation("Cache REMOVE by prefix: {Prefix}, Keys removed: {Count}",
            prefix,keys.Count());
    }
}