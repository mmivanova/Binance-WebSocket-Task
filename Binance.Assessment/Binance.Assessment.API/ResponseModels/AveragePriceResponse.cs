namespace Binance.Assessment.API.ResponseModels;

public class AveragePriceResponse
{
    public float Price { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}