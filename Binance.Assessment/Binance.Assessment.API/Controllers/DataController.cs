using AutoMapper;
using Binance.Assessment.API.RequestModels;
using Binance.Assessment.API.ResponseModels;
using Binance.Assessment.API.Validation;
using Binance.Assessment.DomainModel;
using Binance.Assessments.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class DataController : ControllerBase
    {
        private readonly ISymbolPriceService _symbolPriceService;
        private readonly IMapper _mapper;

        public DataController(ISymbolPriceService symbolPriceService, IMapper mapper)
        {
            _symbolPriceService = symbolPriceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{symbol}/24hAvgPrice")]
        [ValidateSymbol]
        public async Task<ActionResult<AveragePriceResponse>> Get24HAveragePrice(string symbol)
        {
            var averagePrice = await _symbolPriceService.Get24HAverageForSymbol(symbol, DateTime.Now);
            return Ok(_mapper.Map<AveragePriceResponse>(averagePrice));
        }


        [HttpGet]
        [Route("{symbol}/SimpleMovingAverage")]
        [ValidateSymbol]
        public async Task<ActionResult<AveragePriceResponse>> GetSimpleMovingAverage(string symbol, [FromQuery] SimpleMovingAverageRequest request)
        {
            var simpleMovingAverage = await _symbolPriceService.GetSimpleMovingAverage(symbol, _mapper.Map<SimpleMovingAverage>(request));
            return Ok(_mapper.Map<AveragePriceResponse>(simpleMovingAverage));
        }
    }
}
