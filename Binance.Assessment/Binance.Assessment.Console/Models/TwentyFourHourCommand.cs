namespace Binance.Assessment.Console.Models;

/// <summary>
/// 24H command with the same functionality as in the base class
/// </summary>
public class TwentyFourHourCommand : Command, ICommand
{
    public TwentyFourHourCommand(string? input) : base(input)
    {
    }
}