using AutoMapper;
using Binance.Assessment.API.RequestModels;
using Binance.Assessment.API.ResponseModels;
using Binance.Assessment.DomainModel;

namespace Binance.Assessment.API.Infrastructure;

public class DomainProfile : Profile
{
    public DomainProfile()
    {
        CreateMap<SimpleMovingAverageRequest, SimpleMovingAverage>()
            .ForMember(dest => dest.DataIntervalTimePeriod, opt => opt.MapFrom(src => src.DataPointTimePeriod))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime));
        CreateMap<AveragePrice, AveragePriceResponse>();
    }
}