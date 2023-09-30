using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.RequestModels;


public class SimpleMovingAverageRequest
{
    [BindProperty(Name = "n")]
    public int DataPointsAmount { get; set; }

    [BindProperty(Name = "p")]
    public string DataPointTimePeriod { get; set; } //TODO make enum

    [BindProperty(Name = "s")]
    public DateTime StartTime { get; set; }
}