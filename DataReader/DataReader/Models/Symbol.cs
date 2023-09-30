using System.Text.Json.Serialization;

namespace DataReader.Models;

public enum Symbol
{
    [JsonPropertyName("BTCUSDT")] Btcusdt = 1,
    [JsonPropertyName("ADAUSDT")] Adausdt = 2,
    [JsonPropertyName("ETHUSDT")] Ethusdt = 3
}