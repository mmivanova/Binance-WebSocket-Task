namespace Binance.Assessment.DomainModel;

public class AveragePrice
{
    public float Price { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public AveragePrice(float price, DateTime startTime, DateTime endTime)
    {
        Price = price;
        StartTime = startTime;
        EndTime = endTime;
    }

    public override string ToString()
    {
        return $"The average price for the period from {StartTime} to {EndTime} is {Price}";
    }
}