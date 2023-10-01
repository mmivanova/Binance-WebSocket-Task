using AutoMapper;
using Binance.Assessment.API.RequestModels;
using Binance.Assessment.API.ResponseModels;
using Binance.Assessment.DomainModel;

namespace Binance.Assessment.API.Infrastructure;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<SimpleMovingAverageRequest, SimpleMovingAverage>();
        CreateMap<AveragePrice, AveragePriceResponse>();
    }
}