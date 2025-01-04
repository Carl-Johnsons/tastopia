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
            Steps = [
                new Step{ Content = "Crack the eggs into a bowl and whisk with milk, salt, and pepper.", OrdinalNumber = 1 },
                new Step{ Content = "Melt butter in a non-stick pan over medium heat.", OrdinalNumber = 2 },
                new Step{ Content = "Pour the egg mixture into the pan and gently stir until softly set.", OrdinalNumber = 3 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the tomatoes, onion, and garlic.", OrdinalNumber = 1 },
                new Step{ Content = "Sauté onion and garlic in olive oil until soft.", OrdinalNumber = 2 },
                new Step{ Content = "Add tomatoes and vegetable stock, then simmer for 30 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Blend the soup until smooth and season with salt and pepper.", OrdinalNumber = 4 }
            ]
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
            Steps = [
                new Step{ Content = "Cook spaghetti in salted boiling water until al dente.", OrdinalNumber = 1 },
                new Step{ Content = "Fry bacon until crispy.", OrdinalNumber = 2 },
                new Step{ Content = "Mix eggs and grated parmesan in a bowl.", OrdinalNumber = 3 },
                new Step{ Content = "Toss cooked spaghetti with bacon and remove from heat.", OrdinalNumber = 4 },
                new Step{ Content = "Add the egg mixture and stir until creamy.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Chop all vegetables into bite-sized pieces.", OrdinalNumber = 1 },
                new Step{ Content = "Heat olive oil in a wok or large pan.", OrdinalNumber = 2 },
                new Step{ Content = "Add vegetables and stir-fry for 5-7 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Add soy sauce and stir well.", OrdinalNumber = 4 }
            ]
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
            Steps = [
                new Step{ Content = "Preheat the oven to 180°C (350°F).", OrdinalNumber = 1 },
                new Step{ Content = "Mix softened butter with minced garlic and parsley.", OrdinalNumber = 2 },
                new Step{ Content = "Spread the mixture onto sliced baguette.", OrdinalNumber = 3 },
                new Step{ Content = "Bake in the oven for 10-12 minutes until golden.", OrdinalNumber = 4 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
            Title = "Cheese Omelette",
            Description = "A simple and delicious cheese omelette.",
            ImageUrl = "https://www.emborg.com/app/uploads/2023/07/1200x900px_Easy_Cheese_Omelette.png",
            Serves = 2,
            CookTime = "5m",
            IsActive = true,
            Ingredients = ["2 Eggs", "50g Cheese", "Butter", "Salt", "Pepper"],
            Steps = [
                new Step{ Content = "Crack the eggs into a bowl and whisk with salt, pepper, and cheese.", OrdinalNumber = 1 },
                new Step{ Content = "Melt butter in a pan over medium heat.", OrdinalNumber = 2 },
                new Step{ Content = "Pour the egg mixture into the pan and cook until set, gently folding the edges.", OrdinalNumber = 3 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the mushrooms, onion, and garlic.", OrdinalNumber = 1 },
                new Step{ Content = "Sauté the onions and garlic in a pot with some oil until soft.", OrdinalNumber = 2 },
                new Step{ Content = "Add the chopped mushrooms and cook until they release their juices.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in the vegetable stock and let the soup simmer for 20 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Season with salt and pepper, then blend the soup until smooth.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Cook rice according to package instructions.", OrdinalNumber = 1 },
                new Step{ Content = "Scramble the eggs in a pan with some oil or butter.", OrdinalNumber = 2 },
                new Step{ Content = "Add chopped carrots and garlic to the pan, and cook until soft.", OrdinalNumber = 3 },
                new Step{ Content = "Add the cooked rice to the pan and stir-fry with soy sauce.", OrdinalNumber = 4 }
            ]
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
            Steps = [
                new Step{ Content = "Preheat the oven to 180°C (350°F).", OrdinalNumber = 1 },
                new Step{ Content = "Steam the broccoli until tender.", OrdinalNumber = 2 },
                new Step{ Content = "Sauté the onion in butter until soft.", OrdinalNumber = 3 },
                new Step{ Content = "Combine steamed broccoli, sautéed onion, and cheese in a casserole dish.", OrdinalNumber = 4 },
                new Step{ Content = "Bake in the oven for 20-25 minutes, until cheese is melted and bubbly.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Fry the bacon in a pan until crispy.", OrdinalNumber = 1 },
                new Step{ Content = "Crack the eggs into the pan and cook to your desired doneness.", OrdinalNumber = 2 },
                new Step{ Content = "Season the eggs with salt and pepper, then serve with the crispy bacon.", OrdinalNumber = 3 }
            ]
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
            Steps = [
                new Step{ Content = "Peel and devein the shrimp.", OrdinalNumber = 1 },
                new Step{ Content = "Melt butter in a pan and sauté garlic until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add the shrimp to the pan and cook until pink and opaque.", OrdinalNumber = 3 },
                new Step{ Content = "Season with salt, pepper, and parsley before serving.", OrdinalNumber = 4 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the tomatoes, onion, and garlic.", OrdinalNumber = 1 },
                new Step{ Content = "Sauté the onion and garlic in a pot with some oil until soft.", OrdinalNumber = 2 },
                new Step{ Content = "Add chopped tomatoes and vegetable stock to the pot and simmer for 20 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Season with salt, pepper, and fresh basil.", OrdinalNumber = 4 },
                new Step{ Content = "Blend the soup until smooth, then serve hot.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Peel and chop the carrots, onion, and garlic.", OrdinalNumber = 1 },
                new Step{ Content = "Sauté the onion and garlic in a pot with some oil until soft.", OrdinalNumber = 2 },
                new Step{ Content = "Add chopped carrots and vegetable stock, and simmer for 30 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Season with salt and pepper to taste.", OrdinalNumber = 4 },
                new Step{ Content = "Blend the soup until smooth and serve hot.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the onion, garlic, and carrot.", OrdinalNumber = 1 },
                new Step{ Content = "Sauté the onion, garlic, and carrot in a pot with some oil until soft.", OrdinalNumber = 2 },
                new Step{ Content = "Add the rice and soy sauce, then cook for 1-2 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Add 2 cups of water and bring to a boil, then reduce the heat to low and cover.", OrdinalNumber = 4 },
                new Step{ Content = "Cook for 20 minutes, or until rice is tender and water is absorbed.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Preheat the oven to 180°C (350°F).", OrdinalNumber = 1 },
                new Step{ Content = "Remove the stems from the mushrooms and chop them finely.", OrdinalNumber = 2 },
                new Step{ Content = "Sauté the chopped mushroom stems and garlic in butter until soft.", OrdinalNumber = 3 },
                new Step{ Content = "Mix the sautéed mushrooms with cheese and season with salt and pepper.", OrdinalNumber = 4 },
                new Step{ Content = "Stuff the mushroom caps with the cheese mixture and place on a baking sheet.", OrdinalNumber = 5 },
                new Step{ Content = "Bake in the oven for 20 minutes, or until the mushrooms are tender and the cheese is melted.", OrdinalNumber = 6 }
            ]
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
            Steps = [
                new Step{ Content = "Cook the spaghetti according to the package instructions.", OrdinalNumber = 1 },
                new Step{ Content = "Fry the bacon in a pan until crispy, then remove and chop.", OrdinalNumber = 2 },
                new Step{ Content = "In a bowl, whisk together the eggs and Parmesan cheese.", OrdinalNumber = 3 },
                new Step{ Content = "Drain the cooked spaghetti and toss with the crispy bacon.", OrdinalNumber = 4 },
                new Step{ Content = "Pour the egg and cheese mixture over the pasta and toss quickly to coat.", OrdinalNumber = 5 },
                new Step{ Content = "Season with salt and pepper to taste, and serve immediately.", OrdinalNumber = 6 }
            ]
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
            Steps = [
                new Step{ Content = "Peel and chop the potatoes into chunks.", OrdinalNumber = 1 },
                new Step{ Content = "Roast the garlic cloves in the oven for 10 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Boil the potatoes in salted water until tender, about 15 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Mash the potatoes with butter, roasted garlic, salt, and pepper.", OrdinalNumber = 4 },
                new Step{ Content = "Serve hot.", OrdinalNumber = 5 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the onion, garlic, and tomatoes.", OrdinalNumber = 1 },
                new Step{ Content = "Cook the minced beef in a pan until browned.", OrdinalNumber = 2 },
                new Step{ Content = "Sauté the onion and garlic in a separate pan until softened.", OrdinalNumber = 3 },
                new Step{ Content = "Add chopped tomatoes to the pan with the onions and garlic, and cook for 10 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Combine the browned beef with the tomato sauce, and simmer for 15 minutes.", OrdinalNumber = 5 },
                new Step{ Content = "Cook the spaghetti according to package instructions and drain.", OrdinalNumber = 6 },
                new Step{ Content = "Serve the Bolognese sauce over the cooked spaghetti.", OrdinalNumber = 7 }
            ]
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
            Steps = [
                new Step{ Content = "Chop the carrot and broccoli into bite-sized pieces.", OrdinalNumber = 1 },
                new Step{ Content = "Heat the olive oil in a pan over medium-high heat.", OrdinalNumber = 2 },
                new Step{ Content = "Add the carrots and stir-fry for 3-4 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Add the broccoli and continue stir-frying for another 5 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Season with soy sauce, salt, and pepper.", OrdinalNumber = 5 },
                new Step{ Content = "Serve immediately.", OrdinalNumber = 6 }
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
                    Id = Guid.NewGuid(),
                    Content = "Add milk, ice cream, and sugar to a blender.",
                    OrdinalNumber = 1,
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-02.jpg"
                    }
                },
                new Step{
                    Id = Guid.NewGuid(),
                    Content = "Blend until smooth.",
                    OrdinalNumber = 2,
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-03.jpg"
                    }
                },
                new Step{
                    Id = Guid.NewGuid(),
                    Content = "Serve immediately.",
                    OrdinalNumber = 3,
                    AttachedImageUrls = new List<string>{
                        "https://itdoesnttastelikechicken.com/wp-content/uploads/2022/07/how-to-make-ice-cream-in-a-blender-no-churn-without-ice-cream-maker-04.jpg"
                    }
                },
            ],
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
            Steps = [
                new Step{
                    Content = "Preheat the grill to medium-high heat.",
                    OrdinalNumber = 1,
                    AttachedImageUrls = new List<string>{
                        "https://www.eatingwell.com/thmb/uIS7xz8ZcT6WLalfYHxEvpLJF9Y=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/Hot-Grill-98dc8b55c76b427b9a026cf509ec7c48.jpg",
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXHzHjU_DYlhDHtkb3zr_f28KMRHIH-7Lgkg&s",
                        "https://blog.zgrills.com/wp-content/uploads/2022/09/what-temperature-is-medium-heat-on-a-grill.jpg"
                    }
                },
                new Step{
                    Content = "Season the chicken breasts with salt and pepper.",
                    OrdinalNumber = 2,
                    AttachedImageUrls = new List<string>{
                        "https://cdn.apartmenttherapy.info/image/upload/f_auto,q_auto:eco,c_fill,g_auto,w_1690,h_1128/k%2Farchive%2Fa0d25ce80ff3b94487b4df0f5bd83fb943b7d0b2",
                        "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/baked-skin-on-chicken-breast-ingredients.jpg",
                        "https://healthyrecipesblogs.com/wp-content/uploads/2024/03/salt-and-pepper.jpg"

                    }
                },
                new Step{
                    Content = "Grill the chicken for 6-7 minutes on each side.",
                    OrdinalNumber = 3,
                    AttachedImageUrls = new List<string>{
                        "https://s3.festivalfoods.net/blog/uploads/2022/05/IMG_0864.jpeg",
                        "https://cdn.shopify.com/s/files/1/0271/5287/5653/files/20240512131902-holychicken.jpg?v=1715519944&width=1600&height=900"
                    }
                },
                new Step{
                    Content = "Brush the chicken with BBQ sauce during the last 2 minutes of grilling.",
                    OrdinalNumber = 4,
                    AttachedImageUrls = new List<string>{
                        "https://i2.wp.com/www.downshiftology.com/wp-content/uploads/2022/06/BBQ-Chicken-11.jpg"
                    }
                },
                new Step{
                    Content = "Serve with additional BBQ sauce on the side.",
                    OrdinalNumber = 5,
                    AttachedImageUrls = new List<string>{
                        "https://thewholeserving.com/wp-content/uploads/2024/04/Platter-of-prepared-drumsticks-with-a-glass-container-of-extra-barbecue-sauce-on-the-right-side-of-the-platter.jpg"
                    }
                },
            ],
            Comments = [
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "This recipe is amazing! I tried it last night and it turned out great.",
                        CreatedAt = DateTime.Parse("2024-01-01T08:15:30"),
                        UpdatedAt = DateTime.Parse("2024-01-01T08:15:30"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "I found this recipe a bit challenging, but the result was worth it!",
                        CreatedAt = DateTime.Parse("2024-01-02T09:20:15"),
                        UpdatedAt = DateTime.Parse("2024-01-02T09:20:15"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "This recipe was okay, but I would tweak the seasoning next time.",
                        CreatedAt = DateTime.Parse("2024-01-03T11:10:45"),
                        UpdatedAt = DateTime.Parse("2024-01-03T11:10:45"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "Loved the simplicity of this recipe! Perfect for a quick meal.",
                        CreatedAt = DateTime.Parse("2024-01-04T13:45:20"),
                        UpdatedAt = DateTime.Parse("2024-01-04T13:45:20"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "My kids loved this recipe! Will definitely make it again.",
                        CreatedAt = DateTime.Parse("2024-01-05T14:35:10"),
                        UpdatedAt = DateTime.Parse("2024-01-05T14:35:10"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "The instructions were very clear and easy to follow. Thank you!",
                        CreatedAt = DateTime.Parse("2024-01-06T16:20:00"),
                        UpdatedAt = DateTime.Parse("2024-01-06T16:20:00"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "Not a fan of the flavor combination, but it was fun to try something new.",
                        CreatedAt = DateTime.Parse("2024-01-07T18:10:50"),
                        UpdatedAt = DateTime.Parse("2024-01-07T18:10:50"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "I made a vegan version of this recipe, and it turned out great!",
                        CreatedAt = DateTime.Parse("2024-01-08T19:25:40"),
                        UpdatedAt = DateTime.Parse("2024-01-08T19:25:40"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "I tried this recipe with a twist by adding some chili flakes, and it was a hit!",
                        CreatedAt = DateTime.Parse("2024-01-09T20:10:30"),
                        UpdatedAt = DateTime.Parse("2024-01-09T20:10:30"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "The texture of the dish was perfect. Definitely adding this to my recipe book.",
                        CreatedAt = DateTime.Parse("2024-01-10T08:45:50"),
                        UpdatedAt = DateTime.Parse("2024-01-10T08:45:50"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "I appreciate the detailed step-by-step instructions. Made cooking so much easier!",
                        CreatedAt = DateTime.Parse("2024-01-11T12:30:25"),
                        UpdatedAt = DateTime.Parse("2024-01-11T12:30:25"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "Tried this for a dinner party, and everyone asked for the recipe!",
                        CreatedAt = DateTime.Parse("2024-01-12T14:50:00"),
                        UpdatedAt = DateTime.Parse("2024-01-12T14:50:00"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "A bit salty for my taste, but overall a solid recipe.",
                        CreatedAt = DateTime.Parse("2024-01-13T17:15:40"),
                        UpdatedAt = DateTime.Parse("2024-01-13T17:15:40"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "This is now my go-to recipe for family gatherings. Thank you!",
                        CreatedAt = DateTime.Parse("2024-01-14T10:20:15"),
                        UpdatedAt = DateTime.Parse("2024-01-14T10:20:15"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "I added some lemon zest for extra flavor, and it worked perfectly!",
                        CreatedAt = DateTime.Parse("2024-01-15T19:10:25"),
                        UpdatedAt = DateTime.Parse("2024-01-15T19:10:25"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "A classic recipe that never fails to impress.",
                        CreatedAt = DateTime.Parse("2024-01-16T08:00:00"),
                        UpdatedAt = DateTime.Parse("2024-01-16T08:00:00"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "Tried it with fresh herbs, and it made a big difference!",
                        CreatedAt = DateTime.Parse("2024-01-17T15:45:50"),
                        UpdatedAt = DateTime.Parse("2024-01-17T15:45:50"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "A bit time-consuming, but the results were worth it!",
                        CreatedAt = DateTime.Parse("2024-01-18T11:30:00"),
                        UpdatedAt = DateTime.Parse("2024-01-18T11:30:00"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "I substituted some ingredients to fit my diet, and it still tasted great!",
                        CreatedAt = DateTime.Parse("2024-01-19T14:20:15"),
                        UpdatedAt = DateTime.Parse("2024-01-19T14:20:15"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "A wholesome and hearty meal for the whole family.",
                        CreatedAt = DateTime.Parse("2024-01-20T09:15:30"),
                        UpdatedAt = DateTime.Parse("2024-01-20T09:15:30"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
                        Content = "Wonderful recipe!",
                        CreatedAt = DateTime.Parse("2024-01-19T14:19:15"),
                        UpdatedAt = DateTime.Parse("2024-01-19T14:19:15"),
                    },
                    new Comment{
                        AccountId = Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
                        Content = "It's realy delicious!",
                        CreatedAt = DateTime.Parse("2024-01-20T09:16:30"),
                        UpdatedAt = DateTime.Parse("2024-01-20T09:16:30"),
                    },
             ],
             
        },

    ];
}
