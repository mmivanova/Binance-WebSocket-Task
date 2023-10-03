using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessment.Repositories.Models;
using Google.Cloud.Spanner.Data;
using System.Text;

namespace Binance.Assessment.Repositories;

/// <summary>
/// Establishes a connection to the Cloud Spanner Database
/// </summary>
public class SymbolPriceRepository : ISymbolPriceRepository
{
    private readonly SpannerConnection _connection;

    public SymbolPriceRepository(SpannerConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Gets the prices with their time of occurrence for a specified symbol between the two dates
    /// </summary>
    /// <param name="symbolId">Id of the symbol</param>
    /// <param name="startTime">From when to start collecting the data</param>
    /// <param name="endTime">The current point in time</param>
    /// <returns>Task&lt;IEnumerable&lt;(float, long)&gt;&gt;</returns>
    public async Task<IEnumerable<(float, long)>> GetPricesForTimeRange(int symbolId, long startTime, long endTime)
    {
        var cmd = GetCommandFor24HAverage(symbolId, startTime, endTime);
        return await ReadDataFromDatabase(cmd);
    }

    /// <summary>
    /// Gets the prices with their time of occurrence for a specified symbol between the two dates
    /// </summary>
    /// <param name="symbolId">Id of the symbol</param>
    /// <param name="endTimesForEachInterval">The time which to get the closing price for</param>
    /// <returns>Task&lt;IEnumerable&lt;(float, long)&gt;&gt;</returns>
    public async Task<IEnumerable<(float, long)>> GetClosePricesForTimeIntervals(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
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

    //TODO update query to use a query parameters instead of plain text
    private SpannerCommand GetCommandForSimpleMa(int symbolId, IEnumerable<long> endTimesForEachInterval)
    {
        var query = new StringBuilder($"Select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                                 $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                                 $"where {DatabaseConstants.SymbolIdColumnName} = {symbolId} " +
                                 $"and {DatabaseConstants.TimeColumnName} in ( {string.Join(", ", endTimesForEachInterval)} )")
            .ToString();

        return _connection.CreateSelectCommand(query);
    }

    //TODO update query to use a query parameters instead of plain text
    private SpannerCommand GetCommandFor24HAverage(int symbolId, long startTime, long endTime)
    {
        var query = new StringBuilder($"select {DatabaseConstants.PriceColumnName}, {DatabaseConstants.TimeColumnName} " +
                          $"from {DatabaseConstants.ClosePrice1SecondTableName} " +
                          $"where {DatabaseConstants.SymbolIdColumnName} = {symbolId}" +
                          $"and {DatabaseConstants.TimeColumnName} >= {startTime} " +
                          $"and {DatabaseConstants.TimeColumnName} <= {endTime} ")
                    .ToString();

        var command = _connection.CreateSelectCommand(query);

        return command;
    }
}