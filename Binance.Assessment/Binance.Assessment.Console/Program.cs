using Binance.Assessment.Console.Models;
using ICommand = Binance.Assessment.Console.Models.ICommand;

namespace Binance.Assessment.Console;

internal class Program
{
    static void Main(string[] args)
    {
        var input = System.Console.ReadLine();
        try
        {
            var command = ValidateCommand(input);
            System.Console.WriteLine($"{ServiceHelper.GetAveragePriceForCommand(command)}");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }

    private static ICommand ValidateCommand(string? input)
    {
        if (input!.Split(' ').Length == 2)
            return new TwentyFourHourCommand(input);

        if (input!.Split(' ').Length is 4 or 5)
            return new SmaCommand(input);

        throw new ArgumentException("Command is not in the correct format!");
    }
}