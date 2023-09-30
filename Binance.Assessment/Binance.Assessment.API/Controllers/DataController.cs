using Binance.Assessment.API.RequestModels;
using Binance.Assessment.API.Validation;
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

        public DataController(ISymbolPriceService symbolPriceService)
        {
            _symbolPriceService = symbolPriceService;
        }

        [HttpGet]
        [Route("{symbol}/24hAvgPrice")]
        [ValidateSymbol]
        public async Task<ActionResult<float>> Get24HAveragePrice(string symbol)
        {
            var prices = await _symbolPriceService.Get24HAverageForSymbol(symbol, DateTime.Now);
            return Ok(prices);
        }


        [HttpGet]
        [Route("{symbol}/SimpleMovingAverage")]
        [ValidateSymbol]
        public async Task<IActionResult> GetSimpleMovingAverage(string symbol, [FromQuery] SimpleMovingAverageRequest request)
        {
            return Ok();
        }
    }
}
