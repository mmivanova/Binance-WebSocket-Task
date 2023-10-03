﻿using Binance.Assessment.DomainModel;
using System.Globalization;

namespace Binance.Assessment.Console.Models;

public class SmaCommand : Command, ICommand
{
    public int DataPointsAmount { get; set; }
    public DataIntervalsInMinutes TimeInterval { get; set; }
    public DateTime? StartTime { get; set; }

    public SmaCommand(string? input) : base(input)
    {
        var elements = input!.Split(' ');
        DataPointsAmount = ValidateAndExtractDataPointsAmount(elements);
        TimeInterval = ValidateAndExtractTimeInterval(elements);
        StartTime = ValidateAndExtractStartTime(elements);
    }

    private static int ValidateAndExtractDataPointsAmount(IReadOnlyList<string> elements)
    {
        if (!int.TryParse(elements[2], out var dataPoint) && dataPoint is <= 0 or > 1000)
        {
            throw new ArgumentException("Wrong data point amount in the command! Should be an integer bigger than 0 and less than 1000");
        }

        return dataPoint;
    }

    private static DataIntervalsInMinutes ValidateAndExtractTimeInterval(IReadOnlyList<string> elements)
    {
        if (!Enum.TryParse(elements[3], out DataIntervalsInMinutes dataInterval))
        {
            throw new ArgumentException($"Wrong time interval in the command! Should be one of the following {DomainModel.Constants.TimeIntervals}");
        }

        return dataInterval;
    }

    private static DateTime? ValidateAndExtractStartTime(IReadOnlyList<string> elements)
    {
        if (elements.Count < 5) return null;

        if (!DateTime.TryParseExact(elements[5], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw new ArgumentException("Invalid Date format");
        }

        return date;
    }
}