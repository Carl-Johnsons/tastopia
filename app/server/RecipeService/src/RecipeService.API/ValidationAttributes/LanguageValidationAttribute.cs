namespace RecipeService.API.ValidationAttributes;

using Contract.Constants;
using System.ComponentModel.DataAnnotations;

public class LanguageValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedValues = { LanguageValidation.En, LanguageValidation.Vi};

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && (!AllowedValues.Contains(stringValue) && stringValue != ""))
        {
            return new ValidationResult($"The language must be one of the following values: {string.Join(", ", AllowedValues)}.");
        }
        return ValidationResult.Success;
    }
}
