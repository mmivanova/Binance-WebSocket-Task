using System.ComponentModel.DataAnnotations;
using Binance.Assessment.API.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.RequestModels;

public class SimpleMovingAverageRequest
{
    /// <summary>
    /// Amount of data points between 1 and 999
    /// </summary>
    [BindProperty(Name = "n")]
    [Range(1, 999, ErrorMessage = "Data points amount should be between 1 and 999")]
    public int DataPointsAmount { get; set; }

    /// <summary>
    /// The time period represented by each data point. Acceptable values: 1w, 1d, 30m, 5m, 1m
    /// </summary>
    [BindProperty(Name = "p")]
    [ValidateTimePeriod]
    public string DataPointTimePeriod { get; set; }

    /// <summary>
    /// The datetime from which to start the SMA calculation ( a date )
    /// </summary>
    [BindProperty(Name = "s")]
    [ValidateDate]
    public DateOnly? StartTime { get; set; }
}