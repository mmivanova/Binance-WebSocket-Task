namespace Binance.Assessment.DomainModel;

public class SimpleMovingAverage
{
    public int DataPointsAmount { get; set; }
    public DataIntervalsInMinutes DataIntervalTimePeriod { get; set; }
    public DateTime? StartTime { get; set; }

    public SimpleMovingAverage(int dataPointsAmount, DataIntervalsInMinutes dataIntervalTimePeriod, DateTime? startTime)
    {
        DataPointsAmount = dataPointsAmount;
        DataIntervalTimePeriod = dataIntervalTimePeriod;
        StartTime = startTime;
    }
}