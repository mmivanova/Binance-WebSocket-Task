using Binance.Spot;
using DataReader.Models;
using DataReader.Repositories;
using Newtonsoft.Json;

namespace DataReader.Services;

public class DataProcessor
{
    private readonly SymbolPriceRepository _priceRepository;

    public DataProcessor(SymbolPriceRepository priceRepository)
    {
        _priceRepository = priceRepository;
    }

    public async Task ProcessDataAndSaveToDatabase(MarketDataWebSocket socket, CancellationToken token)
    {
        socket.OnMessageReceived(
            async data =>
            {
                try
                {
                    var rawPrice = JsonConvert.DeserializeObject<RawPriceObject>(data)!;
                    await _priceRepository.InsertDataAsync(new SymbolPrice(rawPrice));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }, token);
    }
}