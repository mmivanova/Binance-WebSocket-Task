using DataReader.Models;
using Google.Cloud.Spanner.Data;

namespace DataReader.Repositories
{
    public class SymbolPriceRepository
    {
        private const string ClosePriceTableName = "close_price_1_second";
        private const string SymbolIdColumnName = "symbol_id";
        private const string TimeColumnName = "time";
        private const string PriceColumnName = "price";
        private const string ConsecutiveCountsColumnName = "consecutive_counts";

        private readonly SpannerConnection _spanner;

        public SymbolPriceRepository(SpannerConnection spanner)
        {
            _spanner = spanner;
            _spanner.OpenAsync();
        }

        public async Task InsertDataAsync(SymbolPrice price)
        {
            await _spanner.RunWithRetriableTransactionAsync(async transaction =>
            {
                await using var cmd = _spanner.CreateInsertCommand(ClosePriceTableName, new SpannerParameterCollection
                {
                    { SymbolIdColumnName, SpannerDbType.Int64, price.SymbolId },
                    { TimeColumnName, SpannerDbType.Timestamp, price.Time },
                    { PriceColumnName, SpannerDbType.Float64, price.Price },
                });
                cmd.Transaction = transaction;
                return cmd.ExecuteNonQueryAsync();
            });
        }
    }
}
