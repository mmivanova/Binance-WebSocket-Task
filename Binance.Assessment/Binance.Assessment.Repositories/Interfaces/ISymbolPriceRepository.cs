namespace Binance.Assessment.Repositories.Interfaces
{
    public interface ISymbolPriceRepository
    {
        Task<List<float>> GetPricesForTimeRange(int symbolId, DateTime startTime, DateTime endTime);
    }
}
