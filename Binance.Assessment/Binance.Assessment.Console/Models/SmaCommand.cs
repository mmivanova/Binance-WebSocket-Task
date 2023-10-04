using Binance.Assessment.DomainModel;
using System.Globalization;

namespace Binance.Assessment.Console.Models;

/// <summary>
/// Simple moving average command object
/// </summary>
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

    /// <summary>
    /// Validates that the third argument of the command is a valid data points amount number
    /// </summary>
    /// <param name="elements">the user command arguments</param>
    /// <returns>The data points amount</returns>
    /// <exception cref="ArgumentException"></exception>
    private static int ValidateAndExtractDataPointsAmount(IReadOnlyList<string> elements)
    {
        if (!int.TryParse(elements[2], out var dataPoint) && dataPoint is <= 0 or > 1000)
        {
            throw new ArgumentException("Wrong data point amount in the command! Should be an integer bigger than 0 and less than 1000");
        }

        return dataPoint;
    }

    /// <summary>
    /// Validates that the fourth argument of the command is a valid time interval
    /// </summary>
    /// <param name="elements">the user command arguments</param>
    /// <returns>The time interval</returns>
    /// <exception cref="ArgumentException"></exception>
    private static DataIntervalsInMinutes ValidateAndExtractTimeInterval(IReadOnlyList<string> elements)
    {
        if (!Enum.TryParse(elements[3], out DataIntervalsInMinutes dataInterval))
        {
            throw new ArgumentException($"Wrong time interval in the command! Should be one of the following {DomainModel.Constants.TimeIntervals}");
        }

        return dataInterval;
    }

    /// <summary>
    /// Validates that if there is a fifth argument in the command, it is a valid date
    /// </summary>
    /// <param name="elements">the user command arguments</param>
    /// <returns>The date from which to start the calculations</returns>
    /// <exception cref="ArgumentException"></exception>
    private static DateTime? ValidateAndExtractStartTime(IReadOnlyList<string> elements)
    {
        if (elements.Count < 5) return null;

        if (!DateOnly.TryParseExact(elements[5], "yyyy-MM-dd", out var dateOnly))
        {
            throw new ArgumentException("Invalid Date format");
        }

        return dateOnly.ToDateTime(new TimeOnly());
    }
}