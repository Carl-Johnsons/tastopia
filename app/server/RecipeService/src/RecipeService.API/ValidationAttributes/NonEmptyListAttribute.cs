namespace RecipeService.API.ValidationAttributes;
using System.Collections;
using System.ComponentModel.DataAnnotations;

public class NonEmptyListAttribute : ValidationAttribute
{
    public NonEmptyListAttribute() : base("The list cannot be empty.")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is ICollection collection)
        {
            return collection.Count > 0;
        }
        return false;
    }
}
