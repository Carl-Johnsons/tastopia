using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeData
{
    public static List<Guid> Authors = [
            Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
            Guid.Parse("078ecc42-7643-4cff-b851-eeac5ba1bb29"),
            Guid.Parse("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"),
            Guid.Parse("50e00c7f-39da-48d1-b273-3562225a5972"),
            Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
            Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
            Guid.Parse("03e4b46e-b84a-43a9-a421-1b19e02023bb"),
        ];

    public static List<Recipe> Recipes = [
        new Recipe{
            Id = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
            Title = "Classic Scrambled Eggs",
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
            Description = "A creamy, cheesy pasta with bacon, eggs, and parmesan.",
            ImageUrl = "https://i.imgur.com/le7gFC6.jpg",
            Serves = 2,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper"],
            CreatedAt = DateTime.Parse("2023-05-31 19:06:57"),
            UpdatedAt = DateTime.Parse("2023-05-31 19:06:57"),
        },
        new Recipe{
            Id = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
            Title = "Vegetable Stir-Fry",
            Description = "A healthy stir-fry with various vegetables and soy sauce.",
            ImageUrl = "https://i.imgur.com/aXVMMXA.jpg",
            Serves = 2,
            CookTime = "20m",
            IsActive = true,
            Ingredients = ["1 Bell Pepper", "1 Carrot", "1 Broccoli Head", "2 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove"],
            CreatedAt = DateTime.Parse("2023-02-19 06:28:09"),
            UpdatedAt = DateTime.Parse("2023-02-19 06:28:09"),
        },
        new Recipe{
            Id = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
            Title = "Garlic Bread",
            Description = "A simple and delicious garlic bread recipe, perfect as a side dish.",
            ImageUrl = "https://i.imgur.com/RpT3aRb.jpg",
            Serves = 2,
            CookTime = "15m",
            IsActive = true,
            Ingredients = ["1 Baguette", "50g Butter", "2 Garlic Cloves", "1 tbsp Parsley", "Salt"],
        },
        //20 recipe
        new Recipe{
            Id = Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
            Title = "Cheese Omelette",
            Description = "A simple and delicious cheese omelette.",
            ImageUrl = "https://www.emborg.com/app/uploads/2023/07/1200x900px_Easy_Cheese_Omelette.png",
            Serves = 2,
            CookTime = "5m",
            IsActive = true,
            Ingredients = ["2 Eggs", "50g Cheese", "Butter", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
            Title = "Mushroom Soup",
            Description = "A creamy mushroom soup with fresh mushrooms and spices.",
            ImageUrl = "https://www.mushroomcouncil.com/wp-content/uploads/2017/11/mushroom-soup-3-scaled.jpg",
            Serves = 4,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["200g Mushrooms", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
            Title = "Egg Fried Rice",
            Description = "A quick and tasty egg fried rice recipe.",
            ImageUrl = "https://www.mygingergarlickitchen.com/wp-content/rich-markup-images/4x3/4x3-indian-style-triple-egg-fried-rice-easy-egg-fried-rice-video-recipe.jpg",
            Serves = 2,
            CookTime = "20m",
            IsActive = true,
            Ingredients = ["1 cup Rice", "2 Eggs", "1 Carrot", "Soy Sauce", "2 tbsp Olive Oil", "Garlic"],
        },
        new Recipe{
            Id = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
            Title = "Broccoli and Cheese Casserole",
            Description = "A hearty casserole with broccoli and melted cheese.",
            ImageUrl = "https://www.homemadeinterest.com/wp-content/uploads/2022/09/Broccoli-Cheese-Casserole_-sq-1.jpg",
            Serves = 4,
            CookTime = "45m",
            IsActive = true,
            Ingredients = ["1 Broccoli Head", "200g Cheese", "1 Onion", "2 tbsp Butter", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
            Title = "Bacon and Eggs",
            Description = "A classic breakfast combination of bacon and eggs.",
            ImageUrl = "https://lowcarbinspirations.com/wp-content/uploads/2021/08/Classic-Bacon-and-Eggs-Recipe-3.jpg",
            Serves = 2,
            CookTime = "10m",
            IsActive = true,
            Ingredients = ["2 Eggs", "100g Bacon", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
            Title = "Garlic Butter Shrimp",
            Description = "Shrimp sautéed in a rich garlic butter sauce.",
            ImageUrl = "https://www.jocooks.com/wp-content/uploads/2021/09/garlic-butter-shrimp-1-10.jpg",
            Serves = 2,
            CookTime = "15m",
            IsActive = true,
            Ingredients = ["200g Shrimp", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper", "Parsley"],
        },
        new Recipe{
            Id = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
            Title = "Tomato Basil Soup",
            Description = "A fresh and fragrant tomato basil soup.",
            ImageUrl = "https://thecozyapron.com/wp-content/uploads/2012/02/tomato-basil-soup_thecozyapron_1.jpg",
            Serves = 2,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["4 Tomatoes", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Basil", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
            Title = "Carrot Soup",
            Description = "A warm and healthy carrot soup.",
            ImageUrl = "https://www.allrecipes.com/thmb/9_1D87q39_oirnZ9CXlmllrd9Fc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Simple-Carrot-Soup-2000-d2312230d4454f53baef3fa78f22e7d7.jpg",
            Serves = 4,
            CookTime = "40m",
            IsActive = true,
            Ingredients = ["4 Carrots", "1 Onion", "2 Garlic Cloves", "2 cups Vegetable Stock", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
            Title = "Rice Pilaf",
            Description = "A flavorful rice pilaf with spices and vegetables.",
            ImageUrl = "https://i2.wp.com/lifemadesimplebakes.com/wp-content/uploads/2021/02/perfect-rice-pilaf-square-1200.jpg",
            Serves = 4,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["1 cup Rice", "1 Onion", "2 Garlic Cloves", "1 Carrot", "1 tbsp Soy Sauce", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
            Title = "Cheese Stuffed Mushrooms",
            Description = "Mushrooms stuffed with cheese and baked to perfection.",
            ImageUrl = "https://www.inspiredtaste.net/wp-content/uploads/2018/09/Easy-Stuffed-Mushrooms-Recipe-1200.jpg",
            Serves = 2,
            CookTime = "25m",
            IsActive = true,
            Ingredients = ["200g Mushrooms", "100g Cheese", "2 Garlic Cloves", "1 tbsp Butter", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
            Title = "Bacon Carbonara",
            Description = "A creamy pasta carbonara with crispy bacon.",
            ImageUrl = "https://img.bestrecipes.com.au/_bHk3l7K/br/2016/07/asset-jpg-506607-1.jpg",
            Serves = 2,
            CookTime = "30m",
            IsActive = true,
            Ingredients = ["200g Spaghetti", "100g Bacon", "2 Eggs", "50g Parmesan Cheese", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
            Title = "Garlic Mashed Potatoes",
            Description = "Creamy mashed potatoes with roasted garlic.",
            ImageUrl = "https://www.allrecipes.com/thmb/ytnCq3jVoAyGzGxm_oZxqGI-HCU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/18290-garlic-mashed-potatoes-ddmfs-beauty2-4x3-0327-2-47384a10cded40ae90e574bc7fdb9433.jpg",
            Serves = 4,
            CookTime = "25m",
            IsActive = true,
            Ingredients = ["4 Potatoes", "2 Garlic Cloves", "50g Butter", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
            Title = "Spaghetti Bolognese",
            Description = "A hearty spaghetti bolognese with minced beef and tomato sauce.",
            ImageUrl = "https://cdn.stoneline.de/media/c5/63/4f/1727429313/spaghetti-bolognese.jpeg",
            Serves = 4,
            CookTime = "45m",
            IsActive = true,
            Ingredients = ["200g Spaghetti", "200g Minced Beef", "1 Onion", "2 Garlic Cloves", "4 Tomatoes", "Salt", "Pepper"],
        },
        new Recipe{
            Id = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
            Title = "Carrot and Broccoli Stir-Fry",
            Description = "A quick and healthy stir-fry with carrots and broccoli.",
            ImageUrl = "https://mojo.generalmills.com/api/public/content/PcgVKEIG0UCHstblaGsBYg_gmi_hi_res_jpeg.jpeg?v=d91eed52&t=466b54bb264e48b199fc8e83ef1136b4",
            Serves = 2,
            CookTime = "15m",
            IsActive = true,
            Ingredients = ["1 Carrot", "1 Broccoli Head", "Soy Sauce", "Olive Oil", "Salt", "Pepper"],
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
        },
        new Recipe{
            Id = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
            Title = "BBQ Chicken",
            Description = "Grilled chicken with a smoky BBQ sauce.",
            ImageUrl = "https://www.allrecipes.com/thmb/APtZNY1GgOf3Ph0JUc-j4dImjrU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/2467480-southern-bbq-chicken-Allrecipes-Magazine-4x3-1-3e180dccbaae446c8c2d05f708611fc6.jpg",
            Serves = 4,
            CookTime = "50m",
            IsActive = true,
            Ingredients = ["4 Chicken Breasts", "BBQ Sauce", "Salt", "Pepper"],
        }
    ];

    //public static List<Step> Step => [
    //    // For recipe Classic Scrambled Eggs
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
    //        Content = "Crack the eggs into a bowl and whisk with milk, salt, and pepper.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
    //        Content = "Melt butter in a non-stick pan over medium heat.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
    //        Content = "Pour the egg mixture into the pan and gently stir until softly set.",
    //        OdinalNumber = 3,
    //    },

    //    // For recipe Tomato Soup
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
    //        Content = "Chop the tomatoes, onion, and garlic.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
    //        Content = "Sauté onion and garlic in olive oil until soft.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
    //        Content = "Add tomatoes and vegetable stock, then simmer for 30 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
    //        Content = "Blend the soup until smooth and season with salt and pepper.",
    //        OdinalNumber = 4,
    //    },

    //    // For recipe Pasta Carbonara
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
    //        Content = "Cook spaghetti in salted boiling water until al dente.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
    //        Content = "Fry bacon until crispy.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
    //        Content = "Mix eggs and grated parmesan in a bowl.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
    //        Content = "Toss cooked spaghetti with bacon and remove from heat.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
    //        Content = "Add the egg mixture and stir until creamy.",
    //        OdinalNumber = 5,
    //    },

    //    // For recipe Vegetable Stir-Fry
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
    //        Content = "Chop all vegetables into bite-sized pieces.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
    //        Content = "Heat olive oil in a wok or large pan.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
    //        Content = "Add vegetables and stir-fry for 5-7 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
    //        Content = "Add soy sauce and stir well.",
    //        OdinalNumber = 4,
    //    },

    //    // For recipe Garlic Bread
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
    //        Content = "Preheat the oven to 180°C (350°F).",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
    //        Content = "Mix softened butter with minced garlic and parsley.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
    //        Content = "Spread the mixture onto sliced baguette.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
    //        Content = "Bake in the oven for 10-12 minutes until golden.",
    //        OdinalNumber = 4,
    //    },
    //    // Steps for Cheese Omelette (Id: 9e9b3a16-42f1-40a3-9f60-e704e632b609)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
    //        Content = "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
    //        Content = "Melt butter in a pan over medium heat.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
    //        Content = "Pour the egg mixture into the pan and cook until set, gently folding the edges.",
    //        OdinalNumber = 3,
    //    },

    //    // Steps for Mushroom Soup (Id: d1672c31-64cc-44b5-9630-2e7f9f651234)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
    //        Content = "Chop the mushrooms, onion, and garlic.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
    //        Content = "Sauté the onions and garlic in a pot with some oil until soft.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
    //        Content = "Add the chopped mushrooms and cook until they release their juices.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
    //        Content = "Pour in the vegetable stock and let the soup simmer for 20 minutes.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
    //        Content = "Season with salt and pepper, then blend the soup until smooth.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Egg Fried Rice (Id: 8e607e5c-8dbf-455b-9f5b-9c56e2d79a63)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
    //        Content = "Cook rice according to package instructions.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
    //        Content = "Scramble the eggs in a pan with some oil or butter.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
    //        Content = "Add chopped carrots and garlic to the pan, and cook until soft.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
    //        Content = "Add the cooked rice to the pan and stir-fry with soy sauce.",
    //        OdinalNumber = 4,
    //    },

    //    // Steps for Broccoli and Cheese Casserole (Id: 2d6f3e6c-4b75-4759-92a4-f22c5ca20742)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
    //        Content = "Preheat the oven to 180°C (350°F).",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
    //        Content = "Steam the broccoli until tender.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
    //        Content = "Sauté the onion in butter until soft.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
    //        Content = "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("2d6f3e6c-4b75-4759-92a4-f22c5ca20742"),
    //        Content = "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Bacon and Eggs (Id: e6f1ed85-1046-4fdb-9b85-35c6d2d874cf)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
    //        Content = "Fry the bacon in a pan until crispy.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
    //        Content = "Crack the eggs into the pan and cook to your desired doneness.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("e6f1ed85-1046-4fdb-9b85-35c6d2d874cf"),
    //        Content = "Season the eggs with salt and pepper, then serve with the crispy bacon.",
    //        OdinalNumber = 3,
    //    },

    //    // Steps for Garlic Butter Shrimp (Id: 5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
    //        Content = "Peel and devein the shrimp.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
    //        Content = "Melt butter in a pan and sauté garlic until fragrant.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
    //        Content = "Add the shrimp to the pan and cook until pink and opaque.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5b0d4c7a-b6cc-4fd3-9098-8d19f13c43f1"),
    //        Content = "Season with salt, pepper, and parsley before serving.",
    //        OdinalNumber = 4,
    //    },
    //    // Steps for Tomato Basil Soup (Id: 4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
    //        Content = "Chop the tomatoes, onion, and garlic.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
    //        Content = "Sauté the onion and garlic in a pot with some oil until soft.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
    //        Content = "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
    //        Content = "Season with salt, pepper, and fresh basil.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("4bdfcf88-f3c6-42ca-9bcf-3797fc83f2cf"),
    //        Content = "Blend the soup until smooth, then serve hot.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Carrot Soup (Id: 1f337cae-879e-4d85-9eb9-658b18f5beff)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
    //        Content = "Peel and chop the carrots, onion, and garlic.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
    //        Content = "Sauté the onion and garlic in a pot with some oil until soft.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
    //        Content = "Add chopped carrots and vegetable stock, and simmer for 30 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
    //        Content = "Season with salt and pepper to taste.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("1f337cae-879e-4d85-9eb9-658b18f5beff"),
    //        Content = "Blend the soup until smooth and serve hot.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Rice Pilaf (Id: a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
    //        Content = "Chop the onion, garlic, and carrot.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
    //        Content = "Sauté the onion, garlic, and carrot in a pot with some oil until soft.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
    //        Content = "Add the rice and soy sauce, then cook for 1-2 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
    //        Content = "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("a9085f28-cb56-48c4-94cf-8a0c4f3ff8bb"),
    //        Content = "Cook for 20 minutes, or until rice is tender and water is absorbed.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Cheese Stuffed Mushrooms (Id: f3d6b0a3-89cc-4c98-b254-0d24912bfc7a)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Preheat the oven to 180°C (350°F).",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Remove the stems from the mushrooms and chop them finely.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Sauté the chopped mushroom stems and garlic in butter until soft.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Mix the sautéed mushrooms with cheese and season with salt and pepper.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.",
    //        OdinalNumber = 5,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f3d6b0a3-89cc-4c98-b254-0d24912bfc7a"),
    //        Content = "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.",
    //        OdinalNumber = 6,
    //    },

    //    // Steps for Bacon Carbonara (Id: 5c2c8d61-4474-4933-9f70-f016e45e5f1d)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "Cook the spaghetti according to the package instructions.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "Fry the bacon in a pan until crispy, then remove and chop.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "In a bowl, whisk together the eggs and Parmesan cheese.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "Drain the cooked spaghetti and toss with the crispy bacon.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "Pour the egg and cheese mixture over the pasta and toss quickly to coat.",
    //        OdinalNumber = 5,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("5c2c8d61-4474-4933-9f70-f016e45e5f1d"),
    //        Content = "Season with salt and pepper to taste, and serve immediately.",
    //        OdinalNumber = 6,
    //    },
    //    // Steps for Garlic Mashed Potatoes (Id: f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
    //        Content = "Peel and chop the potatoes into chunks.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
    //        Content = "Roast the garlic cloves in the oven for 10 minutes.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
    //        Content = "Boil the potatoes in salted water until tender, about 15 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
    //        Content = "Mash the potatoes with butter, roasted garlic, salt, and pepper.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f4b4b8f3-3b7a-4f6d-96cf-3815d5d91f97"),
    //        Content = "Serve hot.",
    //        OdinalNumber = 5,
    //    },

    //    // Steps for Spaghetti Bolognese (Id: bb837d5d-3ac2-4c92-a01c-8c67c7d79e11)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Chop the onion, garlic, and tomatoes.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Cook the minced beef in a pan until browned.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Sauté the onion and garlic in a separate pan until softened.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.",
    //        OdinalNumber = 5,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Cook the spaghetti according to package instructions and drain.",
    //        OdinalNumber = 6,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("bb837d5d-3ac2-4c92-a01c-8c67c7d79e11"),
    //        Content = "Serve the Bolognese sauce over the cooked spaghetti.",
    //        OdinalNumber = 7,
    //    },

    //    // Steps for Carrot and Broccoli Stir-Fry (Id: f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Chop the carrot and broccoli into bite-sized pieces.",
    //        OdinalNumber = 1,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Heat the olive oil in a pan over medium-high heat.",
    //        OdinalNumber = 2,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Add the carrots and stir-fry for 3-4 minutes.",
    //        OdinalNumber = 3,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Add the broccoli and continue stir-frying for another 5 minutes.",
    //        OdinalNumber = 4,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Season with soy sauce, salt, and pepper.",
    //        OdinalNumber = 5,
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("f676ac67-84be-4d6f-bbd1-e1b51cc4f4bb"),
    //        Content = "Serve immediately.",
    //        OdinalNumber = 6,
    //    },

    //    // Steps for Milkshake (Id: 3e7ff177-b9d9-4789-b1b2-bce1c1b7955e)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
    //        Content = "Add milk, ice cream, and sugar to a blender.",
    //        OdinalNumber = 1,
    //        AttachedImageUrls = new List<string>{
    //            "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-02.jpg"
    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
    //        Content = "Blend until smooth.",
    //        OdinalNumber = 2,
    //        AttachedImageUrls = new List<string>{
    //            "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-03.jpg"
    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("3e7ff177-b9d9-4789-b1b2-bce1c1b7955e"),
    //        Content = "Serve immediately.",
    //        OdinalNumber = 3,
    //        AttachedImageUrls = new List<string>{
    //            "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-04.jpg"
    //        }
    //    },
    //    // Steps for BBQ Chicken (Id: d2189f90-6991-4901-8195-f0c12d24d900)
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
    //        Content = "Preheat the grill to medium-high heat.",
    //        OdinalNumber = 1,
    //        AttachedImageUrls = new List<string>{
    //            "https://www.eatingwell.com/thmb/uIS7xz8ZcT6WLalfYHxEvpLJF9Y=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Hot-Grill-98dc8b55c76b427b9a026cf509ec7c48.jpg",
    //            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXHzHjU_DYlhDHtkb3zr_f28KMRHIH-7Lgkg&s",
    //            "https://blog.zgrills.com/wp-content/uploads/2022/09/what-temperature-is-medium-heat-on-a-grill.jpg"
    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
    //        Content = "Season the chicken breasts with salt and pepper.",
    //        OdinalNumber = 2,
    //        AttachedImageUrls = new List<string>{
    //            "https://cdn.apartmenttherapy.info/image/upload/f_auto,q_auto:eco,c_fill,g_auto,w_1690,h_1128/k%2Farchive%2Fa0d25ce80ff3b94487b4df0f5bd83fb943b7d0b2",
    //            "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/baked-skin-on-chicken-breast-ingredients.jpg",
    //            "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/salt-and-pepper.jpg"

    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
    //        Content = "Grill the chicken for 6-7 minutes on each side.",
    //        OdinalNumber = 3,
    //        AttachedImageUrls = new List<string>{
    //            "https://s3.festivalfoods.net/blog/uploads/2022/05/IMG_0864.jpeg",
    //            "https://cdn.shopify.com/s/files/1/0271/5287/5653/files/20240512131902-holychicken.jpg?v=1715519944&width=1600&height=900"
    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
    //        Content = "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.",
    //        OdinalNumber = 4,
    //        AttachedImageUrls = new List<string>{
    //            "https://i2.wp.com/www.downshiftology.com/wp-content/uploads/2022/06/BBQ-Chicken-11.jpg"
    //        }
    //    },
    //    new Step{
    //        Id = Guid.NewGuid(),
    //        RecipeId = Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900"),
    //        Content = "Serve with additional BBQ sauce on the side.",
    //        OdinalNumber = 5,
    //        AttachedImageUrls = new List<string>{
    //            "https://thewholeserving.com/wp-content/uploads/2024/04/Platter-of-prepared-drumsticks-with-a-glass-container-of-extra-barbecue-sauce-on-the-right-side-of-the-platter.jpg"
    //        }
    //    },
    //];
}