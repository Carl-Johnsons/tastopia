using System.Collections;
using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.ValidationAttributes;
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MaxLengthListAttribute : ValidationAttribute
{
    private readonly int _maxLength;

    public MaxLengthListAttribute(int maxLength)
    {
        _maxLength = maxLength;
        ErrorMessage = $"The list must have at most {_maxLength} items.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            if (value is IList list)
            {
                if (list.Count > _maxLength)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success!;
    }
}