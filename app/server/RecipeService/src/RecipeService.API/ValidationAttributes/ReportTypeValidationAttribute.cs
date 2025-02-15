namespace RecipeService.API.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

public class ReportTypeValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedValues = { "Recipe", "Comment"};

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && (!AllowedValues.Contains(stringValue) && stringValue != ""))
        {
            return new ValidationResult($"The reportType must be one of the following values: {string.Join(", ", AllowedValues)}.");
        }
        return ValidationResult.Success;
    }
}
