namespace Binance.Assessment.DomainModel;

public class Constants
{
    public const string SpannerCredentialsFileName = "third-reporter-400608-ac462efd3c43.json";

    private const string BtcTickerName = "BTCUSDT";
    private const string AdaTickerName = "ADAUSDT";
    private const string EthTickerName = "ETHUSDT";
    private const string OneMinuteDataPoint = "1m";
    private const string FiveMinutesDataPoint = "5m";
    private const string ThirtyMinutesDataPoint = "30m";
    private const string OneDayDataPoint = "1d";
    private const string OneWeekDataPoint = "1w";
    private const string TwentyFourHourAverageCommandName = "24h";
    private const string SmaCommandName = "sma";

    public static readonly IEnumerable<string> TimeIntervals = new List<string>
    {
        OneMinuteDataPoint, FiveMinutesDataPoint, ThirtyMinutesDataPoint, OneDayDataPoint, OneWeekDataPoint
    };

    public static readonly IEnumerable<string> Tickers = new List<string>
    {
        BtcTickerName, AdaTickerName, EthTickerName
    };

    public static readonly IEnumerable<string> CommandMethods = new List<string>
    {
        TwentyFourHourAverageCommandName, SmaCommandName
    };
}