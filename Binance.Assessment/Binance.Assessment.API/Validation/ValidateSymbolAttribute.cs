using Binance.Assessment.DomainModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Binance.Assessment.API.Validation;

public class ValidateSymbolAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var symbol = context.ActionArguments["symbol"] as string;

        if (!Constants.Tickers.Contains(symbol))
        {
            context.Result = new BadRequestObjectResult($"Symbol must be one of the following: {string.Join(", ", Constants.Tickers)}. Keep in mind that it is case-sensitive.");
            return;
        }

        base.OnActionExecuting(context);
    }
}