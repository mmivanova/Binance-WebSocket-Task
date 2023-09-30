using Binance.Assessment.DomainModel;
using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessments.Services.Interfaces;

namespace Binance.Assessments.Services;

public class SymbolPriceService : ISymbolPriceService
{
    private readonly ISymbolPriceRepository _symbolPriceRepository;

    public SymbolPriceService(ISymbolPriceRepository symbolPriceRepository)
    {
        _symbolPriceRepository = symbolPriceRepository;
    }

    public async Task<float> Get24HAverageForSymbol(string symbol, DateTime endTime)
    {
        var startTime = endTime.AddDays(-1);

        var symbolId = Enum.Parse(typeof(Symbol), symbol, true);
        var prices = await _symbolPriceRepository.GetPricesForTimeRange((int)symbolId, startTime, endTime);

        return prices.Average();
    }
}