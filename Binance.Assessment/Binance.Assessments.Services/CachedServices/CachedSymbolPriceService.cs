using Binance.Assessment.DomainModel;
using Binance.Assessments.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Binance.Assessments.Services.CachedServices;

public class CachedSymbolPriceService : ISymbolPriceService
{
    private const string TwentyFourHoursAveragePriceCachePrefix = "24H_AP_";
    private const string SimpleMovingAverageCachePrefix = "24H_AP_";

    private readonly ISymbolPriceService _symbolPriceService;
    private IMemoryCache _cache;

    public CachedSymbolPriceService(ISymbolPriceService symbolPriceService, IMemoryCache cache)
    {
        _symbolPriceService = symbolPriceService;
        _cache = cache;
    }

    public async Task<AveragePrice> Get24HAverageForSymbol(string symbol, DateTime endTime)
    {
        if (_cache.TryGetValue($"{TwentyFourHoursAveragePriceCachePrefix}_{symbol}_{endTime}", out AveragePrice? averagePrice))
        {
            return averagePrice!;
        }

        averagePrice = await _symbolPriceService.Get24HAverageForSymbol(symbol, endTime);
        _cache.Set($"{TwentyFourHoursAveragePriceCachePrefix}_{symbol}_{endTime}", averagePrice);

        return averagePrice;
    }

    public async Task<AveragePrice> GetSimpleMovingAverage(string symbol, SimpleMovingAverage request)
    {
        var cacheKey = $"{SimpleMovingAverageCachePrefix}_{symbol}_{request.DataIntervalTimePeriod}_{request.DataPointsAmount}_{request.StartTime}";
        if (_cache.TryGetValue(cacheKey, out AveragePrice? averagePrice))
        {
            return averagePrice!;
        }

        averagePrice = await _symbolPriceService.GetSimpleMovingAverage(symbol, request);
        _cache.Set(cacheKey, averagePrice);

        return averagePrice;
    }
}