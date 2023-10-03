namespace Binance.Assessment.DomainModel;

public class SimpleMovingAverage
{
    public int DataPointsAmount { get; set; }
    public DataIntervalsInMinutes DataIntervalTimePeriod { get; set; }
    public DateOnly? StartTime { get; set; }

    public SimpleMovingAverage(int dataPointsAmount, DataIntervalsInMinutes dataIntervalTimePeriod, DateOnly? startTime)
    {
        DataPointsAmount = dataPointsAmount;
        DataIntervalTimePeriod = dataIntervalTimePeriod;
        StartTime = startTime;
    }

    public SimpleMovingAverage()
    {
    }
}