using Binance.Assessment.DomainModel;
using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessments.Services.Extensions;
using Binance.Assessments.Services.Interfaces;
using Google.Api.Gax.Grpc;

namespace Binance.Assessments.Services;

//TODO Add unit testing for the service
public class SymbolPriceService : ISymbolPriceService
{
    private readonly ISymbolPriceRepository _symbolPriceRepository;

    public SymbolPriceService(ISymbolPriceRepository symbolPriceRepository)
    {
        _symbolPriceRepository = symbolPriceRepository;
    }

    public async Task<AveragePrice> Get24HAverageForSymbol(string symbol, DateTime endTime)
    {
        var startTime = endTime.AddDays(-1);
        var symbolId = Enum.Parse(typeof(Symbol), symbol, true);

        var pricesWithTimes = await _symbolPriceRepository.GetPricesForTimeRange((int)symbolId, startTime.ToUniversalTime().ToEpochMilliseconds(), endTime.ToUniversalTime().ToEpochMilliseconds());

        return GetAveragePriceForPeriod(pricesWithTimes);
    }

    public async Task<AveragePrice> GetSimpleMovingAverage(string symbol, SimpleMovingAverage sma)
    {
        var endTime = sma.StartTime.HasValue
            ? sma.StartTime!.Value.ToDateTime(new TimeOnly())
            : DateTime.Now.Date;

        var symbolId = Enum.Parse(typeof(Symbol), symbol, true);
        var timesToGetClosePricesFor = GetClosingTimesForEveryInterval(endTime, sma);

        var pricesWithTimes = await _symbolPriceRepository.GetClosePricesForTimeIntervals((int)symbolId, timesToGetClosePricesFor);

        return GetAveragePriceForPeriod(pricesWithTimes);
    }

    private AveragePrice GetAveragePriceForPeriod(IEnumerable<(float, long)> pricesWithTimes)
    {
        var averagePrice = pricesWithTimes.Select(p => p.Item1).Average();
        var timePeriodStart = DateTimeOffset.FromUnixTimeMilliseconds(pricesWithTimes.Min(t => t.Item2)).DateTime;
        var timePeriodEnd = DateTimeOffset.FromUnixTimeMilliseconds(pricesWithTimes.Max(t => t.Item2)).DateTime;

        return new AveragePrice(averagePrice, timePeriodStart.ToLocalTime(), timePeriodEnd.ToLocalTime());
    }

    private IEnumerable<long> GetClosingTimesForEveryInterval(DateTime endDate, SimpleMovingAverage sma)
    {
        var times = new List<long> { endDate.ToEpochMilliseconds() };
        for (var i = 1; i <= sma.DataPointsAmount - 1; i++)
        {
            times.Add(endDate.AddMinutes(-((int)sma.DataIntervalTimePeriod * i)).ToEpochMilliseconds());
        }

        return times;
    }
}