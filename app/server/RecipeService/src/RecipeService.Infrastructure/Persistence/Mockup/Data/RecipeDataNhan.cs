using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataNhan
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
        new Recipe{
            Id = Guid.Parse("96b13b8d-959e-4b39-98ba-efeeebb1cf94"),
            Title = "Steamed Black Chicken with Ginseng",
            Description = "A nourishing dish with black chicken and ginseng, known for its health benefits.",
            ImageUrl = "https://www.akfood.vn/wp-content/uploads/2024/10/hinh-dai-dien-5.jpg",
            Serves = 2,
            CookTime = "75m",
            Ingredients = [
                "1 whole Black Chicken (cleaned and cut in half)",
                "500ml Water",
                "1 small Ginseng Root",
                "5 Red Dates",
                "10 Goji Berries",
                "2 pieces Shiitake Mushrooms",
                "1 tbsp Fish Sauce",
                "1/2 tsp Salt",
                "1/4 tsp Black Pepper",
                "1-inch Ginger (sliced)"
            ],
            Steps = [
                new Step{ Content = "Rinse the black chicken and blanch in boiling water for 3 minutes.", OrdinalNumber = 1 },
                new Step{ Content = "In a clay pot, add the chicken, ginseng, red dates, goji berries, mushrooms, and ginger.", OrdinalNumber = 2 },
                new Step{ Content = "Pour in water and bring to a boil, then reduce heat and simmer for 60 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Season with fish sauce, salt, and black pepper.", OrdinalNumber = 4 },
                new Step{ Content = "Serve hot with rice or enjoy as a tonic soup.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("d41111aa-0737-44ac-8a56-8ad74c700cdb"),
            Title = "Stir-fried Rice Noodles with Shrimp and Bean Sprouts",
            Description = "A delicious stir-fried rice noodle dish with shrimp, eggs, and fresh bean sprouts.",
            ImageUrl = "https://www.rotinrice.com/wp-content/uploads/2012/05/SoybeanSprouts-1.jpg",
            Serves = 2,
            CookTime = "30m",
            Ingredients = [
                "200g Rice Noodles (Hủ tiếu)",
                "150g Shrimp (peeled and deveined)",
                "1 Egg",
                "100g Mungbean Sprouts (Bean Sprouts)",
                "50g Chives (cut into 3cm lengths)",
                "1 Shallot (sliced)",
                "2 cloves Garlic (minced)",
                "2 tbsp Soy Sauce",
                "1 tbsp Oyster Sauce",
                "1/2 tsp Sugar",
                "1/4 tsp Black Pepper",
                "1 tbsp Cooking Oil",
                "Lime Wedges for serving"
            ],
            Steps = [
                new Step{ Content = "Soak the rice noodles in warm water for 30 minutes, then drain.", OrdinalNumber = 1 },
                new Step{ Content = "Heat oil in a pan and sauté garlic and shallots until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add shrimp and stir-fry until they turn pink.", OrdinalNumber = 3 },
                new Step{ Content = "Push the shrimp aside and crack the egg into the pan, scrambling it lightly.", OrdinalNumber = 4 },
                new Step{ Content = "Add the softened rice noodles, soy sauce, oyster sauce, sugar, and pepper. Stir-fry until well combined.", OrdinalNumber = 5 },
                new Step{ Content = "Toss in the bean sprouts and chives, stir-frying for another minute.", OrdinalNumber = 6 },
                new Step{ Content = "Serve hot with lime wedges on the side.", OrdinalNumber = 7 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("10c3a583-c700-4911-9893-531727fb4645"),
            Title = "Stir-Fried Bean Sprouts with Shrimp",
            Description = "A quick and delicious stir-fried dish with fresh bean sprouts and shrimp.",
            ImageUrl = "https://panlasangpinoy.com/wp-content/uploads/2017/01/Ginisang-Togue-with-Shrimp-500x485.jpg",
            Serves = 2,
            CookTime = "15m",
            Ingredients = [
                "200g Bean Sprouts",
                "150g Shrimp (peeled and deveined)",
                "1 Shallot (minced)",
                "2 cloves Garlic (minced)",
                "1 tbsp Fish Sauce",
                "1/2 tsp Sugar",
                "1/4 tsp Black Pepper",
                "1 tbsp Vegetable Oil",
                "Chopped Green Onions (for garnish)"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pan and sauté garlic and shallot until fragrant.", OrdinalNumber = 1 },
                new Step{ Content = "Add shrimp and stir-fry until pink and cooked through.", OrdinalNumber = 2 },
                new Step{ Content = "Add bean sprouts, fish sauce, sugar, and black pepper. Stir-fry for another 2 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 4 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("0d0557df-80b9-421e-a951-fb64fe5ed4e8"),
            Title = "Steamed Chicken with Ginger",
            Description = "A flavorful and aromatic steamed chicken dish with fresh ginger.",
            ImageUrl = "https://andrewzimmern.com/wp-content/uploads/Steamed-Chicken.jpg",
            Serves = 2,
            CookTime = "30m",
            Ingredients = [
                "400g Chicken (cut into pieces)",
                "50g Ginger (sliced thinly)",
                "2 Shallots (minced)",
                "2 tbsp Soy Sauce",
                "1 tbsp Oyster Sauce",
                "1/2 tsp Sugar",
                "1/4 tsp Black Pepper",
                "1 tbsp Sesame Oil",
                "Chopped Green Onions (for garnish)"
            ],
            Steps = [
                new Step{ Content = "Marinate chicken with soy sauce, oyster sauce, sugar, black pepper, and sesame oil for 15 minutes.", OrdinalNumber = 1 },
                new Step{ Content = "Arrange chicken on a steaming plate and top with sliced ginger and shallots.", OrdinalNumber = 2 },
                new Step{ Content = "Steam over high heat for 20 minutes until chicken is fully cooked.", OrdinalNumber = 3 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 4 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("f00a1613-6c69-43e1-b3b5-acd96efbf92a"),
            Title = "Caramelized Catfish with Ginger",
            Description = "A rich and savory Vietnamese-style caramelized catfish dish infused with ginger.",
            ImageUrl = "https://vickypham.com/wp-content/uploads/2024/08/1bb36-cakho216x9.jpg",
            Serves = 2,
            CookTime = "40m",
            Ingredients = [
                "400g Catfish (sliced into steaks)",
                "50g Ginger (julienned)",
                "2 Shallots (minced)",
                "2 tbsp Fish Sauce",
                "1 tbsp Soy Sauce",
                "1 tbsp Sugar",
                "1/2 tsp Black Pepper",
                "1 tbsp Cooking Oil",
                "1 cup Coconut Water"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pan, add sugar, and stir until it caramelizes.", OrdinalNumber = 1 },
                new Step{ Content = "Add catfish and sear on both sides until golden.", OrdinalNumber = 2 },
                new Step{ Content = "Add minced shallots and julienned ginger, stir for a few seconds.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in fish sauce, soy sauce, and coconut water, then simmer for 30 minutes until sauce thickens.", OrdinalNumber = 4 },
                new Step{ Content = "Sprinkle black pepper and serve hot with rice.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("e36fa6bc-f2e9-400b-a06f-ea63edabce71"),
            Title = "Steamed Egg with Green Onion",
            Description = "A soft and silky steamed egg dish with fragrant green onions.",
            ImageUrl = "https://olivia-yi.com/wp-content/uploads/2022/03/Korean-Steamed-Eggs-768x1024.png",
            Serves = 2,
            CookTime = "15m",
            Ingredients = [
                "3 Eggs",
                "200ml Water (or Chicken Broth)",
                "1/2 tsp Salt",
                "1/2 tsp Fish Sauce",
                "1/4 tsp White Pepper",
                "1 tsp Cooking Oil",
                "2 tbsp Green Onion (finely chopped)"
            ],
            Steps = [
                new Step{ Content = "Beat the eggs gently in a bowl without making too many bubbles.", OrdinalNumber = 1 },
                new Step{ Content = "Mix in water, salt, fish sauce, and white pepper, then strain the mixture for a smooth texture.", OrdinalNumber = 2 },
                new Step{ Content = "Pour the egg mixture into a heatproof bowl, cover with foil, and steam on low heat for 10 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Drizzle a little oil on top, sprinkle with chopped green onions, and steam for another 2 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Serve hot with rice or as a side dish.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("2f38f470-ac7f-4da5-b2c3-d9bc12135833"),
            Title = "Grilled Shrimp with Green Onion Oil",
            Description = "Delicious grilled shrimp brushed with fragrant green onion oil.",
            ImageUrl = "https://danviet.mediacdn.vn/upload/1-2015/images/2015-01-22/1434356498-dbnntom-nuong-mo-hanh-6-091158789.jpg",
            Serves = 2,
            CookTime = "20m",
            Ingredients = [
                "200g Shrimp (whole, with shell)",
                "1 tbsp Cooking Oil",
                "1/2 tsp Salt",
                "1/2 tsp Black Pepper",
                "1 tsp Fish Sauce",
                "2 tbsp Green Onion (finely chopped)"
            ],
            Steps = [
                new Step{ Content = "Clean the shrimp and pat them dry.", OrdinalNumber = 1 },
                new Step{ Content = "Season with salt, pepper, and fish sauce. Let marinate for 10 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Grill the shrimp over medium heat until fully cooked, about 3-4 minutes per side.", OrdinalNumber = 3 },
                new Step{ Content = "In a pan, heat oil and add the chopped green onion, stirring for a few seconds until fragrant.", OrdinalNumber = 4 },
                new Step{ Content = "Brush the green onion oil over the grilled shrimp and serve immediately.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("82254e93-9eff-4dfd-8263-cce3c4a2289a"),
            Title = "Vietnamese Pickled Shallots",
            Description = "Tangy and slightly sweet Vietnamese-style pickled shallots, perfect as a side dish.",
            ImageUrl = "https://moorlandseater.com/wp-content/uploads/2020/07/thai-pickled-shallots-moorlands-eater-DSC08637p.jpg",
            Serves = 4,
            CookTime = "30m (plus 3 days pickling time)",
            Ingredients = [
                "200g Shallots (peeled)",
                "200ml Vinegar",
                "100ml Water",
                "50g Sugar",
                "1/2 tsp Salt",
                "1 Chili (optional, sliced)"
            ],
            Steps = [
                new Step{ Content = "Peel and clean the shallots. Soak them in warm water for 10 minutes to reduce sharpness.", OrdinalNumber = 1 },
                new Step{ Content = "In a pot, combine vinegar, water, sugar, and salt. Heat until the sugar dissolves, then let cool.", OrdinalNumber = 2 },
                new Step{ Content = "Place the shallots in a clean jar and pour the pickling liquid over them.", OrdinalNumber = 3 },
                new Step{ Content = "Add sliced chili if desired. Close the jar tightly and store at room temperature for 3 days before serving.", OrdinalNumber = 4 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("2ab34191-4b0a-494b-9920-c1b12c618b57"),
            Title = "Stuffed Bitter Melon Soup",
            Description = "A comforting soup featuring bitter melon stuffed with seasoned ground pork, simmered in a flavorful broth.",
            ImageUrl = "https://vickypham.com/wp-content/uploads/2024/08/2940d-2024_01_17eosm59297.jpg",
            Serves = 4,
            CookTime = "50m",
            Ingredients = [
                "2 Bitter melons (cut into 5cm sections, hollowed out)",
                "200g Ground pork",
                "3 Shallots (minced)",
                "1 Green onion (chopped, for garnish)",
                "1 tbsp Fish sauce",
                "1/2 tsp Black pepper",
                "1/2 tsp Sugar",
                "1/2 tsp Salt",
                "1 liter Chicken broth",
                "1 tbsp Cooking oil"
            ],
            Steps = [
                new Step{ Content = "In a bowl, mix ground pork, shallots, fish sauce, black pepper, sugar, and salt.", OrdinalNumber = 1 },
                new Step{ Content = "Stuff the seasoned ground pork mixture into the hollowed-out bitter melon sections.", OrdinalNumber = 2 },
                new Step{ Content = "Heat oil in a pot, add minced shallots, and sauté until fragrant.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in chicken broth and bring to a gentle boil.", OrdinalNumber = 4 },
                new Step{ Content = "Add the stuffed bitter melon and simmer for 30 minutes until tender.", OrdinalNumber = 5 },
                new Step{ Content = "Adjust seasoning with salt or fish sauce to taste. Garnish with chopped green onions before serving.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("646f74bf-c84c-4a8c-996c-fa9861e4c8a2"),
            Title = "Stir-Fried Bitter Melon with Eggs",
            Description = "A quick and nutritious dish featuring thinly sliced bitter melon stir-fried with eggs and seasoned for a balanced taste.",
            ImageUrl = "https://images.getrecipekit.com/20240509121943-bitter_melon_with_eggs_1_1296x.webp?aspect_ratio=1:1&quality=90&",
            Serves = 2,
            CookTime = "15m",
            Ingredients = [
                "1 Bitter melon (thinly sliced)",
                "2 Eggs (beaten)",
                "2 Shallots (minced)",
                "1 Green onion (chopped, for garnish)",
                "1 tbsp Fish sauce",
                "1/2 tsp Sugar",
                "1/4 tsp Black pepper",
                "1 tbsp Cooking oil"
            ],
            Steps = [
                new Step{ Content = "Slice the bitter melon thinly and soak in salted water for 10 minutes to reduce bitterness. Drain and set aside.", OrdinalNumber = 1 },
                new Step{ Content = "Heat oil in a pan, add shallots, and sauté until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add the bitter melon and stir-fry for 3–4 minutes until slightly softened.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in the beaten eggs and stir quickly to coat the bitter melon evenly.", OrdinalNumber = 4 },
                new Step{ Content = "Season with fish sauce, sugar, and black pepper. Stir until the eggs are fully cooked.", OrdinalNumber = 5 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("eefcfb9f-0c17-4698-806d-2b333e8c1f18"),
            Title = "Bitter Melon Soup with Pork Ribs",
            Description = "A comforting and nutritious soup featuring tender pork ribs and bitter melon, simmered to perfection.",
            ImageUrl = "https://thumbs.dreamstime.com/b/bitter-melon-soup-pork-ribs-local-thai-90407343.jpg",
            Serves = 4,
            CookTime = "1h",
            Ingredients = [
                "2 Bitter melons (cut into chunks)",
                "500g Pork ribs",
                "1 Shallot (minced)",
                "1.5L Water",
                "1 tbsp Fish sauce",
                "1/2 tsp Salt",
                "1/4 tsp Black pepper",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Blanch the pork ribs in boiling water for 5 minutes to remove impurities, then rinse with clean water.", OrdinalNumber = 1 },
                new Step{ Content = "In a pot, add water and bring it to a boil. Add the cleaned pork ribs and simmer for 30 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Meanwhile, cut the bitter melon into chunks and remove the seeds.", OrdinalNumber = 3 },
                new Step{ Content = "Add the bitter melon to the soup and continue simmering for another 20 minutes until tender.", OrdinalNumber = 4 },
                new Step{ Content = "Season with fish sauce, salt, and black pepper to taste.", OrdinalNumber = 5 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("c0c1a561-1f43-4d38-b166-7633d0c96e85"),
            Title = "Taro and Pork Rib Soup",
            Description = "A hearty and creamy soup with tender pork ribs and soft taro, perfect for a comforting meal.",
            ImageUrl = "https://hoianfoodtour.com/wp-content/uploads/2013/08/canh-khoai-mon-taro-soup-2_2012-08-06_12-51-05.jpg",
            Serves = 4,
            CookTime = "1h",
            Ingredients = [
                "500g Pork ribs",
                "300g Taro (peeled and cut into chunks)",
                "1 Shallot (minced)",
                "1.5L Water",
                "1 tbsp Fish sauce",
                "1/2 tsp Salt",
                "1/4 tsp Black pepper",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Blanch the pork ribs in boiling water for 5 minutes to remove impurities, then rinse with clean water.", OrdinalNumber = 1 },
                new Step{ Content = "In a pot, add water and bring it to a boil. Add the cleaned pork ribs and simmer for 30 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Add the taro pieces and continue simmering for another 20 minutes until soft.", OrdinalNumber = 3 },
                new Step{ Content = "Season with fish sauce, salt, and black pepper to taste.", OrdinalNumber = 4 },
                new Step{ Content = "Garnish with chopped green onions and serve hot.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("d8ca76cf-fa39-4fb1-aa38-4477c503afd8"),
            Title = "Taro and Chicken Curry",
            Description = "A rich and creamy chicken curry with tender taro, infused with aromatic spices and coconut milk.",
            ImageUrl = "https://i0.wp.com/blog.themalamarket.com/wp-content/uploads/2024/10/Yuer-Ji_final_4.jpg?resize=800%2C600&ssl=1",
            Serves = 4,
            CookTime = "1h",
            Ingredients = [
                "500g Chicken (cut into pieces)",
                "300g Taro (peeled and cubed)",
                "200ml Coconut milk",
                "1 Shallot (minced)",
                "2 cloves Garlic (minced)",
                "1 tbsp Curry powder",
                "1 tbsp Fish sauce",
                "1/2 tsp Salt",
                "1/2 tsp Black pepper",
                "2 tbsp Cooking oil",
                "500ml Water",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pot, sauté shallots and garlic until fragrant.", OrdinalNumber = 1 },
                new Step{ Content = "Add curry powder and stir for 30 seconds to release the aroma.", OrdinalNumber = 2 },
                new Step{ Content = "Add chicken pieces, season with fish sauce, salt, and black pepper. Stir-fry until chicken is browned.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in water and bring to a boil. Simmer for 30 minutes until the chicken is tender.", OrdinalNumber = 4 },
                new Step{ Content = "Add taro and continue simmering for 15-20 minutes until the taro is soft.", OrdinalNumber = 5 },
                new Step{ Content = "Pour in coconut milk and stir well. Let it simmer for another 5 minutes.", OrdinalNumber = 6 },
                new Step{ Content = "Garnish with chopped green onions and serve hot with steamed rice or bread.", OrdinalNumber = 7 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("f71a70ca-7f8a-4b2b-8141-9a68eba845b3"),
            Title = "Beef and Potato Stir-Fry",
            Description = "A savory stir-fry featuring tender beef slices and golden potatoes in a flavorful sauce.",
            ImageUrl = "https://i-giadinh.vnecdn.net/2021/10/11/maxresdefault-1633939458-5537-1633939495.jpg",
            Serves = 4,
            CookTime = "35m",
            Ingredients = [
                "400g Beef (thinly sliced)",
                "300g Potato (peeled and sliced into thin strips)",
                "1 Shallot (minced)",
                "2 cloves Garlic (minced)",
                "1 tbsp Soy sauce",
                "1 tbsp Oyster sauce",
                "1/2 tsp Black pepper",
                "1/2 tsp Salt",
                "1 tbsp Cornstarch",
                "2 tbsp Cooking oil",
                "1 Green onion (chopped)",
                "100ml Water"
            ],
            Steps = [
                new Step{ Content = "Marinate beef with soy sauce, oyster sauce, black pepper, and cornstarch for 15 minutes.", OrdinalNumber = 1 },
                new Step{ Content = "Heat oil in a pan, add shallots and garlic, and sauté until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add potatoes and stir-fry for 5 minutes until lightly golden.", OrdinalNumber = 3 },
                new Step{ Content = "Push potatoes to the side, add beef and stir-fry on high heat until browned.", OrdinalNumber = 4 },
                new Step{ Content = "Mix everything together, add water, and let it simmer for 5 minutes until potatoes are soft.", OrdinalNumber = 5 },
                new Step{ Content = "Sprinkle with chopped green onions before serving. Enjoy with steamed rice.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("3d8ba8d0-34a0-48c2-9f1d-cb1a9ec402c2"),
            Title = "Garlic Butter Roasted Potatoes",
            Description = "Crispy roasted potatoes tossed in garlic butter and herbs for a delicious side dish.",
            ImageUrl = "https://i.imgur.com/example.jpg",
            Serves = 4,
            CookTime = "40m",
            Ingredients = [
                "500g Potato (cut into bite-sized cubes)",
                "3 cloves Garlic (minced)",
                "2 tbsp Butter (melted)",
                "1 tbsp Olive oil",
                "1/2 tsp Salt",
                "1/2 tsp Black pepper",
                "1/2 tsp Paprika",
                "1 tbsp Fresh parsley (chopped)"
            ],
            Steps = [
                new Step{ Content = "Preheat oven to 200°C (400°F).", OrdinalNumber = 1 },
                new Step{ Content = "In a bowl, mix potatoes with olive oil, salt, pepper, and paprika.", OrdinalNumber = 2 },
                new Step{ Content = "Spread potatoes on a baking sheet in a single layer and roast for 30 minutes, flipping halfway through.", OrdinalNumber = 3 },
                new Step{ Content = "In a pan, melt butter over low heat and sauté garlic until fragrant.", OrdinalNumber = 4 },
                new Step{ Content = "Remove roasted potatoes from the oven, toss them in garlic butter, and garnish with fresh parsley.", OrdinalNumber = 5 },
                new Step{ Content = "Serve hot and enjoy!", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("2c901acf-88df-45a7-a0c9-bdae174c7b2d"),
            Title = "Kiwi Yogurt Smoothie",
            Description = "A refreshing and creamy smoothie made with fresh kiwi, yogurt, and honey.",
            ImageUrl = "https://i.imgur.com/example.jpg",
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "2 Kiwi (peeled and sliced)",
                "200ml Yogurt (plain or vanilla)",
                "100ml Milk",
                "1 tbsp Honey",
                "1/2 cup Ice cubes"
            ],
            Steps = [
                new Step{ Content = "Peel and slice the kiwi.", OrdinalNumber = 1 },
                new Step{ Content = "In a blender, combine kiwi, yogurt, milk, honey, and ice cubes.", OrdinalNumber = 2 },
                new Step{ Content = "Blend until smooth and creamy.", OrdinalNumber = 3 },
                new Step{ Content = "Pour into glasses and serve immediately.", OrdinalNumber = 4 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("c4e37757-9525-4f5e-ae77-abf2a9a143a5"),
            Title = "Vietnamese Bamboo Shoot Soup",
            Description = "A light and flavorful soup made with fresh bamboo shoots, pork ribs, and aromatic herbs.",
            ImageUrl = "https://cdn.tgdd.vn/Files/2022/04/04/1423846/cach-nau-canh-mang-suon-ngon-ngot-hap-dan-khong-bi-dang-202204041428499880.jpg",
            Serves = 4,
            CookTime = "60m",
            Ingredients = [
                "300g Bamboo shoots (sliced and boiled)",
                "500g Pork ribs",
                "1 Shallot (sliced)",
                "1 tbsp Fish sauce",
                "1/2 tsp Salt",
                "1/2 tsp Black pepper",
                "1.5L Water",
                "2 Green onions (chopped)",
                "Fresh cilantro (for garnish)"
            ],
            Steps = [
                new Step{ Content = "Boil the bamboo shoots for 5 minutes, then drain and rinse with cold water.", OrdinalNumber = 1 },
                new Step{ Content = "In a pot, heat some oil and sauté shallots until fragrant.", OrdinalNumber = 2 },
                new Step{ Content = "Add pork ribs and sear until lightly browned.", OrdinalNumber = 3 },
                new Step{ Content = "Pour in water and bring to a boil, then simmer for 30 minutes.", OrdinalNumber = 4 },
                new Step{ Content = "Add bamboo shoots, fish sauce, salt, and pepper. Simmer for another 15 minutes.", OrdinalNumber = 5 },
                new Step{ Content = "Garnish with green onions and cilantro before serving.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("1d4dfc1b-46fe-42c0-a8b1-1103bc0a3cae"),
            Title = "Stir-Fried Bamboo Shoots with Beef",
            Description = "A savory stir-fry dish with tender beef slices and crunchy bamboo shoots, infused with garlic and oyster sauce.",
            ImageUrl = "https://delightfulplate.com/wp-content/uploads/2023/03/Stir-fried-Beef-with-Bamboo-Shoots-Bo-xao-mang.jpg",
            Serves = 3,
            CookTime = "30m",
            Ingredients = [
                "300g Bamboo shoots (sliced and boiled)",
                "250g Beef (thinly sliced)",
                "1 Shallot (sliced)",
                "3 cloves Garlic (minced)",
                "1 tbsp Soy sauce",
                "1 tbsp Oyster sauce",
                "1/2 tsp Black pepper",
                "1/2 tsp Sugar",
                "2 tbsp Cooking oil",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Boil the bamboo shoots for 5 minutes, drain, and rinse with cold water.", OrdinalNumber = 1 },
                new Step{ Content = "Marinate beef with soy sauce, black pepper, and sugar for 10 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Heat oil in a pan and sauté garlic and shallot until fragrant.", OrdinalNumber = 3 },
                new Step{ Content = "Add marinated beef and stir-fry on high heat until slightly browned.", OrdinalNumber = 4 },
                new Step{ Content = "Add bamboo shoots and oyster sauce, stir well for 5 minutes.", OrdinalNumber = 5 },
                new Step{ Content = "Sprinkle chopped green onions before serving.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("03ba2096-70fe-4d97-9837-f0736aaa790c"),
            Title = "Spicy Bamboo Shoot and Pork Stir-Fry",
            Description = "A flavorful stir-fry with tender pork slices and bamboo shoots in a spicy, savory sauce.",
            ImageUrl = "https://ohsnapletseat.com/wp-content/uploads/2021/12/IMG_1357-1024x768.jpg",
            Serves = 3,
            CookTime = "25m",
            Ingredients = [
                "300g Bamboo shoots (sliced and boiled)",
                "250g Pork (thinly sliced)",
                "2 Shallots (sliced)",
                "3 cloves Garlic (minced)",
                "2 Red chilies (sliced)",
                "1 tbsp Soy sauce",
                "1 tbsp Fish sauce",
                "1/2 tsp Black pepper",
                "1/2 tsp Sugar",
                "1 tbsp Chili paste",
                "2 tbsp Cooking oil",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Boil the bamboo shoots for 5 minutes, drain, and rinse with cold water.", OrdinalNumber = 1 },
                new Step{ Content = "Marinate pork with soy sauce, black pepper, and sugar for 10 minutes.", OrdinalNumber = 2 },
                new Step{ Content = "Heat oil in a pan and sauté garlic, shallots, and chili until fragrant.", OrdinalNumber = 3 },
                new Step{ Content = "Add marinated pork and stir-fry on high heat until slightly browned.", OrdinalNumber = 4 },
                new Step{ Content = "Add bamboo shoots, fish sauce, and chili paste, stir well for 5 minutes.", OrdinalNumber = 5 },
                new Step{ Content = "Sprinkle chopped green onions before serving.", OrdinalNumber = 6 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("32fe68f0-c2be-4409-8df8-3d448fc6e324"),
            Title = "Stir-Fried Luffa with Shrimp",
            Description = "A light and savory stir-fry featuring tender luffa and succulent shrimp, perfect with steamed rice.",
            ImageUrl = "https://static.wixstatic.com/media/da0dac_ff1c40f496a24c22a0b42727afea71bf~mv2.jpg/v1/fill/w_980,h_1307,al_c,q_85,usm_0.66_1.00_0.01,enc_avif,quality_auto/da0dac_ff1c40f496a24c22a0b42727afea71bf~mv2.jpg",
            Serves = 3,
            CookTime = "20m",
            Ingredients = [
                "2 Luffa (peeled and sliced)",
                "200g Shrimp (peeled and deveined)",
                "2 cloves Garlic (minced)",
                "1 Shallot (sliced)",
                "1 tbsp Fish sauce",
                "1/2 tsp Sugar",
                "1/4 tsp Black pepper",
                "2 tbsp Cooking oil",
                "1 Green onion (chopped)"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pan and sauté garlic and shallot until fragrant.", OrdinalNumber = 1 },
                new Step{ Content = "Add shrimp and stir-fry until they turn pink and slightly curled.", OrdinalNumber = 2 },
                new Step{ Content = "Add luffa slices and stir well for 2-3 minutes.", OrdinalNumber = 3 },
                new Step{ Content = "Season with fish sauce, sugar, and black pepper. Stir-fry for another 3 minutes until luffa is tender.", OrdinalNumber = 4 },
                new Step{ Content = "Garnish with chopped green onions before serving.", OrdinalNumber = 5 }
            ]
        },
        new Recipe{
            Id = Guid.Parse("b1933e92-e1ed-4fe2-837d-e77bd22c89ca"),
            Title = "Luffa Soup with Minced Pork",
            Description = "A light and nutritious soup with soft luffa and flavorful minced pork, perfect for a comforting meal.",
            ImageUrl = "https://img-global.cpcdn.com/recipes/a02a09078e0f6736/680x482cq70/canh-m%C6%B0%E1%BB%9Bp-th%E1%BB%8Bt-b%E1%BA%B1m-recipe-main-photo.jpg",
            Serves = 4,
            CookTime = "25m",
            Ingredients = [
                "2 Luffa (peeled and sliced)",
                "200g Ground Pork",
                "1 Shallot (minced)",
                "1 clove Garlic (minced)",
                "1 liter Water",
                "1 tbsp Fish sauce",
                "1/2 tsp Salt",
                "1/4 tsp Black pepper",
                "1 Green onion (chopped)",
                "1 tbsp Cooking oil"
            ],
            Steps = [
                new Step{ Content = "Heat oil in a pot, sauté shallot and garlic until fragrant.", OrdinalNumber = 1 },
                new Step{ Content = "Add ground pork and stir-fry until fully cooked.", OrdinalNumber = 2 },
                new Step{ Content = "Pour in water and bring to a boil.", OrdinalNumber = 3 },
                new Step{ Content = "Add luffa slices and season with fish sauce, salt, and black pepper.", OrdinalNumber = 4 },
                new Step{ Content = "Simmer for 5 minutes until luffa is soft, then garnish with chopped green onion.", OrdinalNumber = 5 }
            ]
        },
    ];
}
