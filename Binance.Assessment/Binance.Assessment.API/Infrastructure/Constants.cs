namespace Binance.Assessment.API.Infrastructure;

public class Constants
{
    public const string BtcTickerName = "BTCUSDT";
    public const string AdaTickerName = "ADAUSDT";
    public const string EthTickerName = "ETHUSDT";
    public const string OneMinuteDataPoint = "1m";
    public const string FiveMinutesDataPoint = "5m";
    public const string ThirtyMinutesDataPoint = "30m";
    public const string OneDayDataPoint = "1d";
    public const string OneWeekDataPoint = "1w";

    public static readonly IEnumerable<string> DataPoints = new List<string>
    {
        OneMinuteDataPoint, FiveMinutesDataPoint, ThirtyMinutesDataPoint, OneDayDataPoint, OneWeekDataPoint
    };

    public static readonly IEnumerable<string> Tickers = new List<string>
    {
        BtcTickerName, AdaTickerName, EthTickerName
    };
}