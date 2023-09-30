using Newtonsoft.Json;

namespace DataReader.Models;

public class RawPriceObject
{
    [JsonProperty("s")]
    public Symbol Symbol { get; set; }

    [JsonProperty("k")]
    public Kline Kline { get; set; }
}

public class Kline
{
    [JsonProperty("T")]
    public long CloseTimeInMillisecondsEpoch { get; set; }

    [JsonProperty("c")]
    public float ClosePrice { get; set; }
}