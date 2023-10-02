namespace Binance.Assessment.DomainModel;

public class SimpleMovingAverage
{
    public int DataPointsAmount { get; set; }
    public DataIntervalsInMinutes DataIntervalTimePeriod { get; set; }
    public DateTime? StartTime { get; set; }
}