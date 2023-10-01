using Binance.Assessment.DomainModel;

namespace Binance.Assessments.Services.Interfaces;

public interface ISymbolPriceService
{
    Task<AveragePrice> Get24HAverageForSymbol(string symbol, DateTime endTime);
    Task<AveragePrice> GetSimpleMovingAverage(string symbol, SimpleMovingAverage request);
}