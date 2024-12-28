namespace RecipeService.API.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
public class NonEmptyListAttribute : ValidationAttribute
{
    public NonEmptyListAttribute() : base("The list cannot be empty.")
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is List<string> list)
        {
            return list.Count > 0;
        }
        return false;
    }
}


