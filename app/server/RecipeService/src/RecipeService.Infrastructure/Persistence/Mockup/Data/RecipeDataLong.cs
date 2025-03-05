using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataLong {
  public static List<Recipe> Recipes = [
    // King Trumpet Mushroom Recipes
    new Recipe {
      Id = Guid.Parse("809aedc6-da3c-4d40-9e04-23fec17ced58"),
      Title = "Sautéed King Trumpet Mushrooms with Garlic",
      Description = "A simple dish highlighting the robust flavor of king " +
                    "trumpet mushrooms sautéed with garlic and herbs.",
      ImageUrl = "https://i.ytimg.com/vi/vgxpce103E0/maxresdefault.jpg",
      Serves = 2, CookTime = "15m",
      Ingredients =
          [
            "200g King Trumpet Mushrooms", "2 cloves Garlic",
            "1 tbsp Olive Oil", "Pinch of Salt", "Pinch of Pepper",
            "1 tbsp Fresh Parsley"
          ],
      Steps =
          [
            new Step { Content = "Clean and slice the mushrooms.",
                       OrdinalNumber = 1 },
            new Step { Content = "Heat olive oil in a pan and sauté minced " +
                                 "garlic until fragrant.",
                       OrdinalNumber = 2 },
            new Step { Content = "Add mushrooms and cook until tender.",
                       OrdinalNumber = 3 },
            new Step {
              Content = "Season with salt, pepper, and garnish with parsley.",
              OrdinalNumber = 4
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("3cd271e8-6da9-48e5-a86f-92a841428099"),
      Title = "King Trumpet Mushroom Risotto",
      Description = "A creamy risotto featuring king trumpet mushrooms for a " +
                    "savory and satisfying meal.",
      ImageUrl = "https://lifesourcenaturalfoods.com/wp-content/uploads/2022/" +
                 "04/Risotto_TrumpetMushroom-0154_530x350.jpg",
      Serves = 4, CookTime = "30m",
      Ingredients =
          [
            "250g King Trumpet Mushrooms", "1 cup Arborio Rice",
            "1/2 cup White Wine", "4 cups Vegetable Broth", "1 Onion, diced",
            "2 tbsp Butter", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content =
                           "Sauté chopped onion in butter until translucent.",
                       OrdinalNumber = 1 },
            new Step { Content = "Add sliced mushrooms and cook until soft.",
                       OrdinalNumber = 2 },
            new Step { Content = "Stir in rice and toast for 2 minutes.",
                       OrdinalNumber = 3 },
            new Step { Content = "Gradually add broth and wine, stirring " +
                                 "constantly until creamy.",
                       OrdinalNumber = 4 },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 5 }
          ]
    },

    // Shiitake Mushroom Recipes
    new Recipe {
      Id = Guid.Parse("698b75da-7407-4f52-b8ef-ff50abeb71dd"),
      Title = "Shiitake Mushroom Stir-Fry",
      Description = "A quick stir-fry with shiitake mushrooms and " +
                    "vegetables, perfect for a healthy meal.",
      ImageUrl = "https://cleananddelicious.com/wp-content/uploads/2021/08/" +
                 "shiitake-mushrooms-recipe-1.jpg",
      Serves = 3, CookTime = "20m",
      Ingredients =
          [
            "200g Shiitake Mushrooms", "1 Red Bell Pepper, sliced",
            "1 tbsp Soy Sauce", "1 tbsp Sesame Oil", "2 cloves Garlic, minced",
            "1 tsp Fresh Ginger, grated"
          ],
      Steps =
          [
            new Step { Content = "Slice shiitake mushrooms and bell pepper.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Heat sesame oil in a wok; add garlic and ginger.",
                       OrdinalNumber = 2 },
            new Step {
              Content =
                  "Add mushrooms and bell pepper, stir-frying for 5 minutes.",
              OrdinalNumber = 3
            },
            new Step {
              Content = "Drizzle with soy sauce and cook for 2 more minutes.",
              OrdinalNumber = 4
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("3e794493-78c9-4f59-b6e3-3dae0989d314"),
      Title = "Shiitake Mushroom Soup",
      Description =
          "A warming soup featuring shiitake mushrooms and aromatic herbs.",
      ImageUrl =
          "https://www.simplyrecipes.com/thmb/rXAU4tiQSHzaDWJc0M1OBKzCyDE=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "__opt__aboutcom__coeus__resources__content_migration__simply_" +
          "recipes__uploads__2012__01__chicken-soup-ginger-shiitake-horiz-a-" +
          "1600-313805e44a124afe87bd2a7a8c7977b2.jpg",
      Serves = 4, CookTime = "25m",
      Ingredients =
          [
            "250g Shiitake Mushrooms, sliced", "4 cups Chicken Broth",
            "1 Onion, chopped", "2 cloves Garlic, minced", "1 tbsp Soy Sauce",
            "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content =
                           "Sauté chopped onion and garlic until softened.",
                       OrdinalNumber = 1 },
            new Step {
              Content = "Add sliced shiitake mushrooms and cook for 3 minutes.",
              OrdinalNumber = 2
            },
            new Step {
              Content =
                  "Pour in chicken broth and soy sauce; simmer for 15 minutes.",
              OrdinalNumber = 3
            },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 4 }
          ]
    },

    // Enoki Mushroom Recipes
    new Recipe {
      Id = Guid.Parse("6192c1bc-f8f4-4f15-9574-79fe3a544fd8"),
      Title = "Enoki Mushroom Tempura",
      Description = "Crispy tempura featuring delicate enoki mushrooms " +
                    "served with a light dipping sauce.",
      ImageUrl =
          "https://images.food52.com/BcnVFMvgVxyF1yW5hEGSNhovXiM=/1200x1200/" +
          "10381dc8-9505-45c8-a7d0-fa3de6734885--yi-jun-loh-enoki-mushrooms." +
          "jpg",
      Serves = 2, CookTime = "20m",
      Ingredients =
          [
            "150g Enoki Mushrooms", "1 cup Tempura Batter",
            "1 cup Vegetable Oil", "1 tsp Salt"
          ],
      Steps =
          [
            new Step { Content = "Prepare the tempura batter according to " +
                                 "package instructions.",
                       OrdinalNumber = 1 },
            new Step { Content = "Dip enoki mushrooms into the batter.",
                       OrdinalNumber = 2 },
            new Step { Content = "Deep fry in hot oil until golden and crispy.",
                       OrdinalNumber = 3 },
            new Step { Content =
                           "Drain on paper towels and sprinkle with salt.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("adbfc35d-e0f5-471b-a97f-8762e3584aeb"),
      Title = "Enoki Mushroom Salad",
      Description = "A refreshing salad with enoki mushrooms and mixed " +
                    "greens dressed in a light vinaigrette.",
      ImageUrl =
          "https://kirrconcept.com/cdn/shop/articles/IMG_9828.jpg?v=1629862239",
      Serves = 2, CookTime = "10m",
      Ingredients =
          [
            "100g Enoki Mushrooms", "50g Mixed Greens", "1 tbsp Olive Oil",
            "1 tsp Lemon Juice", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Clean enoki mushrooms and trim the ends.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Combine mushrooms with mixed greens in a bowl.",
                       OrdinalNumber = 2 },
            new Step {
              Content =
                  "Drizzle olive oil and lemon juice, then toss to combine.",
              OrdinalNumber = 3
            },
            new Step { Content = "Season with salt and pepper.",
                       OrdinalNumber = 4 }
          ]
    },

    // Black Fungus Recipes
    new Recipe {
      Id = Guid.Parse("2c7c4a1f-f6a0-435e-9703-d30e4be80f5c"),
      Title = "Black Fungus Salad",
      Description =
          "A tangy and refreshing salad featuring crisp black fungus.",
      ImageUrl = "https://images.squarespace-cdn.com/content/v1/" +
                 "5b4f41e196e76f286102ccf0/" +
                 "1554816525020-QI2JAMREEOJJHFZ59F4D/IMG_9783.jpg",
      Serves = 2, CookTime = "15m",
      Ingredients =
          [
            "150g Black Fungus", "1 Cucumber, julienned", "2 tbsp Rice Vinegar",
            "1 tsp Sugar", "Pinch of Salt"
          ],
      Steps =
          [
            new Step {
              Content =
                  "Soak black fungus in warm water until softened, then drain.",
              OrdinalNumber = 1
            },
            new Step { Content = "Slice cucumber into thin julienne strips.",
                       OrdinalNumber = 2 },
            new Step { Content = "Toss black fungus and cucumber with rice " +
                                 "vinegar, sugar, and salt.",
                       OrdinalNumber = 3 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("c0a09d9c-299d-4214-827a-c0ce8e016c52"),
      Title = "Black Fungus Stir-Fry with Vegetables",
      Description =
          "A quick stir-fry combining black fungus with colorful vegetables.",
      ImageUrl =
          "https://img-global.cpcdn.com/recipes/ba0fe90072e4379c/680x482cq70/" +
          "stir-fried-black-fungus-pork-egg-recipe-main-photo.jpg",
      Serves = 3, CookTime = "20m",
      Ingredients =
          [
            "150g Black Fungus, soaked and sliced", "1 Carrot, julienned",
            "1 Bell Pepper, sliced", "1 tbsp Soy Sauce", "1 tsp Sesame Oil",
            "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content =
                           "Prepare black fungus by soaking and slicing it.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Julienne the carrot and slice the bell pepper.",
                       OrdinalNumber = 2 },
            new Step {
              Content =
                  "Stir-fry all ingredients in sesame oil over high heat.",
              OrdinalNumber = 3
            },
            new Step { Content =
                           "Add soy sauce and season with salt and pepper.",
                       OrdinalNumber = 4 }
          ]
    },

    // Flower Snails Recipes
    new Recipe {
      Id = Guid.Parse("b0082062-dc9f-4c8c-86c4-a991f086e97c"),
      Title = "Garlic Butter Flower Snails",
      Description = "Tender flower snails sautéed in garlic butter—a " +
                    "delicacy for seafood lovers.",
      ImageUrl = "https://encrypted-tbn0.gstatic.com/" +
                 "images?q=tbn:ANd9GcQNSO09cA-W6VK52vSsKlYvvv9DLt56WzRI2Q&s",
      Serves = 2, CookTime = "15m",
      Ingredients =
          [
            "200g Flower Snails", "3 cloves Garlic, minced", "2 tbsp Butter",
            "1 tbsp Lemon Juice", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Clean the flower snails thoroughly.",
                       OrdinalNumber = 1 },
            new Step { Content = "Melt butter in a pan and sauté minced " +
                                 "garlic until fragrant.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Add flower snails and cook until heated through.",
                       OrdinalNumber = 3 },
            new Step {
              Content =
                  "Drizzle with lemon juice and season with salt and pepper.",
              OrdinalNumber = 4
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("f9a698fa-7104-47fe-afec-d983cfa8d637"),
      Title = "Spicy Flower Snail Stew",
      Description = "A hearty stew with flower snails simmered in a spicy " +
                    "tomato-based sauce.",
      ImageUrl = "https://homemadeinhk.wordpress.com/wp-content/uploads/2014/" +
                 "01/photo-3-54.jpg",
      Serves = 3, CookTime = "40m",
      Ingredients =
          [
            "250g Flower Snails", "1 can (400g) Diced Tomatoes",
            "1 Onion, chopped", "2 cloves Garlic, minced", "1 tsp Chili Flakes",
            "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Sauté chopped onion and garlic until soft.",
                       OrdinalNumber = 1 },
            new Step { Content = "Add flower snails and cook for 5 minutes.",
                       OrdinalNumber = 2 },
            new Step { Content = "Pour in diced tomatoes and add chili flakes.",
                       OrdinalNumber = 3 },
            new Step {
              Content =
                  "Simmer for 25 minutes and season with salt and pepper.",
              OrdinalNumber = 4
            }
          ]
    },

    // Bell Pepper Recipes
    new Recipe {
      Id = Guid.Parse("973ce668-0ef9-474b-b1de-3de84f817075"),
      Title = "Stuffed Bell Peppers",
      Description = "Colorful bell peppers stuffed with a savory mix of " +
                    "rice, meat, and vegetables.",
      ImageUrl = "https://tyberrymuch.com/wp-content/uploads/2020/09/" +
                 "vegan-stuffed-peppers-recipe-735x735.jpg",
      Serves = 4, CookTime = "35m",
      Ingredients =
          [
            "4 Bell Peppers, tops removed", "1 cup Rice, cooked",
            "200g Ground Beef", "1 Onion, diced", "1 Tomato, chopped",
            "1 tsp Olive Oil", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Preheat oven to 375°F.", OrdinalNumber = 1 },
            new Step { Content = "Hollow out bell peppers.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Mix cooked rice, ground beef, onion, and tomato.",
                       OrdinalNumber = 3 },
            new Step {
              Content =
                  "Stuff peppers with the mixture and bake for 25 minutes.",
              OrdinalNumber = 4
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("a4e35661-acec-4c7d-9026-6f6c2d1d4607"),
      Title = "Bell Pepper Fajitas",
      Description = "Sizzling bell pepper fajitas with onions and marinated " +
                    "chicken—perfect for a quick dinner.",
      ImageUrl =
          "https://www.foodandwine.com/thmb/PtpbKVWUpLOKmFRuDQbptohVrRM=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "201403-xl-chicken-fajitas-with-bell-peppers-" +
          "77a7e7e8e4c34ef18f42b9c51144de76.jpg",
      Serves = 3, CookTime = "20m",
      Ingredients =
          [
            "2 Bell Peppers, sliced", "1 Onion, sliced",
            "200g Chicken Breast, thinly sliced", "1 tbsp Fajita Seasoning",
            "1 tbsp Olive Oil", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Slice bell peppers and onion into strips.",
                       OrdinalNumber = 1 },
            new Step { Content = "Toss chicken with fajita seasoning.",
                       OrdinalNumber = 2 },
            new Step {
              Content =
                  "Sauté chicken until nearly cooked, then add vegetables.",
              OrdinalNumber = 3
            },
            new Step {
              Content =
                  "Cook for an additional 5 minutes until vegetables soften.",
              OrdinalNumber = 4
            }
          ]
    },

    // Lemongrass Recipes
    new Recipe {
      Id = Guid.Parse("1b4d8f34-5a70-4370-9255-6e7f6dd3e5f0"),
      Title = "Lemongrass Chicken Soup",
      Description = "A fragrant chicken soup infused with lemongrass and " +
                    "fresh herbs for a warming meal.",
      ImageUrl =
          "https://www.allrecipes.com/thmb/n5SFaJDeLLZOIVIt6GMRV_z5nak=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "4549752-e276cb4c148043458e61cacd14eb2f97.jpg",
      Serves = 4, CookTime = "30m",
      Ingredients =
          [
            "500g Chicken Thighs, boneless", "2 Stalks Lemongrass, bruised",
            "1 Liter Chicken Broth", "1 Onion, chopped",
            "2 cloves Garlic, minced", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content =
                           "Bruise lemongrass and simmer in chicken broth.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Add chopped onion, garlic, and chicken thighs.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Simmer for 20 minutes until chicken is cooked.",
                       OrdinalNumber = 3 },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("c93e0c70-8ea9-4606-9f84-bfbd3782c78b"),
      Title = "Grilled Lemongrass Beef Skewers",
      Description = "Tender beef skewers marinated with lemongrass and " +
                    "grilled to perfection.",
      ImageUrl =
          "https://www.seriouseats.com/thmb/G5C2-Pr4m6ET_yEn7GumPe6H0WA=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "__opt__aboutcom__coeus__resources__content_migration__serious_" +
          "eats__seriouseats.com__images__2015__06__20150530-shao-z-beef-" +
          "skewers-4-be3e8f2aa59d40a9ab4f4c8b6e110bfb.jpg",
      Serves = 3, CookTime = "25m",
      Ingredients =
          [
            "300g Beef Sirloin, cubed", "2 Stalks Lemongrass, finely chopped",
            "1 tbsp Soy Sauce", "1 tbsp Olive Oil", "1 Garlic Clove, minced",
            "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Marinate beef with lemongrass, garlic, soy " +
                                 "sauce, and olive oil.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Skewer the beef cubes and grill for 15 minutes.",
                       OrdinalNumber = 2 },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 3 }
          ]
    },

    // Prok Ribs Recipes (Pork Ribs)
    new Recipe {
      Id = Guid.Parse("cc1cca39-2c3c-4dce-a487-466d52faaf39"),
      Title = "BBQ Pork Ribs",
      Description = "Succulent pork ribs slathered in barbecue sauce and " +
                    "slow-cooked to perfection.",
      ImageUrl =
          "https://www.allrecipes.com/thmb/IWVelWahUb2gQxixWJC2N-HXp0k=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "22469-Barbecue-Ribs-ddmfs-2x1-210-" +
          "e799db142f594b00bb317bb357d0971c.jpg",
      Serves = 4, CookTime = "2h",
      Ingredients =
          [
            "1kg Pork Ribs", "1 cup BBQ Sauce", "1 tbsp Brown Sugar",
            "1 tsp Smoked Paprika", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Preheat oven to 300°F.", OrdinalNumber = 1 },
            new Step { Content = "Season pork ribs with salt, pepper, brown " +
                                 "sugar, and paprika.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Coat ribs with BBQ sauce and bake for 2 hours.",
                       OrdinalNumber = 3 },
            new Step { Content = "Finish under the broiler for 5 minutes to " +
                                 "caramelize the sauce.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("50aaa4bd-d682-4df6-bb36-682915b91737"),
      Title = "Slow Cooked Pork Ribs",
      Description = "Tender pork ribs slow cooked in a rich, savory sauce " +
                    "for an unforgettable flavor.",
      ImageUrl =
          "https://www.allrecipes.com/thmb/Yk6XPvsjJwPiq6Ed8UxejK0oKFE=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "228498-slow-cooker-baby-back-ribs-DDMFS-008-4x3-" +
          "052755bf2a444771918ef432757479a7.jpg",
      Serves = 4, CookTime = "3h",
      Ingredients =
          [
            "1kg Pork Ribs", "1 cup Tomato Sauce", "2 tbsp Apple Cider Vinegar",
            "1 tsp Garlic Powder", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step {
              Content =
                  "Season pork ribs with salt, pepper, and garlic powder.",
              OrdinalNumber = 1
            },
            new Step { Content =
                           "Combine tomato sauce and vinegar in a slow cooker.",
                       OrdinalNumber = 2 },
            new Step { Content = "Add ribs and cook on low for 3 hours.",
                       OrdinalNumber = 3 },
            new Step { Content = "Serve with extra sauce drizzled on top.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("9e8cbee2-9275-4d8d-9e0a-7454e4cb525c"),
      Title = "Spicy Korean Pork Ribs",
      Description = "Korean-style pork ribs with a spicy gochujang glaze, " +
                    "delivering a sweet, savory, and fiery kick.",
      ImageUrl =
          "https://www.souschef.co.uk/cdn/shop/articles/" +
          "timthumb_34217f00-4032-414c-8807-b8389009f024.jpg?v=1531317210",
      Serves = 4, CookTime = "1h 30m",
      Ingredients =
          [
            "1kg Pork Ribs", "1/2 cup Gochujang (Korean chili paste)",
            "1/4 cup Soy Sauce", "2 tbsp Rice Vinegar", "2 tbsp Honey",
            "1 tbsp Sesame Oil", "2 cloves Garlic, minced",
            "1 inch Ginger, grated", "Sesame Seeds (for garnish)",
            "Green Onions, chopped (for garnish)"
          ],
      Steps =
          [
            new Step { Content = "Cut the pork ribs into individual pieces " +
                                 "or smaller sections.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "In a bowl, whisk together gochujang, soy sauce, " +
                           "rice vinegar, honey, sesame oil, minced garlic, " +
                           "and grated ginger to create the marinade.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Marinate the ribs in the mixture for at least 30 " +
                           "minutes (or up to overnight in the refrigerator).",
                       OrdinalNumber = 3 },
            new Step { Content = "Preheat oven to 350°F (175°C).",
                       OrdinalNumber = 4 },
            new Step {
              Content =
                  "Arrange ribs on a baking sheet lined with parchment paper.",
              OrdinalNumber = 5
            },
            new Step {
              Content =
                  "Bake for 45-60 minutes, basting with the marinade every " +
                  "15 minutes, until ribs are cooked through and tender.",
              OrdinalNumber = 6
            },
            new Step { Content = "Optional: Finish under the broiler for a " +
                                 "few minutes for extra char.",
                       OrdinalNumber = 7 },
            new Step { Content = "Garnish with sesame seeds and chopped " +
                                 "green onions before serving.",
                       OrdinalNumber = 8 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("de3b30cd-cc23-4c6e-a1ca-262f076d009b"),
      Title = "Honey Garlic Glazed Pork Ribs",
      Description = "Pork ribs coated in a sticky and sweet honey garlic " +
                    "glaze, baked until caramelized and delicious.",
      ImageUrl =
          "https://www.ontario.ca/foodland/_next/" +
          "image?url=https%3A%2F%2Fwww.ontario.ca%2Ffiles%2Fs3fs-public%" +
          "2Ffoodland%2F2024-09%2Fhoney-garlic-ribs.jpg&w=1200&q=75",
      Serves = 4, CookTime = "2h 15m",
      Ingredients =
          [
            "1kg Pork Ribs", "1/2 cup Honey", "1/4 cup Soy Sauce",
            "4 cloves Garlic, minced", "1 tbsp Rice Vinegar",
            "1 tsp Ginger, grated", "1/2 tsp Red Pepper Flakes (optional)",
            "Salt and Pepper to taste"

          ],
      Steps =
          [new Step { Content = "Preheat oven to 325°F (160°C).",
                      OrdinalNumber = 1 },
           new Step { Content =
                          "Season pork ribs generously with salt and pepper.",
                      OrdinalNumber = 2 },
           new Step { Content = "Place ribs in a baking dish.",
                      OrdinalNumber = 3 },
           new Step { Content = "In a small bowl, whisk together honey, soy " +
                                "sauce, minced garlic, rice vinegar, grated " +
                                "ginger, and red pepper flakes (if using).",
                      OrdinalNumber = 4 },
           new Step { Content = "Pour half of the honey garlic glaze over " +
                                "the ribs, ensuring they are well coated.",
                      OrdinalNumber = 5 },
           new Step {
             Content =
                 "Cover the baking dish with foil and bake for 1.5 hours.",
             OrdinalNumber = 6
           },
           new Step { Content =
                          "Remove foil, baste with the remaining glaze, and " +
                          "bake uncovered for another 30-45 minutes, or " +
                          "until ribs are tender and the glaze is caramelized.",
                      OrdinalNumber = 7 },
           new Step { Content = "Let rest for 10 minutes before serving.",
                      OrdinalNumber = 8 }]
    }

    // Beef Recipes
    new Recipe {
      Id = Guid.Parse("b5c2bfe4-d3e9-48d5-a9a5-c92c445bc9e5"),
      Title = "Beef Stroganoff",
      Description = "Classic beef stroganoff with tender strips of beef in a " +
                    "creamy mushroom sauce.",
      ImageUrl =
          "https://www.allrecipes.com/thmb/mSWde3PHTu-fDkbvWBw0D1JlS8U=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "25202beef-stroganoff-iii-ddmfs-3x4-233-" +
          "0f26fa477e9c446b970a32502468efc6.jpg",
      Serves = 4, CookTime = "35m",
      Ingredients =
          [
            "500g Beef Sirloin, thinly sliced", "200g Mushrooms, sliced",
            "1 Onion, sliced", "1 cup Sour Cream", "2 tbsp Flour",
            "1 cup Beef Broth", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Sauté beef strips until browned.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Add onions and mushrooms; cook until softened.",
                       OrdinalNumber = 2 },
            new Step {
              Content = "Stir in flour and beef broth; simmer for 15 minutes.",
              OrdinalNumber = 3
            },
            new Step { Content = "Mix in sour cream and heat through.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("72bd5fac-18d8-496a-a5ab-952a2a9a43e5"),
      Title = "Grilled Beef Steak with Herbs",
      Description = "Juicy beef steak grilled with a mix of fresh herbs for " +
                    "a flavorful meal.",
      ImageUrl = "https://encrypted-tbn0.gstatic.com/" +
                 "images?q=tbn:ANd9GcSe_SrEHEh4hWd43bqPXvCDI9ziJNEfmWXZxQ&s",
      Serves = 2, CookTime = "20m",
      Ingredients =
          [
            "300g Beef Steak", "1 tbsp Olive Oil", "1 Garlic Clove, minced",
            "1 tsp Rosemary", "1 tsp Thyme", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step {
              Content =
                  "Marinate steak with olive oil, garlic, rosemary, and thyme.",
              OrdinalNumber = 1
            },
            new Step { Content = "Grill steak to desired doneness.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Let rest for 5 minutes, then slice and serve.",
                       OrdinalNumber = 3 }
          ]
    },

    // Ground Pork Recipes
    new Recipe {
      Id = Guid.Parse("4e1723cb-2227-43f9-a700-41485450a139"),
      Title = "Ground Pork Dumplings",
      Description = "Delicious dumplings filled with seasoned ground pork, " +
                    "perfect for dipping.",
      ImageUrl =
          "https://www.allrecipes.com/thmb/tCt2q6Sly6Spyu7fsM5c3Vxs8DI=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "14759-pork-dumplings-DDMFS-4x3-f87c9459ec73475f9dcab4cc651c46d3.jpg",
      Serves = 4, CookTime = "40m",
      Ingredients =
          [
            "300g Ground Pork", "20 Dumpling Wrappers", "1/2 Cabbage, shredded",
            "2 cloves Garlic, minced", "1 tbsp Soy Sauce", "Pinch of Salt",
            "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Mix ground pork with shredded cabbage, " +
                                 "garlic, and soy sauce.",
                       OrdinalNumber = 1 },
            new Step {
              Content =
                  "Fill dumpling wrappers with the mixture and fold tightly.",
              OrdinalNumber = 2
            },
            new Step { Content = "Steam dumplings for 15 minutes.",
                       OrdinalNumber = 3 },
            new Step { Content = "Serve with your favorite dipping sauce.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("b87b86bd-7f60-407e-a106-10f049890c1d"),
      Title = "Spicy Ground Pork Noodles",
      Description = "A hearty noodle dish tossed with spicy ground pork and " +
                    "crisp vegetables.",
      ImageUrl = "https://encrypted-tbn0.gstatic.com/" +
                 "images?q=tbn:ANd9GcTs6QeAYCdEUS8Mo7wrHP1L6nh7hRA65Wzrog&s",
      Serves = 3, CookTime = "30m",
      Ingredients =
          [
            "250g Ground Pork", "200g Noodles, cooked",
            "1 Red Bell Pepper, sliced", "1 Onion, sliced",
            "2 tbsp Chili Garlic Sauce", "1 tbsp Soy Sauce", "Pinch of Salt"
          ],
      Steps =
          [
            new Step { Content =
                           "Cook noodles according to package instructions.",
                       OrdinalNumber = 1 },
            new Step { Content = "Sauté ground pork with sliced onion and " +
                                 "bell pepper until browned.",
                       OrdinalNumber = 2 },
            new Step { Content = "Mix in chili garlic sauce and soy sauce, " +
                                 "then toss with noodles.",
                       OrdinalNumber = 3 },
            new Step { Content = "Serve hot.", OrdinalNumber = 4 }
          ]
    },

    // Chicken Heart Recipes
    new Recipe {
      Id = Guid.Parse("70b9fd56-2ea3-4fc9-9477-75d7749fc259"),
      Title = "Chicken Heart Skewers",
      Description = "Grilled chicken hearts marinated in a tangy sauce and " +
                    "skewered for an adventurous appetizer.",
      ImageUrl =
          "https://www.foodnetwork.com/content/dam/images/food/fullset/2021/" +
          "10/25/YK504_Chicken-Heart-Yakitori-Skewers_s4x3.jpg",
      Serves = 3, CookTime = "25m",
      Ingredients =
          [
            "300g Chicken Hearts, cleaned", "2 tbsp Soy Sauce",
            "1 tbsp Olive Oil", "1 Garlic Clove, minced", "Pinch of Salt",
            "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Clean and marinate chicken hearts with soy " +
                                 "sauce, olive oil, and garlic.",
                       OrdinalNumber = 1 },
            new Step { Content = "Skewer the hearts evenly.",
                       OrdinalNumber = 2 },
            new Step { Content =
                           "Grill for 15 minutes until charred and tender.",
                       OrdinalNumber = 3 },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("31fd92db-c271-421a-adad-0b8c8455972e"),
      Title = "Sautéed Chicken Hearts with Herbs",
      Description = "Tender chicken hearts quickly sautéed with fresh " +
                    "herbs—a unique and savory appetizer.",
      ImageUrl = "https://sandersonfarms.com/wp-content/uploads/2017/04/" +
                 "Recipes_Chicken_Hearts_with_Onions_and_Mushroom_Hero_" +
                 "824x758-720x.jpg",
      Serves = 2, CookTime = "20m",
      Ingredients =
          [
            "250g Chicken Hearts, trimmed", "1 tbsp Olive Oil",
            "1 Garlic Clove, minced", "1 tsp Thyme", "Pinch of Salt",
            "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Clean and slice chicken hearts.",
                       OrdinalNumber = 1 },
            new Step {
              Content =
                  "Heat olive oil in a pan and sauté garlic until fragrant.",
              OrdinalNumber = 2
            },
            new Step { Content =
                           "Add chicken hearts and thyme; cook until tender.",
                       OrdinalNumber = 3 },
            new Step { Content = "Season with salt and pepper before serving.",
                       OrdinalNumber = 4 }
          ]
    },

    // Shrimp Recipes
    new Recipe {
      Id = Guid.Parse("aec08a9e-b7ce-466c-a7e1-877b7c94dcc9"),
      Title = "Garlic Shrimp Pasta",
      Description = "A quick pasta dish tossed with garlic, succulent " +
                    "shrimp, and fresh herbs.",
      ImageUrl = "https://myeagereats.com/wp-content/uploads/2021/05/" +
                 "wp-1620987156623.jpg",
      Serves = 3, CookTime = "20m",
      Ingredients =
          [
            "250g Shrimp, peeled", "200g Pasta, cooked al dente",
            "3 cloves Garlic, minced", "2 tbsp Olive Oil", "1 tbsp Lemon Juice",
            "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Cook pasta until al dente and set aside.",
                       OrdinalNumber = 1 },
            new Step { Content = "Sauté garlic in olive oil until fragrant.",
                       OrdinalNumber = 2 },
            new Step { Content = "Add shrimp and cook until they turn pink.",
                       OrdinalNumber = 3 },
            new Step { Content = "Toss pasta with shrimp, drizzle lemon " +
                                 "juice, and season to taste.",
                       OrdinalNumber = 4 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("485ce44f-4764-40e8-b531-86771e9ab0eb"),
      Title = "Spicy Shrimp Tacos",
      Description = "Tacos filled with spicy shrimp, crisp slaw, and a zesty " +
                    "lime dressing.",
      ImageUrl = "https://bingeworthybites.com/wp-content/uploads/2020/01/" +
                 "Spicy-Shrimp-Tacos.jpg",
      Serves = 2, CookTime = "25m",
      Ingredients =
          [
            "200g Shrimp, deveined", "4 Small Tortillas", "1 cup Cabbage Slaw",
            "1 tbsp Chili Powder", "1 Lime, juiced", "1 tbsp Olive Oil",
            "Pinch of Salt"
          ],
      Steps =
          [
            new Step { Content = "Season shrimp with chili powder and salt.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Sauté shrimp in olive oil until cooked through.",
                       OrdinalNumber = 2 },
            new Step { Content = "Assemble tacos with shrimp, cabbage slaw, " +
                                 "and a squeeze of lime.",
                       OrdinalNumber = 3 }
          ]
    },
    new Recipe {
      Id = Guid.Parse("ef388ae9-c38c-4b0b-9ef4-eb076b313b49"),
      Title = "Coconut Curry Shrimp",
      Description = "Tender shrimp simmered in a fragrant and creamy coconut " +
                    "curry sauce, served with rice.",
      ImageUrl = "https://encrypted-tbn0.gstatic.com/" +
                 "images?q=tbn:ANd9GcTR4TVLeZEYZo0RM5p_Y9Z592ZJyVBFhzjMgg&s",
      Serves = 4, CookTime = "30m",
      Ingredients =
          [
            "500g Shrimp, peeled and deveined", "1 can (400ml) Coconut Milk",
            "1 tbsp Red Curry Paste", "1 Onion, chopped",
            "2 cloves Garlic, minced", "1 inch Ginger, grated",
            "1 Red Bell Pepper, sliced", "1/2 cup Vegetable Broth",
            "2 tbsp Fish Sauce (or Soy Sauce)", "1 tbsp Brown Sugar",
            "1 tbsp Lime Juice", "2 tbsp Vegetable Oil",
            "Fresh Cilantro, chopped (for garnish)", "Cooked Rice, for serving"
          ],
      Steps =
          [
            new Step { Content = "Heat vegetable oil in a large pan or wok " +
                                 "over medium-high heat.",
                       OrdinalNumber = 1 },
            new Step { Content = "Add chopped onion and sauté until " +
                                 "softened, about 3 minutes.",
                       OrdinalNumber = 2 },
            new Step { Content = "Add minced garlic and grated ginger, and " +
                                 "cook for another minute until fragrant.",
                       OrdinalNumber = 3 },
            new Step { Content =
                           "Stir in the red curry paste and cook for 1 minute.",
                       OrdinalNumber = 4 },
            new Step {
              Content = "Add the sliced bell pepper and cook for 2-3 minutes.",
              OrdinalNumber = 5
            },
            new Step { Content = "Pour in the coconut milk and vegetable " +
                                 "broth. Bring to a simmer.",
                       OrdinalNumber = 6 },
            new Step {
              Content =
                  "Stir in the fish sauce (or soy sauce) and brown sugar.",
              OrdinalNumber = 7
            },
            new Step { Content =
                           "Add the shrimp and cook until they turn pink and " +
                           "are cooked through, about 3-5 minutes.",
                       OrdinalNumber = 8 },
            new Step { Content = "Stir in the lime juice.", OrdinalNumber = 9 },
            new Step {
              Content =
                  "Serve hot over cooked rice, garnished with fresh cilantro.",
              OrdinalNumber = 10
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("04788402-e7b0-4a60-a550-9073859a2857"),
      Title = "Shrimp Scampi with Zucchini Noodles",
      Description =
          "A lighter take on classic shrimp scampi, using zucchini noodles " +
          "instead of pasta for a healthy and flavorful meal.",
      ImageUrl =
          "https://www.thespruceeats.com/thmb/qWmjcmUZxtRwve7YJRhUPzBjTt8=/" +
          "1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/" +
          "zucchini-noodles-with-shrimp-scampi-4154511-" +
          "c81b9cc83fcf467f867ee7d3e9af72c5.jpg",
      Serves = 2, CookTime = "20m",
      Ingredients =
          [
            "300g Shrimp, peeled and deveined",
            "2 medium Zucchinis, spiralized", "4 cloves Garlic, minced",
            "1/4 cup Dry White Wine (like Sauvignon Blanc or Pinot Grigio)",
            "2 tbsp Olive Oil", "2 tbsp Butter", "1 tbsp Lemon Juice",
            "1/4 cup chopped Fresh Parsley", "Red Pepper Flakes, to taste",
            "Salt and Black Pepper, to taste"
          ],
      Steps =
          [new Step { Content =
                          "Spiralize the zucchinis into noodles.  If you " +
                          "don't have a spiralizer, use a vegetable peeler " +
                          "to create long, thin ribbons.",
                      OrdinalNumber = 1 },
           new Step {
             Content =
                 "Pat the zucchini noodles dry with paper towels to remove " +
                 "excess moisture. This will help them cook better.",
             OrdinalNumber = 2
           },
           new Step { Content = "Heat olive oil and butter in a large " +
                                "skillet over medium-high heat.",
                      OrdinalNumber = 3 },
           new Step { Content = "Add minced garlic and cook until fragrant, " +
                                "about 30 seconds. Don't let it burn.",
                      OrdinalNumber = 4 },
           new Step { Content =
                          "Add the shrimp to the skillet and cook for 2-3 " +
                          "minutes per side, until pink and cooked through.",
                      OrdinalNumber = 5 },
           new Step { Content =
                          "Pour in the white wine and let it simmer for a " +
                          "minute, allowing the alcohol to evaporate.",
                      OrdinalNumber = 6 },
           new Step {
             Content =
                 "Add the zucchini noodles to the skillet and toss gently to " +
                 "coat them with the sauce. Cook for 2-3 minutes, until they " +
                 "are slightly softened but still have some bite.",
             OrdinalNumber = 7
           },
           new Step { Content = "Stir in the lemon juice, chopped parsley, " +
                                "and red pepper flakes (if using).",
                      OrdinalNumber = 8 },
           new Step { Content = "Season with salt and black pepper to taste.",
                      OrdinalNumber = 9 },
           new Step { Content = "Serve immediately.", OrdinalNumber = 10 }]
    }

    // Lettuce Recipes
    new Recipe {
      Id = Guid.Parse("96c0c736-c0bc-464a-b57f-e98e9b36ff11"),
      Title = "Crispy Lettuce Wraps",
      Description = "Fresh lettuce leaves filled with a savory mix of " +
                    "vegetables and ground pork for a light meal.",
      ImageUrl = "https://cdn.apartmenttherapy.info/image/upload/v1715705702/" +
                 "k/Photo/Recipes/2024-05-chicken-lettuce-wraps/" +
                 "chicken-lettuce-wraps-315.jpg",
      Serves = 3, CookTime = "15m",
      Ingredients =
          [
            "1 Head Lettuce, leaves separated", "200g Ground Pork",
            "1 Carrot, julienned", "1 Red Bell Pepper, diced",
            "1 tbsp Soy Sauce", "Pinch of Salt", "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Cook ground pork with diced carrot and " +
                                 "bell pepper until browned.",
                       OrdinalNumber = 1 },
            new Step { Content = "Season with soy sauce, salt, and pepper.",
                       OrdinalNumber = 2 },
            new Step {
              Content =
                  "Spoon the mixture into crisp lettuce leaves and serve.",
              OrdinalNumber = 3
            }
          ]
    },
    new Recipe {
      Id = Guid.Parse("40ecea83-f36e-413e-a5e0-e4d7a9a4190a"),
      Title = "Lettuce Caesar Salad",
      Description = "A refreshing twist on the classic Caesar salad using " +
                    "crisp lettuce and a tangy dressing.",
      ImageUrl = "hhttps://apleasantlittlekitchen.com/wp-content/uploads/" +
                 "2021/02/Herbed-Butter-Lettuce-Salad-1-of-1-scaled.jpg",
      Serves = 2, CookTime = "10m",
      Ingredients =
          [
            "1 Head Lettuce, chopped", "50g Parmesan Cheese, grated",
            "2 tbsp Caesar Dressing", "1 Crouton Pack", "Pinch of Salt",
            "Pinch of Pepper"
          ],
      Steps =
          [
            new Step { Content = "Toss chopped lettuce with Caesar dressing.",
                       OrdinalNumber = 1 },
            new Step { Content =
                           "Top with grated Parmesan cheese and croutons.",
                       OrdinalNumber = 2 },
            new Step { Content = "Season lightly with salt and pepper.",
                       OrdinalNumber = 3 }
          ]
    }
  ];
}
