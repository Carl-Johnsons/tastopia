namespace RecipeService.API.ValidationAttributes;

using System.ComponentModel.DataAnnotations;

public class TagStatusValidationAttribute : ValidationAttribute
{
    private static readonly string[] AllowedValues = { "Pending", "Active", "Inactive" };

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string stringValue && (!AllowedValues.Contains(stringValue) && stringValue != ""))
        {
            return new ValidationResult($"The tag status must be one of the following values: {string.Join(", ", AllowedValues)}.");
        }

        return ValidationResult.Success;
    }
}
