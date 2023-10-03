using AutoMapper;
using Binance.Assessment.API.RequestModels;
using Binance.Assessment.API.ResponseModels;
using Binance.Assessment.API.Validation;
using Binance.Assessment.DomainModel;
using Binance.Assessments.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.Controllers;

//TODO Add unit testing for the API layer
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

    /// <summary>
    /// Get the average price for the last 24 hours for the specified symbol
    /// </summary>
    /// <param name="symbol">*Case-sensitive* The ticker of the crypto - "BTCUSDT", "ADAUSDT", "ETHUSDT"</param>
    /// <returns>AveragePriceResponse object</returns>
    /// <response code="200">Success, returns the calculated average price</response>
    /// <response code="400">The symbol was not in the correct format or is not supported. </response>
    [HttpGet]
    [Route("{symbol}/24hAvgPrice")]
    [ValidateSymbol]
    public async Task<ActionResult<AveragePriceResponse>> Get24HAveragePrice(string symbol)
    {
        var averagePrice = await _symbolPriceService.Get24HAverageForSymbol(symbol, DateTime.Now);
        return Ok(_mapper.Map<AveragePriceResponse>(averagePrice));
    }

    /// <summary>
    /// Get the Simple Moving Average for the specified symbol
    /// </summary>
    /// <param name="symbol">*Case-sensitive* The ticker of the crypto - "BTCUSDT", "ADAUSDT", "ETHUSDT"</param>
    /// <param name="request">The data points </param>
    /// <returns>AveragePriceResponse object</returns>
    /// <response code="200">Success, returns the calculated SMA</response>
    /// <response code="400">One or more validations failed. A meaningful message will be displayed.</response>
    [HttpGet]
    [Route("{symbol}/SimpleMovingAverage")]
    [ValidateSymbol]
    public async Task<ActionResult<AveragePriceResponse>> GetSimpleMovingAverage(string symbol, [FromQuery] SimpleMovingAverageRequest request)
    {
        var simpleMovingAverage = await _symbolPriceService.GetSimpleMovingAverage(symbol, _mapper.Map<SimpleMovingAverage>(request));
        return Ok(_mapper.Map<AveragePriceResponse>(simpleMovingAverage));
    }
}