using Binance.Assessment.DomainModel;
using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessment.Repositories.Models;
using Google.Cloud.Spanner.Data;

namespace Binance.Assessment.Repositories;

public class SymbolPriceRepository : ISymbolPriceRepository
{
    private readonly SpannerConnection _connection;

    public SymbolPriceRepository(SpannerConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<SymbolPrice>> GetAll()
    {
        var symbolPrices = new List<SymbolPrice>();
        var cmd = _connection.CreateSelectCommand($"select * from {DatabaseConstants.ClosePrice1SecondTableName} limit 1000");
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var symbolPrice = new SymbolPrice
            {
                ClosePrice = reader.GetFieldValue<float>(DatabaseConstants.PriceColumnName),
                ConsecutiveCounts = reader.GetFieldValue<int>(DatabaseConstants.ConsecutiveCountsColumnName),
                SymbolId = reader.GetFieldValue<int>(DatabaseConstants.SymbolIdColumnName),
                CloseTime = reader.GetFieldValue<DateTime>(DatabaseConstants.TimeColumnName)
            };

            symbolPrices.Add(symbolPrice);
        }

        return symbolPrices;
    }

    public async Task<List<float>> GetPricesForTimeRange(int symbolId, DateTime startTime, DateTime endTime)
    {
        var prices = new List<float>();
        var startTimeFormatted = startTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        var endTimeFormatted = endTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        var cmd = _connection.CreateSelectCommand($"select price from {DatabaseConstants.ClosePrice1SecondTableName} where symbol_id = {symbolId} and time >= '{startTimeFormatted}' and time <= '{endTimeFormatted}'");
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            prices.Add(reader.GetFieldValue<float>(DatabaseConstants.PriceColumnName));
        }

        return prices;
    }

}