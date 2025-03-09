using RecipeService.Domain.Entities;
using static System.Net.WebRequestMethods;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataDuc
{
    public static List<Recipe> Recipes = [
        new Recipe{
            Id = Guid.Parse("262c5dc6-2b2f-4b17-acb7-d8c33bc67c1b"),
            Title = "Chinese-style Braised WHite Radish",
            Description = "I love daikon radish. But mostly, I use it as a garnish or a side dish. I never thought that radish alone can make such a tasty recipe. Salads are great, but you will fall in love with this vegan recipe once you try it. The key to making tender and savory radish is braising. Braising is a common cooking method in Asian kitchens. Similar to stewing, you cook with medium-low heat for a longer time. ",
            ImageUrl = "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/Chinese-braised-daikon-radish-2.jpg",
            Serves = 2,
            CookTime = "30m",
            Ingredients = ["1 White Radish", "1 Scallion", "50 grams of Fresh ginger", "2 teaspoons of Olive oil"],
            Steps = [
                new Step{
                    Content = "Peel the daikon radish and roll cut into small pieces.",
                    AttachedImageUrls = [
                        "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/How-to-make-braised-daikon-radish-1.jpg",
                        "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/How-to-make-braised-daikon-radish-2.jpg"
                    ]
                },
                new Step{
                    Content = "Rinse the scallion with water. Then remove the root, and separate the white part from the green. Cut the white part into approximately 2-inch pieces. And dice the green part finely."
                },
                new Step{
                    Content = "Wash the fresh ginger and slice it thinly. Set aside for later."
                },
                new Step{
                    Content = "On a pan, heat two teaspoons of olive oil over medium-high heat. Then add ginger slices and the white stems of scallion. Sauté and coat the pan with oil evenly."
                },
                new Step{
                    Content = "When the scallion and ginger start to fragrant, add the daikon radish blocks to the pan. Stir-fry for 3 to 5 minutes until the surface of the radish turns slightly brown.",
                    AttachedImageUrls = [
                        "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/How-to-make-braised-daikon-radish-3.jpg"
                    ]
                },
                new Step{
                    Content = "Add half a cup of water or just enough to cover the ingredients. Pour light soy sauce, dark soy sauce, soybean paste, and cooking wine into the pan. Stir until the water boils.",
                    AttachedImageUrls = [
                        "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/How-to-make-braised-daikon-radish-5.jpg"
                    ]
                },
                new Step{
                    Content = "Turn the heat down to medium-low, cover the lid and simmer for 10 to 12 minutes. Check on the amount of sauce occasionally to avoid burning. Add water if needed.",
                    AttachedImageUrls = [
                        "https://cdn.shortpixel.ai/spai/w_813+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/How-to-make-braised-daikon-radish-6.jpg"
                    ]
                },
                new Step{
                    Content = "Open the lid. If there’s too much liquid, turn the heat to medium-high and cook until the sauce thickens. Sprinkle the green scallion. Transfer to a plate and serve with rice or noodle.",
                    AttachedImageUrls = [
                        "https://cdn.shortpixel.ai/spai/w_875+q_lossy+ret_img+to_webp/cooklikeasian.com/wp-content/uploads/2021/06/Chinese-braised-daikon-radish-1.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("310fd400-f2b6-4b22-baa9-68792bc84312"),
            Title = "Blueberry Oat Breakfast Muffins",
            Description = "These quick and easy blueberry oat breakfast muffins are a sweet, comforting way to start your day. Mashed banana and sweet potato puree take the place of eggs and added sugar in this vegan recipe. Picky-kid-tested and plant-based-mom-approved, these moist, low-fat, fruit-filled delights are great for any occasion!",
            ImageUrl = "https://www.forksoverknives.com/uploads/Blueberry_Oats_Muffin3873_WP.jpg",
            Serves = 1,
            CookTime = "40m",
            Ingredients = [
                "1 medium banana, mashed",
                "One (15-ounce) can sweet potato puree",
                "¼ cup pure maple syrup",
                "1 teaspoon vanilla extract",
                "2 cups whole oat flour",
                "½ teaspoon baking soda",
                "½ teaspoon baking powder",
                "½ teaspoon salt",
                "1 teaspoon ground cinnamon",
                "½ teaspoon ground nutmeg",
                "¼ teaspoon ground ginger",
                "1 cup fresh or frozen blueberries"
            ],
            Steps = [
                new Step{
                    Content = "Preheat the oven to 375°F. In a large bowl, combine the mashed banana, sweet potato puree, maple syrup, and vanilla."
                },
                new Step{
                    Content = "In a small bowl, combine the oat flour, baking soda, baking powder, salt, cinnamon, nutmeg, and ginger. Transfer this mixture to the large bowl of wet ingredient, and mix together gently until well combined. Avoid over-mixing to prevent toughness in the final product. Fold in the blueberries."
                },
                new Step{
                    Content = "Spoon the batter into silicone muffin cups and bake for 20 minutes or until the muffins are lightly browned. Remove from the oven and let cool for 5 minutes. Store the muffins in an airtight container."
                },
            ],
        },
        new Recipe{
            Id = Guid.Parse("0ec9266c-1380-480e-9be7-b4f86e5b2252"),
            Title = "Paula Deen's Okra Fritters",
            Description = "A classic Southern snack featuring crispy okra fritters with a hint of seasoning, inspired by Paula Deen's famous recipe.",
            ImageUrl = "https://cdn.pauladeen.com/wp-content/uploads/2017/10/31070246/okra_fritters_1.jpg",
            Serves = 10,
            CookTime = "10m",
            Ingredients = [
                "1 1/2 cups self-rising flour",
                "1 tablespoon Paula Deen's House Seasoning",
                "1 cup buttermilk",
                "1 egg",
                "4 cups chopped okra",
                "Vegetable oil for frying"
            ],
            Steps = [
                new Step{
                    Content = "Heat the vegetable oil in a cast iron Dutch oven to 350°F."
                },
                new Step{
                    Content = "In a large bowl, whisk together the self-rising flour, Paula Deen's House Seasoning, buttermilk, and egg until smooth."
                },
                new Step{
                    Content = "Fold in the chopped okra until evenly coated."
                },
                new Step{
                    Content = "Using a tablespoon, scoop portions of the mixture into the hot oil. Fry the fritters, flipping frequently for even browning, about 12-15 minutes total."
                },
                new Step{
                    Content = "Remove with a slotted spoon, drain on paper towels, and serve hot."
                },
            ],
        },
        new Recipe{
            Id = Guid.Parse("e348525a-d9bd-4d0a-a7ee-0d2a9c9ae1cf"),
            Title = "Spicy Stir-Fried Chinese Long Beans",
            Description = "A real-world Chinese stir-fry featuring long beans, garlic, shallots, and a savory sauce with a kick of spice. Optionally enhanced with crispy bacon.",ImageUrl = "https://walczakrecipes.blog/wp-content/uploads/2019/02/spicy-green-beans.png",
            Serves = 6,
            CookTime = "8m",
            Ingredients = [
                "2 tbsp toban djan (Chinese chili bean sauce)",
                "2 tbsp oyster sauce (or vegetarian oyster sauce)",
                "1 tsp sugar",
                "1 tbsp cooking oil (if using center cut bacon)",
                "6 slices thick center cut bacon, cut into bite sizes (optional)",
                "3 cloves garlic, chopped",
                "1 small shallot, chopped (or 1/4 small onion)",
                "1 lb long beans, cut into 2-inch pieces",
                "1/4 cup chicken stock"
            ],
            Steps = [
                new Step{
                    Content = "In a small bowl, combine the toban djan, oyster sauce, and sugar to form the sauce. Set aside."
                },
                new Step{
                    Content = "Heat a wok over high heat. Add the oil and bacon (if using) and fry until the bacon edges are crispy and fat is released."
                },
                new Step{
                    Content = "Add the chopped garlic and shallot; stir fry for 30 seconds to 1 minute until fragrant."
                },
                new Step{
                    Content = "Pour in the prepared sauce and stir fry for another 30 seconds to 1 minute, being careful not to burn the garlic."
                },
                new Step{
                    Content = "Add the long beans and chicken stock, then cover with a lid and let steam for 5–6 minutes until the beans are tender-crisp."
                },
                new Step{
                    Content = "Transfer to a serving plate and serve hot."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("2b3c4d5e-6f7a-8b9c-0d1e-2f3a4b5c6d7e"),
            Title = "Stir-Fried Garlic String Beans (3 Ingredients Only)",
            Description = "A super simple stir-fry featuring string beans, garlic, and salt. Crispy on the outside, tender on the inside—just like the Chinese restaurant favorite.",
            ImageUrl = "https://lh4.googleusercontent.com/y3I3rudXgFO9zhiJCEV-iH3W_0rPbyKhk9nQo3P5A0cueb29WTlHBHtwkPXxELQbDmPpjtqDDYhTiqVz7oC2IDnznoTpHAtkgdx_9zfP8ZDPlex5Pa9iVoJzZKnJUDhpgugainU-=s0",
            Serves = 3,
            CookTime = "15m",
            Ingredients = [
                "1 lb string beans, trimmed",
                "8-10 cloves garlic, minced",
                "1/2 tsp salt",
                "1/2 cup vegetable oil (for frying)"
            ],
            Steps = [
                new Step{
                    Content = "Trim the ends of the string beans, rinse, and pat them dry.",
                    AttachedImageUrls = [
                        "https://lh4.googleusercontent.com/nFCsTAhZKkdsKlaYek2nTLWX7uCKN_RTc5VEVUiquwlsGmfN_7357ce5xk5Nx9I1o0OXTo4_LPXugiuoF6oKZVWB0dGzLyXyKMt9To-0xVxPBPHjOfKFNffHnZUgwM16-73-citd=s0"
                    ]
                },
                new Step{
                    Content = "Heat 1/2 cup of vegetable oil in a small shallow pan or wok over medium-high heat. Fry the string beans in batches for about 30–60 seconds per side until they’re lightly blistered and crispy. Remove and drain on paper towels."
                },
                new Step{
                    Content = "In a clean pan, add 1 tbsp of oil over medium-high heat. Sauté the minced garlic until fragrant, about 1 minute."
                },
                new Step{
                    Content = "Return the fried string beans to the pan, sprinkle with salt, and toss to combine. Stir-fry for another minute so the flavors meld.",
                    AttachedImageUrls = [
                        "https://lh5.googleusercontent.com/AHJuRuk0b6v6idi3k63AEbs1J7fElhkIlQ3Ph16wSu5H0Ie7VhX9sLw17qOrWZcoO4kIkuQKbcTxU5KGUiZo7jc8XBVBa0zRSMa0eCsmdEqLSoZY5Qpet2IRFgsPp8I0osAl9i7t=s0"
                    ]
                },
                new Step{
                    Content = "Transfer to a serving plate and enjoy hot as a side dish!",
                    AttachedImageUrls = [
                        "https://lh3.googleusercontent.com/rY0gVqk-o6o4vGKe93QBjO6csEMKLQ1EZSciCD5fORVNyeZjjZ0O07i_7yqoiGtmJmPjET3swOQlu81OIngzhJT3eTNGf4jHmNqbuT28bMtThI9F0yflgB9wQGV1AWHO6clhwlsL=s0",
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("b733e46a-d9c0-4d18-a48e-b95e4433aaa7"),
            Title = "Chinese Garlic String Beans",
            Description = "A quick and healthy stir-fry featuring crisp string beans blistered with garlic and tossed in a light, savory sauce. Perfect as a side dish for any meal!",
            ImageUrl = "https://i.imgur.com/d6uP5WX.jpeg",
            Serves = 2,
            CookTime = "15m",
            Ingredients = [
                "1 lb string beans, trimmed",
                "8 cloves garlic, thinly sliced",
                "2 tbsp olive oil or avocado oil",
                "1-2 tbsp coconut aminos or low-sodium soy sauce",
                "Salt, to taste"
            ],
            Steps = [
                new Step{
                    Content = "Heat a large skillet or wok on medium high heat for 3 to 5 minutes. When the pan is hot, swirl in the avocado oil and add the green beans.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/1KCjggo.jpeg",
                        "https://i.imgur.com/QDru82T.jpeg",
                        "https://i.imgur.com/kVpbp6g.jpeg"
                    ]
                },
                new Step{
                    Content = "Cook, stirring frequently, for 3 to 5 minutes or until the green beans are blistered and tender.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/NPk0aF3.jpeg"
                    ]
                },
                new Step{
                    Content = "Transfer the green beans to a platter, leaving the oil in the skillet.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/su56Ewo.jpeg"
                    ]
                },
                new Step{
                    Content = "Turn down the heat to medium and add the minced garlic to the empty pan. Cook, stirring frequently, until the garlic is fragrant but doesn’t burn. Pour in the broth and scrape up the browned bits.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/iKhfTmt.jpeg"
                    ]
                },
                new Step{
                    Content = "Next, add the coconut aminos, and Umami Stir-Fry Powder, Spicy Sichuan Powder, or Diamond Crystal kosher salt and stir up any browned bits. Taste and adjust the sauce for seasoning.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/GxnJsEL.jpeg"
                    ]
                },
                new Step{
                    Content = "Add the blistered green beans back in the pan and toss well to coat the beans with the garlic sauce. Plate and serve!",
                    AttachedImageUrls = [
                        "https://i.imgur.com/nZJ7BRc.jpeg",
                        "https://i.imgur.com/hNmhzrP.jpeg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-ab12-cd34ef56gh78"),
            Title = "Strawberry Spinach Salad with Poppy Seed Dressing",
            Description = "A refreshing salad featuring baby spinach, juicy strawberries, red onion, and a tangy poppy seed dressing.",
            ImageUrl = "https://www.allrecipes.com/thmb/PqbFLx2uVrHN2KvLouUKxHyDTfY=/0x512/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/4539063_Strawberry-Spinach-Salad-I-4x3-a696255b0c0e4d808fae6e91fae2f3fc.jpg",
            Serves = 4,
            CookTime = "1h20m",
            Ingredients = [
                "½ cup white sugar",
                "½ cup olive oil",
                "¼ cup distilled white vinegar",
                "2 tablespoons sesame seeds",
                "1 tablespoon poppy seeds",
                "1 tablespoon minced onion",
                "¼ teaspoon paprika",
                "¼ teaspoon Worcestershire sauce",
                "1 quart strawberries - cleaned, hulled and sliced",
                "10 ounces fresh spinach - rinsed, dried and torn into bite-size pieces",
                "¼ cup almonds, blanched and slivered"
            ],
            Steps = [
                new Step{
                    Content = "Gather all ingredients.",
                    AttachedImageUrls = [
                        "https://www.allrecipes.com/thmb/poXhm3c109iL-0ku580ILuuim3U=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/AR-14276-Strawberry-Spinach-Salad-Step-1-15d0ff2fddfe4066942735621d2d0f3f.jpg"
                    ]
                },
                new Step{
                    Content = "Make dressing: Whisk together sugar, oil, vinegar, sesame seeds, poppy seeds, onion, paprika, and Worcestershire in a medium bowl. Cover and chill for 1 hour.",
                    AttachedImageUrls = [
                        "https://www.allrecipes.com/thmb/XOaQvm3c7yV2r6rPjS7zIjHP37k=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/AR-14276-Strawberry-Spinach-Salad-Step-2-8338e41117b1489080067d624ce3dbbc.jpg"
                    ]
                },
                new Step{
                    Content = "Make salad: Combine strawberries, spinach, and almonds in a large bowl. Pour dressing over salad; toss to coat.",
                    AttachedImageUrls = [
                        "https://www.allrecipes.com/thmb/79cqdMxJ5GpKLMLssdeLDNOPfHg=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/AR-14276-Strawberry-Spinach-Salad-Step-3-f928771224c24bf58ac762bb0441f5f2.jpg",
                        "https://www.allrecipes.com/thmb/OGMwOX2l1Vufxr7GF8Ql0giSMjg=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/AR-14276-Strawberry-Spinach-Salad-Step-4-0ea001448b1c4ab5bad1a15b77fd6f9f.jpg"
                    ]
                },
                new Step{
                    Content = "Refrigerate for 10 to 15 minutes before serving.",
                    AttachedImageUrls = [
                        "https://www.allrecipes.com/thmb/RwmdraTbFvOqDYiFAS3RIWxm7e4=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/AR-14276-Strawberry-Spinach-Salad-4x3-135a121dc0b24ad693289e221dcd3477.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("b2c3d4e5-f6a7-8901-bc23-de45fg67hi89"),
            Title = "Classic Strawberry Shortcake",
            Description = "A timeless dessert featuring sweet macerated strawberries, fluffy whipped cream, and tender shortcakes.",
            ImageUrl = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fimages.media-allrecipes.com%2Fuserphotos%2F2377260.jpg&q=60&c=sc&poi=auto&orient=true&h=512",
            Serves = 8,
            CookTime = "1h30m",
            Ingredients = [
                "¼ cup unsalted butter",
                "2 cups self-rising flour",
                "¼ cup white sugar, plus more for topping",
                "½ cup milk",
                "¼ cup heavy whipping cream, plus more for brushing",
                "4 pints fresh strawberries, hulled and quartered",
                "½ cup white sugar",
                "¾ cup heavy whipping cream",
                "1 tablespoon white sugar",
                "3 drops vanilla extract"
            ],
            Steps = [
                new Step{
                    Content = "Preheat the oven to 425 degrees F (220 degrees C). Line a baking sheet with parchment paper."
                },
                new Step{
                    Content = "Melt butter in small saucepan over medium heat. Stir continually, letting foam dissipate, just until butter begins to brown, 2 to 3 minutes. Remove from heat."
                },
                new Step{
                    Content = "Whisk self-rising flour and 1/4 cup white sugar together in a bowl. Add milk, 1/4 cup cream, and toasted butter; mix just until combined."
                },
                new Step{
                    Content = "Turn dough out onto a floured surface; press or roll into a rectangle about 1-inch thick. Cut in half lengthwise, then cut each half into three portions. Place shortcake portions on the prepared baking sheet. Brush with cream; sprinkle with sugar."
                },
                new Step{
                    Content = "Bake in the preheated oven until golden brown, 15 to 18 minutes. Transfer shortcakes to a rack to cool completely."
                },
                new Step{
                    Content = "Sprinkle sliced strawberries with 1/4 cup sugar; stir until sugar begins to dissolve. Refrigerate until juice from berries is extracted, about 1 hour."
                },
                new Step{
                    Content = "Beat 3/4 cup cream, 1 tablespoon sugar, and few drops vanilla extract in a bowl with an electric mixer until soft peaks form."
                },
                new Step{
                    Content = "Split shortcakes in half; place bottom half in a bowl. Spoon strawberries and juice over shortcake half; top with shortcake top. Spoon more strawberries and juice over shortbread. Finish with a dollop of whipped cream.",
                    AttachedImageUrls = [
                        "https://www.allrecipes.com/thmb/QbDkVbXyxVeXwYdAWlY1UkuVGMI=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/2377260-517c29a9a314446982c6dae4143e9414.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("c3d4e5f6-a7b8-9012-cd34-ef56gh78ij90"),
            Title = "Strawberry Banana Smoothie",
            Description = "A refreshing and healthy smoothie blending strawberries, banana, Greek yogurt, and honey for a quick breakfast or snack.",
            ImageUrl = "https://www.allrecipes.com/recipe/215093/strawberry-banana-smoothie",
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "1 cup fresh strawberries, hulled",
                "1 ripe banana",
                "1/2 cup Greek yogurt",
                "1/2 cup milk (or almond milk)",
                "1 tbsp honey",
                "Ice cubes as needed"
            ],
            Steps = [
                new Step{
                    Content = "Place the strawberries, banana, Greek yogurt, milk, and honey in a blender."
                },
                new Step{
                    Content = "Blend until smooth. Adjust thickness with ice or additional milk as needed."
                },
                new Step{
                    Content = "Pour into glasses and serve immediately."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("d4e5f6a7-b8c9-0123-de45-f678gh90ij12"),
            Title = "Strawberry Basil Bruschetta",
            Description = "A unique appetizer featuring toasted baguette slices topped with creamy goat cheese, fresh strawberries, basil, and a drizzle of balsamic glaze.",
            ImageUrl = "https://www.bonappetit.com/recipe/strawberry-basil-bruschetta",
            Serves = 4,
            CookTime = "15m",
            Ingredients = [
                "1 baguette, sliced and toasted",
                "1 cup fresh strawberries, diced",
                "1/2 cup goat cheese, softened",
                "1/4 cup fresh basil, chopped",
                "1 tbsp balsamic glaze",
                "Salt and pepper to taste"
            ],
            Steps = [
                new Step{
                    Content = "Spread the softened goat cheese onto toasted baguette slices."
                },
                new Step{
                    Content = "Top each slice with diced strawberries and chopped basil."
                },
                new Step{
                    Content = "Drizzle with balsamic glaze, season with salt and pepper, and serve immediately."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("e5f6a7b8-c9d0-1234-ef56-gh78ij90kl34"),
            Title = "Strawberry Balsamic Chicken",
            Description = "A savory dish where tender chicken breasts are glazed with a strawberry-balsamic reduction for a delightful balance of sweet and tangy flavors.",
            ImageUrl = "https://www.foodnetwork.com/recipes/food-network-kitchen/strawberry-balsamic-chicken-recipe-2011617",
            Serves = 4,
            CookTime = "40m",
            Ingredients = [
                "4 boneless, skinless chicken breasts",
                "2 cups fresh strawberries, chopped",
                "1/2 cup balsamic vinegar",
                "1/4 cup honey",
                "2 cloves garlic, minced",
                "2 tbsp olive oil",
                "Salt and pepper to taste"
            ],
            Steps = [
                new Step{
                    Content = "Season the chicken breasts with salt and pepper."
                },
                new Step{
                    Content = "Heat olive oil in a skillet over medium-high heat and sear the chicken until golden, about 5 minutes per side."
                },
                new Step{
                    Content = "Remove the chicken and set aside. In the same skillet, add garlic and strawberries; cook until the strawberries soften."
                },
                new Step{
                    Content = "Stir in balsamic vinegar and honey; simmer until the sauce thickens slightly."
                },
                new Step{
                    Content = "Return the chicken to the skillet and spoon the sauce over it. Cook for an additional 10 minutes until the chicken is cooked through. Serve hot."
                }
            ],
        },

    ];
}
