using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataThinh
{
    public static List<Recipe> Recipes = [
        new Recipe{
            Id = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Title = "Classic Scrambled Eggs",
            Description = "A quick and easy recipe for creamy scrambled eggs, perfect for breakfast.",
            ImageUrl = "https://i.imgur.com/rxAzMjR.jpg",
            Serves = 2,
            CookTime = "10m",
            Ingredients = ["2 Eggs", "2 tbsp Milk", "1 tbsp Butter", "Salt", "Pepper"],
            Steps = [
                new Step{ Content = "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", OrdinalNumber = 1 },
                new Step{ Content = "Melt butter in a non-stick pan over medium heat.", OrdinalNumber = 2 },
                new Step{ Content = "Pour the egg mixture into the pan and gently stir until softly set.", OrdinalNumber = 3 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
            Title = "Milkshake",
            Description = "A creamy and refreshing milkshake.",
            ImageUrl = "https://www.allrecipes.com/thmb/uzxCGTc-5WCUZnZ7BUcYcmWKxjo=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/AR-48974-vanilla-milkshake-hero-4x3-c815295c714f41f6b17b104e7403a53b.jpg",
            Serves = 2,
            CookTime = "5m",
            IsActive = true,
            Ingredients = ["2 cups Milk", "100g Ice Cream", "1 tbsp Sugar"],
            Steps = [
                new Step{
                    Content = "Add milk, ice cream, and sugar to a blender.",
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-02.jpg"
                    }
                },
                new Step{
                    Content = "Blend until smooth.",
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-03.jpg"
                    }
                },
                new Step{
                    Content = "Serve immediately.",
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-04.jpg"
                    }
                },
            ],
        },
    ];
}
