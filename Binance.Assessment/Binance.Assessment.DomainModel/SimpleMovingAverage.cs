namespace Binance.Assessment.DomainModel;

public class SimpleMovingAverage
{
    public int DataPointsAmount { get; set; }
    public DataPointsInMinutes DataPointTimePeriod { get; set; }
    public DateTime? StartTime { get; set; }
}