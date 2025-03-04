using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataThinh
{
    public static List<Recipe> Recipes = [
        new Recipe{
            Id = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"),
            Title = "Stir-Fried Chicken with Bitter Melon",
            Description = "A delicious and slightly bitter stir-fried chicken with bitter melon, a classic Vietnamese dish.",
            ImageUrl = "https://amiablefoods.com/wp-content/uploads/chicken-ampalaya-810-x-810-700x700.jpg",
            Serves = 3,
            CookTime = "25m",
            Ingredients = [
                "300g Chicken (sliced)",
                "1 Bitter Melon (sliced thinly)",
                "1 Shallot (minced)",
                "2 cloves Garlic (minced)",
                "1 tbsp Fish Sauce",
                "1/2 tsp Sugar",
                "1/2 tsp Salt",
                "1/2 tsp Black Pepper",
                "1 tbsp Vegetable Oil"
            ],
            Steps = [
                new Step{ Content = "Slice the bitter melon thinly and soak in salted water for 10 minutes, then drain.", OrdinalNumber = 1 },
                new Step{ Content = "Heat oil in a pan and sauté garlic and shallot until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add the chicken slices and stir-fry until cooked through.", OrdinalNumber = 3 },
                new Step{ Content = "Add the bitter melon and stir-fry for another 3-5 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Season with fish sauce, sugar, salt, and black pepper. Stir well.", OrdinalNumber = 5 },
                new Step{ Content = "Serve hot with steamed rice.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"),
            Title = "Chicken Soup with Bamboo Shoots",
            Description = "A hearty and aromatic chicken soup with bamboo shoots, perfect for a comforting meal.",
            ImageUrl = "https://iscleecam.edu.vn/wp-content/uploads/2024/04/Vietnamese-Bamboo-Shoot-Soup-With-Chicken-2-800x600.jpg",
            Serves = 4,
            CookTime = "40m",
            Ingredients = [
                "400g Chicken (bone-in, cut into pieces)",
                "200g Bamboo Shoots (sliced)",
                "1 Shallot (minced)",
                "2 cloves Garlic (minced)",
                "1-inch Ginger (sliced)",
                "1 tsp Salt",
                "1/2 tsp Black Pepper",
                "1 tbsp Fish Sauce",
                "1 liter Water",
                "2 tbsp Vegetable Oil",
                "Chopped Green Onions (for garnish)"
            ],
            Steps = [
                new Step{ Content = "Boil the chicken in water for 5 minutes, then discard the water and rinse the chicken.", OrdinalNumber = 1 },
                new Step{ Content = "Heat oil in a pot and sauté shallot, garlic, and ginger until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add the chicken and stir-fry for a few minutes until slightly browned.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in water and bring to a boil, then reduce to a simmer.", OrdinalNumber = 4 },
                new Step{ Content = "Add bamboo shoots and season with salt, pepper, and fish sauce.", OrdinalNumber = 5 },
                new Step{ Content = "Simmer for 20-25 minutes until chicken is tender.", OrdinalNumber = 6 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 7 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("34f37c02-feb4-4189-b56f-50158aab1c81"),
            Title = "Grilled Lemongrass Chicken",
            Description = "A flavorful grilled chicken marinated with lemongrass, garlic, and fish sauce.",
            ImageUrl = "https://www.allrecipes.com/thmb/oOXQt20gForU70bXy-bQKeUlL0k=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/6827213-vietnamese-grilled-lemongrass-chicken-AllrecipesPhoto-1x1-1-36e69cd06cd646e3b380675e7f0c5a43.jpg",
            Serves = 4,
            CookTime = "45m",
            Ingredients = [
                "500g Chicken Thighs (boneless, skin-on)",
                "2 stalks Lemongrass (minced)",
                "2 cloves Garlic (minced)",
                "1 Shallot (minced)",
                "1 tbsp Fish Sauce",
                "1/2 tsp Black Pepper",
                "1 tsp Sugar",
                "1 tbsp Vegetable Oil"
            ],
            Steps = [
                new Step{ Content = "In a bowl, mix lemongrass, garlic, shallot, fish sauce, black pepper, sugar, and oil.", OrdinalNumber = 1 },
                new Step{ Content = "Marinate chicken with the mixture for at least 30 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Preheat grill or pan over medium heat.", OrdinalNumber = 3 },
                new Step{ Content = "Grill the chicken for 5-7 minutes per side until fully cooked.", OrdinalNumber = 4 },
                new Step{ Content = "Serve hot with steamed rice or fresh vegetables.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("e47f7b1d-8382-4578-9c6f-ce771f709c27"),
            Title = "Stewed Silkie Chicken with Chinese Herbs",
            Description = "A nutritious and flavorful stewed Silkie chicken with traditional Chinese herbs.",
            ImageUrl = "https://www.shutterstock.com/image-photo/delicoius-silky-chicken-blackboned-soup-600nw-2230647447.jpg",
            Serves = 4,
            CookTime = "90m",
            Ingredients = [
                "1 Silkie Chicken (whole, cleaned and cut into pieces)",
                "2 Shallots (minced)",
                "3 cloves Garlic (minced)",
                "1-inch Ginger (sliced)",
                "1 tbsp Fish Sauce",
                "1/2 tsp Black Pepper",
                "1 tsp Salt",
                "1 liter Water",
                "2 tbsp Vegetable Oil"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pot and sauté shallots, garlic, and ginger until fragrant.", OrdinalNumber = 1 },
                new Step{ Content = "Add Silkie chicken and stir-fry for a few minutes until slightly browned.", OrdinalNumber = 2 },
                new Step{ Content = "Pour in water and bring to a boil, then reduce to a simmer.", OrdinalNumber = 3 },
                new Step{ Content = "Season with fish sauce, salt, and black pepper.", OrdinalNumber = 4 },
                new Step{ Content = "Simmer for 60-90 minutes until the chicken is tender and the broth is flavorful.", OrdinalNumber = 5 },
                new Step{ Content = "Serve hot with steamed rice.", OrdinalNumber = 6 }
            ]
        },
    ];
}
