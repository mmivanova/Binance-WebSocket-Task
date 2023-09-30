namespace Binance.Assessments.Services.Interfaces;

public interface ISymbolPriceService
{
    Task<float> Get24HAverageForSymbol(string symbol, DateTime endTime);
}