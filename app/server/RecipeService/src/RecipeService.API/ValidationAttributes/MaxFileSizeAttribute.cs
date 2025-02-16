using System.ComponentModel.DataAnnotations;
namespace RecipeService.API.ValidationAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxSizeInBytes;

    public MaxFileSizeAttribute(long maxSizeInMB)
    {
        _maxSizeInBytes = maxSizeInMB * 1024 * 1024;
        ErrorMessage = $"File size must not exceed {maxSizeInMB} MB.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null) {
            if (value is IFormFile file)
            {
                if (file.Length > _maxSizeInBytes)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success!;
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class MaxFileSizeListAttribute : ValidationAttribute
{
    private readonly long _maxSizeInBytes;

    public MaxFileSizeListAttribute(long maxSizeInMB)
    {
        _maxSizeInBytes = maxSizeInMB * 1024 * 1024;
        ErrorMessage = $"Each file must not exceed {maxSizeInMB} MB.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is List<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > _maxSizeInBytes)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
        return ValidationResult.Success!;
    }
}
