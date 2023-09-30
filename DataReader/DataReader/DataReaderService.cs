using DataReader.Models;
using DataReader.Services;
using Microsoft.Extensions.Options;

namespace DataReader;

public class DataReaderService : BackgroundService
{
    private readonly StreamsConfig _config;
    private readonly StreamsConnector _connector;
    private readonly DataProcessor _processor;

    public DataReaderService(IOptions<StreamsConfig> config, DataProcessor processor, StreamsConnector connector)
    {
        _processor = processor;
        _connector = connector;
        _config = config.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var btcWebSocket = await _connector.Connect(_config.BtcKlineStream, cancellationToken);
        var adaWebSocket = await _connector.Connect(_config.AdaKlineStream, cancellationToken);
        var ethWebSocket = await _connector.Connect(_config.EthKlineStream, cancellationToken);

        if (!cancellationToken.IsCancellationRequested)
        {
            await _processor.ProcessDataAndSaveToDatabase(btcWebSocket, cancellationToken);
            await _processor.ProcessDataAndSaveToDatabase(adaWebSocket, cancellationToken);
            await _processor.ProcessDataAndSaveToDatabase(ethWebSocket, cancellationToken);
        }

        await _connector.Disconnect(cancellationToken, btcWebSocket, adaWebSocket, ethWebSocket);
    }
}