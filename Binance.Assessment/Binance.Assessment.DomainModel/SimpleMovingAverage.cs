namespace Binance.Assessment.DomainModel;

public class SimpleMovingAverage
{
    public int DataPointsAmount { get; set; }
    public DataIntervalsInMilliseconds DataIntervalTimePeriod { get; set; }
    public DateTime? StartTime { get; set; }
}