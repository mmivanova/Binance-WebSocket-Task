﻿using Binance.Assessment.Console.Models;
using Binance.Assessment.DomainModel;
using Binance.Assessment.Repositories;
using Binance.Assessments.Services;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Spanner.Data;

namespace Binance.Assessment.Console;

public static class ServiceHelper
{
    private static readonly SymbolPriceRepository Repository = new(new SpannerConnection(
        Constants.SpannerConnectionString,
        GoogleCredential.FromFile($"..\\..\\..\\..\\{DomainModel.Constants.SpannerCredentialsFileName}")));

    private static readonly SymbolPriceService Service = new(Repository);

    public static async Task<AveragePrice> GetAveragePriceForCommand(ICommand command)
    {
        switch (command.Method)
        {
            case "24h":
                var twentyFourHCommand = command as TwentyFourHourCommand;
                return await Service.Get24HAverageForSymbol(twentyFourHCommand!.Symbol, DateTime.Now);
            case "sma":
                var smaCommand = command as SmaCommand;
                return await Service.GetSimpleMovingAverage(smaCommand!.Symbol, new SimpleMovingAverage(smaCommand.DataPointsAmount, smaCommand.TimeInterval, smaCommand.StartTime));
            default:
                throw new NotSupportedException();
        }
    }
}