using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessment.Repositories.Models;
using Google.Cloud.Spanner.Data;
using System.Text;

namespace Binance.Assessment.Repositories;

public class SymbolPriceRepository : ISymbolPriceRepository
{
    private readonly SpannerConnection _connection;

    public SymbolPriceRepository(SpannerConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<(float, long)>> GetPricesForTimeRange(int symbolId, long startTime, long endTime)
    {
        var prices = new List<(float, long)>();

        var cmd = _connection.CreateSelectCommand(GenerateQueryFor24HAverage(symbolId, startTime, endTime));
        return await ReadDataFromDatabase(cmd);
    }


    public async Task<IEnumerable<(float, long)>> GetClosePricesForTimeIntervals(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
        var prices = new List<(float, long)>();

        var cmd = _connection.CreateSelectCommand(GenerateQueryForSimpleMa(symbolId, endTimesForEachInterval));
        return await ReadDataFromDatabase(cmd);
    }

    private async Task<IEnumerable<(float, long)>> ReadDataFromDatabase(SpannerCommand command)
    {
        var prices = new List<(float, long)>();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var price = reader.GetFieldValue<float>(DatabaseConstants.PriceColumnName);
            var time = reader.GetFieldValue<long>(DatabaseConstants.TimeColumnName);
            prices.Add((price, time));
        }

        return prices;
    }

    private string GenerateQueryForSimpleMa(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
        return new StringBuilder($"Select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                                 $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                                 $"where {DatabaseConstants.SymbolIdColumnName} = {symbolId} " +
                                 $"and {DatabaseConstants.TimeColumnName} in ( {string.Join(", ", endTimesForEachInterval)} )")
            .ToString();
    }

    private string GenerateQueryFor24HAverage(int symbolId, long startTime, long endTime)
    {
        return new StringBuilder($"select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                                 $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                                 $"where {DatabaseConstants.SymbolIdColumnName} = {symbolId} " +
                                 $"and {DatabaseConstants.TimeColumnName} >= {startTime} " +
                                 $"and {DatabaseConstants.TimeColumnName} <= {endTime}")
            .ToString();
    }
}