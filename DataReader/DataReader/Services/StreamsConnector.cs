using Binance.Spot;

namespace DataReader.Services;

public class StreamsConnector
{
    public async Task<MarketDataWebSocket> Connect(string streamName, CancellationToken token)
    {
        var webSocket = new MarketDataWebSocket(streamName);
        await webSocket.ConnectAsync(token);
        return webSocket;
    }

    public async Task Disconnect(MarketDataWebSocket socket, CancellationToken token)
    {
        await socket.DisconnectAsync(token);
    }

    public async Task Disconnect(CancellationToken token, params MarketDataWebSocket[] sockets)
    {
        foreach (var socket in sockets)
        {
            await socket.DisconnectAsync(token);
        }
    }
}