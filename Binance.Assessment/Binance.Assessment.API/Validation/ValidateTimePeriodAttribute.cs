using System.ComponentModel.DataAnnotations;
using Binance.Assessment.API.Infrastructure;

namespace Binance.Assessment.API.Validation;

public class ValidateTimePeriodAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var timePeriod = value as string;

        return Constants.DataPoints.Contains(timePeriod)
            ? ValidationResult.Success
            : new ValidationResult($"Time period must be one of the following: {string.Join(", ", Constants.DataPoints)}");
    }
}