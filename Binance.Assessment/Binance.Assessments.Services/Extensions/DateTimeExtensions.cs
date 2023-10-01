namespace Binance.Assessments.Services.Extensions;

public static class DateTimeExtensions
{
    public static long ToEpochMilliseconds(this DateTime dateTime)
    {
        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var roundedDateTime = dateTime.ToUniversalTime().RoundToSeconds();
        var timeSpan = roundedDateTime - epoch;
        return (long)timeSpan.TotalMilliseconds + 999;
    }

    private static DateTime RoundToSeconds(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day,
            dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);
    }
}