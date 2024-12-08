using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var recipe1Id = Guid.NewGuid();
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = recipe1Id,
                Title = "Classic Scrambled Eggs",
                AuthorId = Guid.NewGuid(),
                Description = "A quick and easy recipe for creamy scrambled eggs, perfect for breakfast.",
                ImageUrl = "https://www.svgrepo.com/show/84264/recipes.svg",
                Serves = 2,
                CookTime = new TimeSpan(0, 10, 0)
            }
        );

        

        modelBuilder.Entity<Step>().HasData(
            new Step { RecipeId = recipe1Id, Content = "Crack eggs into a bowl, add salt and pepper, and whisk well.", OdinalNumber = 1 },
            new Step { RecipeId = recipe1Id, Content = "Melt butter in a pan over medium heat.", OdinalNumber = 2 },
            new Step { RecipeId = recipe1Id, Content = "Pour the eggs into the pan and gently stir until cooked to your liking.", OdinalNumber = 3 }
        );

        var recipe2Id = Guid.NewGuid();
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = recipe2Id,
                Title = "Tomato Soup",
                AuthorId = Guid.NewGuid(),
                Description = "A comforting tomato soup made from fresh tomatoes and spices.",
                ImageUrl = "https://www.svgrepo.com/show/84264/recipes.svg",
                Serves = 4,
                CookTime = new TimeSpan(0, 30, 0)
            }
        );

        

        modelBuilder.Entity<Step>().HasData(
            new Step { RecipeId = recipe2Id, Content = "Heat olive oil in a pot, sauté onions and garlic until soft.", OdinalNumber = 1 },
            new Step { RecipeId = recipe2Id, Content = "Add chopped tomatoes and cook for 10 minutes.", OdinalNumber = 2 },
            new Step { RecipeId = recipe2Id, Content = "Blend the mixture until smooth and simmer for another 10 minutes.", OdinalNumber = 3 }
        );

        var recipe3Id = Guid.NewGuid();
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = recipe3Id,
                Title = "Vegetable Stir-Fry",
                AuthorId = Guid.NewGuid(),
                Description = "A healthy stir-fry with various vegetables and soy sauce.",
                ImageUrl = "https://www.svgrepo.com/show/84264/recipes.svg",
                Serves = 3,
                CookTime = new TimeSpan(0, 20, 0)
            }
        );


        modelBuilder.Entity<Step>().HasData(
            new Step { RecipeId = recipe3Id, Content = "Heat oil in a pan, add sliced carrots, broccoli, and bell peppers.", OdinalNumber = 1 },
            new Step { RecipeId = recipe3Id, Content = "Stir-fry for 5-7 minutes, then add soy sauce and cook for another 3 minutes.", OdinalNumber = 2 },
            new Step { RecipeId = recipe3Id, Content = "Serve hot with steamed rice.", OdinalNumber = 3 }
        );

        var recipe4Id = Guid.NewGuid();
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = recipe4Id,
                Title = "Garlic Bread",
                AuthorId = Guid.NewGuid(),
                Description = "A simple and delicious garlic bread recipe, perfect as a side dish.",
                ImageUrl = "https://www.svgrepo.com/show/84264/recipes.svg",
                Serves = 4,
                CookTime = new TimeSpan(0, 15, 0)
            }
        );

        
        modelBuilder.Entity<Step>().HasData(
            new Step { RecipeId = recipe4Id, Content = "Preheat the oven to 375°F (190°C).", OdinalNumber = 1 },
            new Step { RecipeId = recipe4Id, Content = "Spread garlic butter on bread slices and arrange on a baking sheet.", OdinalNumber = 2 },
            new Step { RecipeId = recipe4Id, Content = "Bake for 10 minutes or until golden and crispy.", OdinalNumber = 3 }
        );

        var recipe5Id = Guid.NewGuid();
        modelBuilder.Entity<Recipe>().HasData(
            new Recipe
            {
                Id = recipe5Id,
                Title = "Pasta Carbonara",
                AuthorId = Guid.NewGuid(),
                Description = "A creamy, cheesy pasta with bacon, eggs, and parmesan.",
                ImageUrl = "https://www.svgrepo.com/show/84264/recipes.svg",
                Serves = 4,
                CookTime = new TimeSpan(0, 20, 0)
            }
        );

        modelBuilder.Entity<Step>().HasData(
            new Step { RecipeId = recipe5Id, Content = "Cook spaghetti according to package instructions.", OdinalNumber = 1 },
            new Step { RecipeId = recipe5Id, Content = "Fry bacon until crispy, then chop into pieces.", OdinalNumber = 2 },
            new Step { RecipeId = recipe5Id, Content = "Toss spaghetti with bacon, add beaten eggs and parmesan cheese, and mix until creamy.", OdinalNumber = 3 },
            new Step { RecipeId = recipe5Id, Content = "Serve hot with extra parmesan on top.", OdinalNumber = 4 }
        );
    }

    
}
