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

        var cmd = GetCommandFor24HAverage(symbolId, startTime, endTime);
        return await ReadDataFromDatabase(cmd);
    }

    public async Task<IEnumerable<(float, long)>> GetClosePricesForTimeIntervals(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
        var prices = new List<(float, long)>();

        var cmd = GetCommandForSimpleMa(symbolId, endTimesForEachInterval);
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

    private SpannerCommand GetCommandForSimpleMa(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
        var query = new StringBuilder($"Select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                                 $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                                 $"where {DatabaseConstants.SymbolIdColumnName} = {symbolId} " +
                                 $"and {DatabaseConstants.TimeColumnName} in ( {string.Join(", ", endTimesForEachInterval)} )")
            .ToString();

        //var command = _connection.CreateSelectCommand(query, new SpannerParameterCollection
        //{
        //    { $"{nameof(symbolId)}", SpannerDbType.Int64 },
        //    { $"{nameof(endTimesForEachInterval)}", SpannerDbType.ArrayOf(SpannerDbType.Int64) }
        //});
        //command.Parameters[$"{nameof(symbolId)}"].Value = symbolId;
        //command.Parameters[$"{nameof(endTimesForEachInterval)}"].Value = endTimesForEachInterval;

        return _connection.CreateSelectCommand(query);
    }

    private SpannerCommand GetCommandFor24HAverage(int symbolId, long startTime, long endTime)
    {
        var query = new StringBuilder($"select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                          $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                          $"where {DatabaseConstants.SymbolIdColumnName} = @{nameof(symbolId)} " +
                          $"and {DatabaseConstants.TimeColumnName} >= @{nameof(startTime)} " +
                          $"and {DatabaseConstants.TimeColumnName} <= @{nameof(endTime)}")
                    .ToString();

        var command = _connection.CreateSelectCommand(query, new SpannerParameterCollection
        {
            { $"{nameof(symbolId)}", SpannerDbType.Int64 },
            { $"{nameof(startTime)}", SpannerDbType.Int64 },
            { $"{nameof(endTime)}", SpannerDbType.Int64 }
        });
        command.Parameters[$"{nameof(symbolId)}"].Value = symbolId;
        command.Parameters[$"{nameof(startTime)}"].Value = startTime;
        command.Parameters[$"{nameof(endTime)}"].Value = endTime;

        return command;
    }
}