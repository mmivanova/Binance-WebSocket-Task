using System.ComponentModel.DataAnnotations;

namespace Binance.Assessment.API.Validation;

public class ValidateDateAttribute : ValidationAttribute
{
    /// <summary>
    /// Validates that the provided date is in the correct yyyy-MM-dd format
    /// </summary>
    /// <param name="value">the date to validate</param>
    /// <param name="validationContext"></param>
    /// <returns>Success or throws ValidationResult exception</returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dateTime = value!.ToString();
        const string acceptableDateFormat = "yyyy-MM-dd";

        return DateOnly.TryParseExact(dateTime, acceptableDateFormat, out _)
            ? ValidationResult.Success
            : new ValidationResult($"The Date is not in a valid {acceptableDateFormat} format.");
    }
}