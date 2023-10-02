using Binance.Spot;
using DataReader.Models;
using DataReader.Repositories;
using Newtonsoft.Json;

namespace DataReader.Services;

public class DataProcessor
{
    private readonly SymbolPriceRepository _priceRepository;
    private readonly ILogger _logger;

    public DataProcessor(SymbolPriceRepository priceRepository, ILogger<DataProcessor> logger)
    {
        _priceRepository = priceRepository;
        _logger = logger;
    }

    public async Task ProcessDataAndSaveToDatabase(MarketDataWebSocket socket, CancellationToken token)
    {
        socket.OnMessageReceived(
            async data =>
            {
                try
                {
                    _logger.LogInformation("{DateTime.Now} Received message {data}", DateTime.Now, data);
                    var rawPrice = JsonConvert.DeserializeObject<RawPriceObject>(data)!;
                    _logger.LogInformation($"{DateTime.Now} Inserting data - Symbol {rawPrice.Symbol}, time {rawPrice.Kline.CloseTimeInMillisecondsEpoch}, {rawPrice.Kline.ClosePrice}");
                    await _priceRepository.InsertDataAsync(new SymbolPrice(rawPrice));
                }
                catch (Exception e)
                {
                    _logger.LogError("{DateTime.Now} Exception - {e}; Inner exception - {e.InnerException}", DateTime.Now, e.Message, e.InnerException?.Message);
                    throw;
                }
            }, token);
    }
}