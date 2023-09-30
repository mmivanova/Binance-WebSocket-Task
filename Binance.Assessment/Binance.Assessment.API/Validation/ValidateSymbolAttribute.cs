using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.Validation
{
    public class ValidateSymbolAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var symbol = context.ActionArguments["symbol"] as string;

            if (symbol != "BTCUSDT" && symbol != "ADAUSDT" && symbol != "ETHUSDT")
            {
                context.Result = new BadRequestObjectResult("Symbol must be one of the following: BTCUSDT, ADAUSDT, ETHUSDT. Keep in mind that it is case-sensitive.");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
