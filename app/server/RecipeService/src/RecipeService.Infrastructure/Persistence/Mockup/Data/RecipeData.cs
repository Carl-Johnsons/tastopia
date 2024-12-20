using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeData
{
    public static List<Recipe> Recipe => [
        new Recipe{
            Id = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Title = "Classic Scrambled Eggs",
            AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Description = "A quick and easy recipe for creamy scrambled eggs, perfect for breakfast.",
            ImageUrl = "https://i.imgur.com/rxAzMjR.jpg",
            Serves = 2,
            CookTime = "10m",
            IsActive = true,
            Ingredients = ["2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
            Title = "Tomato Soup",
            AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Description = "A comforting tomato soup made from fresh tomatoes and spices.",
            ImageUrl = "https://i.imgur.com/SzhMVWs.jpg",
            Serves = 2,
            CookTime = "40m",
            IsActive = true,
            Ingredients = ["4 Ripe Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Title = "Pasta Carbonara",
            AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Description = "A creamy, cheesy pasta with bacon, eggs, and parmesan.",
            ImageUrl = "https://i.imgur.com/le7gFC6.jpg",
            Serves = 2,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Title = "Vegetable Stir-Fry",
            AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Description = "A healthy stir-fry with various vegetables and soy sauce.",
            ImageUrl = "https://i.imgur.com/aXVMMXA.jpg",
            Serves = 2,
            CookTime = "20m",
            IsActive = true,
            Ingredients = ["1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove"],
        },
        new Recipe{
            Id = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Title = "Garlic Bread",
            AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Description = "A simple and delicious garlic bread recipe, perfect as a side dish.",
            ImageUrl = "https://i.imgur.com/RpT3aRb.jpg",
            Serves = 2,
            CookTime = "15m",
            IsActive = true,
            Ingredients = ["1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt"],
        },
    ];

    public static List<Step> Step => [
        // For recipe Classic Scrambled Eggs
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Content = "Crack the eggs into a bowl and whisk with milk, salt, and pepper.",
            OdinalNumber = 1,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Content = "Melt butter in a non-stick pan over medium heat.",
            OdinalNumber = 2,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Content = "Pour the egg mixture into the pan and gently stir until softly set.",
            OdinalNumber = 3,
        },

        // For recipe Tomato Soup
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
            Content = "Chop the tomatoes, onion, and garlic.",
            OdinalNumber = 1,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
            Content = "Sauté onion and garlic in olive oil until soft.",
            OdinalNumber = 2,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
            Content = "Add tomatoes and vegetable stock, then simmer for 30 minutes.",
            OdinalNumber = 3,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
            Content = "Blend the soup until smooth and season with salt and pepper.",
            OdinalNumber = 4,
        },

        // For recipe Pasta Carbonara
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Content = "Cook spaghetti in salted boiling water until al dente.",
            OdinalNumber = 1,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Content = "Fry bacon until crispy.",
            OdinalNumber = 2,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Content = "Mix eggs and grated parmesan in a bowl.",
            OdinalNumber = 3,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Content = "Toss cooked spaghetti with bacon and remove from heat.",
            OdinalNumber = 4,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
            Content = "Add the egg mixture and stir until creamy.",
            OdinalNumber = 5,
        },

        // For recipe Vegetable Stir-Fry
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Content = "Chop all vegetables into bite-sized pieces.",
            OdinalNumber = 1,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Content = "Heat olive oil in a wok or large pan.",
            OdinalNumber = 2,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Content = "Add vegetables and stir-fry for 5-7 minutes.",
            OdinalNumber = 3,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Content = "Add soy sauce and stir well.",
            OdinalNumber = 4,
        },

        // For recipe Garlic Bread
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Content = "Preheat the oven to 180°C (350°F).",
            OdinalNumber = 1,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Content = "Mix softened butter with minced garlic and parsley.",
            OdinalNumber = 2,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Content = "Spread the mixture onto sliced baguette.",
            OdinalNumber = 3,
        },
        new Step{
            Id = Guid.NewGuid(),
            RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Content = "Bake in the oven for 10-12 minutes until golden.",
            OdinalNumber = 4,
        },
    ];
}