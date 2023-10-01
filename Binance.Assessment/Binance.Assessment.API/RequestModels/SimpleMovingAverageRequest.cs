﻿using Binance.Assessment.API.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Binance.Assessment.DomainModel;

namespace Binance.Assessment.API.RequestModels;

public class SimpleMovingAverageRequest
{
    [BindProperty(Name = "n")]
    public int DataPointsAmount { get; set; }

    [BindProperty(Name = "p")]
    [ValidateTimePeriod]
    public string DataPointTimePeriod { get; set; }

    [BindProperty(Name = "s")]
    public DateTime? StartTime { get; set; }
}