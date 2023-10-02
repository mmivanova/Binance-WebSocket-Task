using Binance.Assessment.Console.Models;
using Binance.Assessment.DomainModel;
using Binance.Assessment.Repositories;
using Binance.Assessments.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Spanner.Data;
using ICommand = Binance.Assessment.Console.Models.ICommand;

namespace Binance.Assessment.Console;

internal class Program
{
    static void Main(string[] args)
    {
        var repo = new SymbolPriceRepository(new SpannerConnection(Constants.SpannerConnectionString, GoogleCredential.FromFile("..\\..\\..\\..\\third-reporter-400608-ac462efd3c43.json")));
        var service = new SymbolPriceService(repo);

        var input = System.Console.ReadLine();
        var command = ValidateCommand(input);

        switch (command.Method)
        {
            case "24h":
                var twentyFourHCommand = command as TwentyFourHourCommand;
                var avPrice24H = service.Get24HAverageForSymbol(twentyFourHCommand!.Symbol, DateTime.Now).Result;
                System.Console.WriteLine(avPrice24H);
                break;
            case "sma":
                var smaCommand = command as SmaCommand;
                var sma = service.GetSimpleMovingAverage(smaCommand!.Symbol, new SimpleMovingAverage(smaCommand.DataPointsAmount, smaCommand.TimeInterval, smaCommand.StartTime)).Result;
                System.Console.WriteLine(sma);
                break;
        }
    }

    private static ICommand? ValidateCommand(string? input)
    {
        if (input!.Split(' ').Length == 2)
            return new TwentyFourHourCommand(input);

        if (input!.Split(' ').Length is 4 or 5)
            return new SmaCommand(input);

        throw new ArgumentException("Command is not in the correct format!");
    }
}