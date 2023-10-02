using DataReader.Models;
using DataReader.Repositories;
using DataReader.Services;
using Google.Cloud.Spanner.Data;
using Microsoft.Extensions.Hosting;

namespace DataReader;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
                {
                    services
                        .AddOptions<StreamsConfig>()
                        .Configure(options =>
                        {
                            context.Configuration.GetSection(nameof(StreamsConfig)).Bind(options);
                        });

                    services.AddSingleton(_ =>
                        new SpannerConnection(context.Configuration.GetSection("CloudSpannerConnectionString").Value));
                    services.AddSingleton<SymbolPriceRepository>();
                    services.AddSingleton<DataProcessor>();
                    services.AddSingleton<StreamsConnector>();

                    services.AddHostedService<DataReaderService>();
                })
                .Build();

        host.Run();
    }
}