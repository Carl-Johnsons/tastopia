
using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class IngredientData
{
    public static List<Ingredient> Data = new List<Ingredient>
    {
        new Ingredient { Id = Guid.NewGuid(), Name = "Chicken Egg", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Butter", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Salt", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Black Pepper", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Tomato", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Onion", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Garlic", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Olive Oil", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Carrot", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Broccoli", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Bell Pepper", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Bread", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Parmesan Cheese", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Spaghetti", ImageUrl = "url" },
        new Ingredient { Id = Guid.NewGuid(), Name = "Bacon", ImageUrl = "url" }
    };
}
