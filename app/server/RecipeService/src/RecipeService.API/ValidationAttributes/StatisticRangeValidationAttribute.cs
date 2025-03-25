namespace RecipeService.API.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

public class StatisticRangeValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedValues = { "24h", "7d", "30d", "3M", "12M", "24M" };
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && (!AllowedValues.Contains(stringValue) && stringValue != ""))
        {
            return new ValidationResult($"The statistic range must be one of the following values: {string.Join(", ", AllowedValues)}.");
        }

        return ValidationResult.Success;
    }
}
