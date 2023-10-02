using Binance.Assessment.Repositories;
using Binance.Assessment.Repositories.Interfaces;
using Binance.Assessments.Services;
using Binance.Assessments.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Spanner.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Assessment.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        BuildServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void BuildServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            })
            .AddXmlSerializerFormatters();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton(_ => new SpannerConnection(configuration.GetSection("CloudSpannerConnectionString").Value, GoogleCredential.FromFile("third-reporter-400608-ac462efd3c43.json")));
        services.AddSingleton<ISymbolPriceRepository, SymbolPriceRepository>();
        services.AddSingleton<ISymbolPriceService, SymbolPriceService>();
    }
}