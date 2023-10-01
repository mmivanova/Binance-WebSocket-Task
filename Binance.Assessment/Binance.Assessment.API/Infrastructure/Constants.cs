namespace Binance.Assessment.API.Infrastructure;

public class Constants
{
    public const string BtcTicketName = "BTCUSDT";
    public const string AdaTicketName = "ADAUSDT";
    public const string EthTicketName = "ETHUSDT";
    public const string OneMinuteDataPoint = "1m";
    public const string FiveMinutesDataPoint = "5m";
    public const string ThirtyMinutesDataPoint = "30m";
    public const string OneDayDataPoint = "1d";
    public const string OneWeekDataPoint = "1w";

    public readonly IEnumerable<string> DataPoints = new List<string>
    {
        OneMinuteDataPoint, FiveMinutesDataPoint, ThirtyMinutesDataPoint, OneDayDataPoint, OneWeekDataPoint
    };

    public readonly IEnumerable<string> Tickers = new List<string>
    {
        BtcTicketName, AdaTicketName, EthTicketName
    };
}