using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Binance.Assessment.API.Validation;

public class ValidateDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dateTime = value!.ToString();
        const string acceptableDateFormat = "yyyy-MM-dd";

        return DateOnly.TryParseExact(dateTime, acceptableDateFormat, out _)
            ? ValidationResult.Success
            : new ValidationResult($"The Date is not in a valid {acceptableDateFormat} format.");
    }
}