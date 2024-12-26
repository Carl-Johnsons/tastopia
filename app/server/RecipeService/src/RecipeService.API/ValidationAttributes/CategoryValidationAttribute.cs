namespace RecipeService.API.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

public class CategoryValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedValues = { "DISHTYPE", "INGREDIENT" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && (!AllowedValues.Contains(stringValue.ToUpper()) && stringValue != ""))
        {
            return new ValidationResult($"The category must be one of the following values: {string.Join(", ", AllowedValues)}.");
        }

        return ValidationResult.Success;
    }
}
