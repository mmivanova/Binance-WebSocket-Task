namespace Binance.Assessment.Repositories.Interfaces;

public interface ISymbolPriceRepository
{
    Task<IEnumerable<(float, long)>> GetPricesForTimeRange(int symbolId, long startTime, long endTime);
    Task<IEnumerable<(float, long)>> GetClosePricesForTimeIntervals(int symbolId, IEnumerable<long> endTimesForEachInterval);
}