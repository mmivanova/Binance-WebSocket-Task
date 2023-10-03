using Binance.Assessment.DomainModel;
using Binance.Assessments.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Binance.Assessments.Services.CachedServices;

//TODO Extend caching logic
public class CachedSymbolPriceService : ISymbolPriceService
{
    private const string TwentyFourHoursAveragePriceCachePrefix = "24H_AP_";
    private const string SimpleMovingAverageCachePrefix = "SMA_AP_";

    private readonly ISymbolPriceService _symbolPriceService;
    private readonly IMemoryCache _cache;

    public CachedSymbolPriceService(ISymbolPriceService symbolPriceService, IMemoryCache cache)
    {
        _symbolPriceService = symbolPriceService;
        _cache = cache;
    }

    /// <summary>
    /// Tries to get data for the specified symbol and end time from the cache, and if it is not successful, it invokes the actual implementation
    /// </summary>
    /// <param name="symbol">The name of the symbol</param>
    /// <param name="endTime">The time from which to start the calculations</param>
    /// <returns>AveragePrice object</returns>
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

    /// <summary>
    /// Tries to get data for the SMA of a specified symbol from the cache, and if it is not successful, it invokes the actual implementation
    /// </summary>
    /// <param name="symbol">The name of the symbol</param>
    /// <param name="request">The SMA characteristics</param>
    /// <returns>AveragePrice object</returns>
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