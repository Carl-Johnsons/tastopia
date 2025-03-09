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
            Id = Guid.Parse("fdcec077-b088-4af5-8271-f2f97c191c4b"),
            Title = "Strawberry Banana Smoothie",
            Description = "This classic strawberry banana smoothie recipe is easy to make with 4 simple ingredients in less than 5 minutes, and always tastes so sweet and refreshing!",
            ImageUrl = "https://i.imgur.com/cjoKYcr.jpeg",
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "Frozen strawberries",
                "Banana",
                "Milk",
                "Ice",
            ],
            Steps = [
                new Step{
                    Content = "Combine ingredients. Add all ingredients to a blender."
                },
                new Step{
                    Content = "Blend. Pulse until smooth. (If your smoothie seems too thick, add in a bit of extra milk or water. If it seems too thin, add in more strawberries or banana.)"
                },
                new Step{
                    Content = "Serve. Then pour the smoothie into a serving glass or two…and enjoy!"
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("3b6fcddb-daea-47a3-a338-1697d307586d"),
            Title = "Strawberry Basil Bruschetta",
            Description = "Greek yogurt, fresh strawberries, basil and balsamic reduction over a French baguette gives you perfection.",
            ImageUrl = "https://www.jocooks.com/wp-content/uploads/2014/07/strawberry-basil-bruschetta-2.jpg",
            Serves = 1,
            CookTime = "15m",
            Ingredients = [
                "1 French baguette",
                "½ cup Greek yogurt",
                "1½ cups strawberries",
                "½ cup fresh basil",
                "½ cup balsamic vinegar",
                "2 tablespoon almond slivers",
                "black pepper"
            ],
            Steps = [
                new Step{
                    Content = "Pour the vinegar in a small sauce pan and bring to a boil over medium heat. Lower the heat and continue cooking, stirring occasionally for about 10 minutes or until vinegar thickens so that it’s close to a honey consistency. Let cool."
                },
                new Step{
                    Content = "Bake the French baguette slices until toasted. I actually placed mine under the broiler for about a minute on both sides, make sure you don’t burn them."
                },
                new Step{
                    Content = "Spread some yogurt over each slice, then top with strawberries and chopped basil. Drizzle a bit of the balsamic reduction over each slice and top with slivered almonds for a bit of crunch. Finish of with some freshly ground black pepper."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("b3333754-11d3-41a3-bb3b-8010ad1d4e00"),
            Title = "Strawberry Balsamic Chicken",
            Description = "This strawberry balsamic chicken is a delicious and easy fruity version of caprese that I adore!",
            ImageUrl = "https://www.gimmesomeoven.com/wp-content/uploads/2013/07/Strawberry-Balsamic-Chicken-3.jpg",
            Serves = 4,
            CookTime = "40m",
            Ingredients = [
                "4 boneless, skinless chicken breasts",
                "1 cup balsamic vinegar, divided",
                "Salt and pepper",
                "1 pint fresh strawberries, hulled and roughly chopped",
                "4 oz. fresh mozzarella ball (or shredded mozzarella), roughly chopped",
                "1/4 cup chopped fresh basil",
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 400 degrees F."
                },
                new Step{
                    Content = "Combine chicken breasts and 1/2 cup balsamic in a ziplock bag. Refrigerate for at least 5 minutes, or up to 5 hours. When ready to cook, place chicken in a baking dish and pour the remaining vinegar on top. Season chicken generously with salt and pepper. Bake for 30 minutes, or until chicken is cooked through and no longer pink inside."
                },
                new Step{
                    Content = "Meanwhile, bring the remaining 1/2 cup vinegar to a boil in a small saucepan over medium-high heat. Reduce heat to medium-low and simmer for about 10 minutes, or until reduced by half. Remove and set aside."
                },
                new Step{
                    Content = "In a small bowl, stir together strawberries, mozzarella and basil to make a caprese topping."
                },
                new Step{
                    Content = "When the chicken is ready, transfer to serving dishes. Then top with the strawberry caprese topping, and then drizzle with the reduced balsamic vinegar."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("27d860f7-3625-4df2-bb0b-65da56c64265"),
            Title = "Hyacinth Bean Sweet Soup",
            Description = "A traditional Vietnamese sweet soup from Huế made with hyacinth beans, coconut milk, brown sugar, and pandan leaves. Enjoy it warm or chilled!",
            ImageUrl = "https://vietnamesefood.com.vn/kcfinder/upload/images/hinh%2010/Hyacinth%20Bean%20Sweet%20Soup%20Recipe%20(Ch%C3%A8%20%C4%90%E1%BA%ADu%20V%C3%A1n)%20(1).jpg",
            Serves = 6,
            CookTime = "1d 45m",
            Ingredients = [
                "300g hyacinth beans",
                "Brown sugar cubes",
                "Pandan leaves (50g)",
                "6 teaspoons tapioca starch",
                "½ teaspoon vanilla starch.",
            ],
            Steps = [
                new Step{
                    Content = "Soak hyacinth beans in cold water (cover all beans) and bring to cook until it boils. Then, wait for 1 night."
                },
                new Step{
                    Content = "Next day, bring to clean and spread out all covers of beans. If you see any bean floats on water, take it out because of its bad quality."
                },
                new Step{
                    Content = "Clean well again and bring to steam until they are soft."
                },
                new Step{
                    Content = "Cook water (3 – 4 bowls) with brown sugar cubes (depend on your flavor) and a little salt until it boils. Next, add pandan leaves into pot (tie like a bunch before adding into pot).",
                    AttachedImageUrls = [
                        "https://vietnamesefood.com.vn/kcfinder/upload/images/hinh%2010/Hyacinth%20Bean%20Sweet%20Soup%20Recipe%20(Ch%C3%A8%20%C4%90%E1%BA%ADu%20V%C3%A1n)%20(5).jpg"
                    ]
                },
                new Step{
                    Content = "Mix tapioca starch with 100ml water, pour slowly into mixture in step 4, use wood spoon to stir well and gently. When it becomes thicker is perfect."
                },
                new Step{
                    Content = "Now, you add steamed hyacinth beans into mixture in step 5, stir gently to make sure these beans will not be broken. Season again to suit your flavor and turn off the heat, add more vanilla starch and stir gently.",
                    AttachedImageUrls = [
                        "https://vietnamesefood.com.vn/kcfinder/upload/images/hinh%2010/Hyacinth%20Bean%20Sweet%20Soup%20Recipe%20(Ch%C3%A8%20%C4%90%E1%BA%ADu%20V%C3%A1n)%20(1).png",
                        "https://vietnamesefood.com.vn/kcfinder/upload/images/hinh%2010/Hyacinth%20Bean%20Sweet%20Soup%20Recipe%20(Ch%C3%A8%20%C4%90%E1%BA%ADu%20V%C3%A1n)%20(3).jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("44a7ea08-8e19-4f67-85d1-8bc0dc2a7bef"),
            Title = "Vietnamese Green Papaya Salad",
            Description = "This super fresh Vietnamese-inspired green papaya salad is packed with crunchy raw veggies, roasted peanuts, and fried shallots and tossed with a bright and citrusy lime dressing! It’s easy to prepare, refreshing, and plant-based!",
            ImageUrl = "https://fullofplants.com/wp-content/uploads/2019/02/Easy-Vietnamese-Papaya-Salad-Vegan-4.jpg",
            Serves = 4,
            CookTime = "20m",
            Ingredients = [
                "Green Papaya",
                "Carrot",
                "Cucumber",
                "Fish sauce",
                "Garlic",
                "Chili",
                "Lime",
                "Sugar",
                "Fresh herbs",
                "Peanuts",
                "Fried shallots",
                "Salt to taste"
            ],
            Steps = [
                new Step{
                    Content = "Peel and slice the papaya. Start by peeling the papaya skin using a vegetable peeler. Then, slice it in half lengthwise. Using an ice cream scoop or a regular spoon, scoop out the seeds and discard them."
                },
                new Step{
                    Content = "Let it soak. Transfer the green papaya halves to a large bowl and cover with cold water. Let it soak for about 15 minutes."
                },
                new Step{
                    Content = "Grate it into thin strips. Next, drain the papaya well and pat it dry using kitchen paper towels. Using a vegetable grater or a food processor fitted with the grating disk, grate the papaya into long and thin strips (also called julienne).",
                    AttachedImageUrls = [
                        "https://fullofplants.com/wp-content/uploads/2019/02/Easy-Vietnamese-Papaya-Salad-Vegan-6.jpg"
                    ]
                },
                new Step{
                    Content = "Soak the papaya strips. Transfer the papaya strips to a large bowl, cover with ice-cold water, and add 1/4 teaspoon of salt. Let it sit for 10 minutes (this step helps crisp up the papaya by extracting some of the water, as well as more latex). Drain and squeeze firmly with your hands to remove excess water. Transfer the drained papaya strips to a large mixing bowl."
                },
                new Step{
                    Content = "Slice the other vegetables. In the meantime, julienne the carrot (grate it into long strips) and thinly slice the cucumber. Transfer to the mixing bowl.",
                    AttachedImageUrls = [
                        "https://fullofplants.com/wp-content/uploads/2023/01/Easy-Vietnamese-Papaya-Salad-Vegan-22-1024x1536.jpg",
                        "https://fullofplants.com/wp-content/uploads/2023/01/Easy-Vietnamese-Papaya-Salad-Vegan-23-1024x1536.jpg"
                    ]
                },
                new Step{
                    Content = "Mince the aromatics. Finely mince the garlic and chili. Alternatively, you can crush them in a mortar and pestle."
                },
                new Step{
                    Content = "Combine everything. Add the minced garlic, chili, fish sauce, sugar, and lime juice to a small bowl. Stir until the sugar has completely dissolved."
                },
                new Step{
                    Content = "Toss the salad with the dressing. Finally, toss the green papaya and carrots with the dressing. Incorporate the fresh herbs, peanuts, and fried shallots, and toss again. Serve immediately!",
                    AttachedImageUrls = [
                        "https://fullofplants.com/wp-content/uploads/2019/02/Easy-Vietnamese-Papaya-Salad-Vegan-12.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("9d4e3acd-05ea-4766-bbbd-cd7a06243088"),
            Title = "Vietnamese Papaya Soup with Pork",
            Description = "Vietnamese folk remedy says Papaya Soup, or Canh Du Du, stimulates lactation in new mothers. Whether it’s true or not, I surely didn’t complain when I was endlessly served Canh Du Du after the birth of my children. Canh Du Du and Vietnamese Sour Catfish Soup (Canh Chua) are two my favorite Vietnamese soups.",
            ImageUrl = "https://i0.wp.com/vickypham.com/wp-content/uploads/2017/05/de953-vietnamesepapayasoupcanhdudu.jpg?resize=2500%2C2500",
            Serves = 4,
            CookTime = "45m",
            Ingredients = [
                "1 lb pork shoulder (cut into small cubes, blanch in boiling salted water for 5 minutes, then rinse)",
                "1 1/2 lbs ripened yet firm papaya flesh (cut into small cubes)",
                "1 tablespoon minced shallot",
                "1 teaspoon minced garlic",
                "2-3 tablespoons vegetable oil",
                "6 cups water",
                "1/2 teaspoon salt",
                "1 teaspoon chicken bouillon powder (or 1 teaspoon salt if none is available)",
                "1/4 cup chopped cilantro"
            ],
            Steps = [
                new Step{
                    Content = "Add vegetable oil to the bottom of a small pot. Heat on high. Add shallots and garlic. Stir until shallot and garlic are limp and fragrant (about 15 seconds).",
                    AttachedImageUrls = [
                        "https://i.imgur.com/BkBxlYx.png",
                        "https://i.imgur.com/td1391T.png",
                        "https://i.imgur.com/BVzq728.png"
                    ]
                },
                new Step{
                    Content = "Add pork. Toss until it is evenly coated with the aromatics.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/NYtfghN.png",
                        "https://i.imgur.com/6jO2s5l.png"
                    ]
                },
                new Step{
                    Content = "Add water. Be careful as oil may splatter. Bring the pot to a boil then reduce heat to low. Simmer for 25 minutes or until pork is chopstick or fork tender. Occasionally skim off the foam at the top.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/t0GOzPJ.png",
                        "https://i.imgur.com/sYPd5wH.png",
                        "https://i.imgur.com/rFtJT7Y.png"
                    ]
                },
                new Step{
                    Content = "Add papaya. Cook for 5 minutes. Season with salt and chicken soup powder.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/TSGTVxi.png",
                        "https://i.imgur.com/a4I1Q7I.png",
                        "https://i.imgur.com/Q74WU0B.png"
                    ]
                },
                new Step{
                    Content = "Turn off heat. Garnish with cilantro.",
                    AttachedImageUrls = [
                        "https://i.imgur.com/RTgBITF.png",
                        "https://i.imgur.com/71IfDXl.png"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("9a2ffffa-d335-452f-a299-375fd7d8cb58"),
            Title = "Vietnamese Papaya Smoothie",
            Description = "A refreshing papaya smoothie blending ripe papaya, yogurt, and a hint of condensed milk for a creamy, tropical treat.",
            ImageUrl = "https://i0.wp.com/cookwithchung.com/wp-content/uploads/2022/10/Summer-papaya-smoothie.jpg",
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "1 cup papaya",
                "1 cup ice cubes",
                "50ml milk",
                "1 tbsp condensed milk",
            ],
            Steps = [
                new Step{
                    Content = "Peel and deseed the papaya. Cut into chunks."
                },
                new Step{
                    Content = "Add papaya, ice cubes, milk and condensed milk to the blender. Blend on high speed until fully blended"
                },
                new Step{
                    Content = "Pour into a glass and enjoy!"
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("b7703e62-9271-4790-ac6c-309e9e32653a"),
            Title = "Pineapple Chicken Stir-Fry",
            Description = "This pineapple chicken stir fry recipe has a deliciously sweet and tangy sauce that perfectly coats the tender chicken, pineapple, and veggies. It costs less than takeout! This dish is flavored with fish sauce, sugar, and a hint of black pepper – a perfect balance of tangy and sweet.",
            ImageUrl = "https://www.saltandlavender.com/wp-content/uploads/2020/07/pineapple-chicken-1-1200x1751.jpg",
            Serves = 4,
            CookTime = "25m",
            Ingredients = [
                "500g chicken breast, thinly sliced",
                "2 cups fresh pineapple, cut into chunks",
                "1 red bell pepper, sliced",
                "1 medium onion, sliced",
                "3 cloves garlic, minced",
                "2 tbsp vegetable oil",
                "2 tbsp fish sauce",
                "1 tbsp sugar (adjust to taste)",
                "1/2 tsp black pepper",
                "Fresh cilantro, chopped (for garnish)"
            ],
            Steps = [
                new Step{
                    Content = "Prepare ingredient",
                    AttachedImageUrls = [
                        "https://www.saltandlavender.com/wp-content/uploads/2020/07/ingredients-for-pineapple-chicken-1200x1782.jpg",
                    ]
                },
                new Step{
                    Content = "Mix the sauce ingredients together in a small bowl, then set aside. Chop the veggies. Cut the chicken into bite-size pieces and coat them in the garlic powder, pepper, and cornstarch.",
                    AttachedImageUrls = [
                        "https://www.saltandlavender.com/wp-content/uploads/2020/07/how-to-make-pineapple-chicken-1-1200x783.jpg"
                    ]
                },
                new Step{
                    Content = "Pan fry the chicken in olive oil in two batches in a skillet. That way it’ll brown up nicely and won’t steam by being too crowded. Transfer to a plate once cooked to 165F. In the skillet, stir fry the pineapple.",
                    AttachedImageUrls = [
                        "https://www.saltandlavender.com/wp-content/uploads/2020/07/how-to-make-pineapple-chicken-2-1200x783.jpg"
                    ]
                },
                new Step{
                    Content = "Add in the veggies, and cook until tender-crisp. Return the chicken back to the pan. Pour in the sauce, and let it bubble until thickened. Garnish with scallions if desired.",
                    AttachedImageUrls = [
                        "https://www.saltandlavender.com/wp-content/uploads/2020/07/how-to-make-pineapple-chicken-3-1200x783.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("0f1281b5-608a-4df8-a4b6-605aae8abbf8"),
            Title = "Vietnamese Sweet & Sour Soup",
            Description = "This is a simplified but still flavorful version of my mom's Vietnamese Sweet and Sour soup and can be ready in under 30 minutes. Pineapple is used to sweeten the broth and lemons are used for the sour element. Easy and fast to prepare.",
            ImageUrl = "https://www.cooking-therapy.com/wp-content/uploads/2020/12/Canh-Chua-13.jpg",
            Serves = 6,
            CookTime = "40m",
            Ingredients = [
                "2 quarts water or chicken stock",
                "1 large tomato",
                "juice from 16 oz can pineapple and 3 pineapple rings cut into chunks",
                "6-8 large shrimp",
                "3 Tablespoons lemon juice ",
                "2 Tablespoons sugar",
                "5 Tablespoons Vietnamese or Thai fish sauce",
                "handful of okra",
                "handful of bean sprouts",
                "2 large leaves of culantro",
                "salt",
                "hot pepper",
                "scallions and cilantro for garnish"
            ],
            Steps = [
                new Step{
                    Content = "Bring water or chicken stock* to a boil."
                },
                new Step{
                    Content = "Add tomato, pineapple juice and pineapple chunks and reduce heat to simmer for 5 minutes."
                },
                new Step{
                    Content = "Add shrimp, optional chili, lemon, sugar, and fish sauce and simmer 3-5 minutes or until shrimp is cooked."
                },
                new Step{
                    Content = "Add okra, bean sprouts and recao. Cover and remove from heat. Let stand 5 minutes."
                },
                new Step{
                    Content = "Salt to taste and garnish with scallions and cilantro."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("027157a0-866f-487a-98b7-e94f4bfcc300"),
            Title = "Shrimps, bean sprouts and watermelon salad",
            Description = "A light salad bursting with fruity flavors and the surprising tang and umami of Vietnamese fish sauce.",
            ImageUrl = "https://umamidays.com/wp-content/uploads/2022/06/shrimp-watermelon-salad.jpg",
            Serves = 4,
            CookTime = "20m",
            Ingredients = [
                "20 medium shrimps peeled and deveined",
                "salt",
                "pepper",
                "6 cups seedless watermelon cubes",
                "1 cup mandarin orange segments",
                "4 generous handfuls mung bean sprouts",
                "1 small head Romaine lettuce",
                "1 cup cubed feta",
                "¼ cup mixed fish sauce"
            ],
            Steps = [
                new Step{
                    Content = "Cook the shrimps in a frying pan, boil enough water to reach a depth of one inch. Sprinkle with salt and pepper."
                },
                new Step{
                    Content = "Spread the shrimps in the boiling water and cook for two to two and a half minutes per side."
                },
                new Step{
                    Content = "Scoop out the shrimps and dry on a bed of paper towels. Cool."
                },
                new Step{
                    Content = "Toss the fruits and vegetables in a large mixing bowl, toss together the watermelon cubes, mandarin orange segments, bean sprouts, lettuce and feta."
                },
                new Step{
                    Content = "Add the shrimps. Toss to distribute.Drizzle in the mixed fish sauce and toss a few more times. Serve immediately.",
                    AttachedImageUrls = [
                        "https://umamidays.com/wp-content/uploads/2022/06/shrimp-watermelon-salad-2-1200x900.jpg",
                        "https://umamidays.com/wp-content/uploads/2022/06/vietnamese-mixed-fish-sauce.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("77eaa718-0346-4d79-8f3f-5dd6dbf9d615"),
            Title = "Vietnamese Watermelon Smoothie",
            Description = "A refreshing and creamy smoothie made with ripe watermelon, coconut water, and a squeeze of lime for a tropical Vietnamese twist.",
            ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQSAWndjhgeBuq40UBNv3r-0dEsAAiew0ruqA&s", // example source
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "2 cups ripe watermelon, cubed",
                "1/2 cup coconut water",
                "Juice of 1 lime",
                "A few fresh mint leaves (optional)",
                "Ice cubes (as needed)"
            ],
            Steps = [
                new Step{
                    Content = "Place the watermelon cubes, coconut water, lime juice, and mint leaves in a blender."
                },
                new Step{
                    Content = "Blend until smooth. Add ice cubes to reach your desired consistency."
                },
                new Step{
                    Content = "Pour into glasses and serve immediately."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("a5c8f8c8-97c5-4047-b28b-1b8ab377cbbf"),
            Title = "Chilled Cantaloupe Soup",
            Description = "Very refreshing fruit soup, served chilled. Great for luncheons. Garnish with mint if desired.",
            ImageUrl = "https://www.allrecipes.com/thmb/IX2uWzS9P2NnVczL_eo3eY3Yx5o=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/19125-61dcf1cab4044e179cef30c4d0ed814f.jpg",
            Serves = 6,
            CookTime = "1h20m",
            Ingredients = [
                "1 cantaloupe - peeled, seeded and cubed",
                "2 cups orange juice",
                "1 tablespoon fresh lime juice",
                "¼ teaspoon ground cinnamon",
            ],
            Steps = [
                new Step{
                    Content = "Peel, seed, and cube the cantaloupe."
                },
                new Step{
                    Content = "Place cantaloupe and 1/2 cup orange juice in a blender or food processor; cover, and process until smooth. Transfer to large bowl. Stir in lime juice, cinnamon, and remaining orange juice. Cover, and refrigerate for at least one hour. Garnish with mint if desired."
                },
            ],
        },
        new Recipe{
            Id = Guid.Parse("52c0c17c-6837-4f6b-a6db-9a7545c4075d"),
            Title = "Crisp Fried Pig's Tail",
            Description = "A Little Porky Universe: The Qualities of a Pig's Tail",
            ImageUrl = "https://www.seriouseats.com/thmb/dUyzl_nOQq-xuDbY6pcUP_KFsoM=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():format(webp)/__opt__aboutcom__coeus__resources__content_migration__serious_eats__seriouseats.com__recipes__images__20090901-pigs-tails-finished-44cf2f6acb374a42a31d961c51075f6f.jpg",
            Serves = 4,
            CookTime = "1h40m",
            Ingredients = [
                "2 pounds pigs tails, cut into approximately 4-inch sections",
                "2 quarts oil for deep frying",
                "1/2 cup flour, or enough to coat the tails",
                "3 cloves garlic, smashed",
                "Half an onion, roughly chopped",
                "3 tablespoons soy sauce",
                "1/2 cup rice wine or white wine of your choice for braising",
                "1 teaspoon salt",
                "A splash soy sauce",
                "A splash red wine",
                "Salt and pepper to taste",
            ],
            Steps = [
                new Step{
                    Content = "In a wok or pot, heat the quarts of oil to 350°F. Carefully rinse and pat dry all the pigs' tails. Gently slip them into the oil and fry for 3 to 4 minutes, until the tails are golden brown."
                },
                new Step{
                    Content = "Place the tails into a braising pot and cover with water. Add the garlic, onion, soy sauce, wine, and salt. Simmer for an hour, and let cool. Reserve the stock for another use."
                },
                new Step{
                    Content = "Reheat the oil until it is 350°F. Pat the tails dry with a paper towel or clean cloth, taking care that no residual stock is sticking to the surface. Roll each tail segment around in the flour, until it is evenly coated."
                },
                new Step{
                    Content = "With one hand, very quickly slip the tail segments into the pot, using your other hand to maneuver the lid of the pot as you go along. Fry for another 3 minutes, until the tails are golden brown and crisped."
                },
                new Step{
                    Content = "Tossing evenly, dress the tails in a mixture of soy sauce and vinegar with salt and pepper to taste. The tails will have absorbed only a bit of the dressing, but it is enough. Have a bit of the dressing handy on the side, for dipping."
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("2b94a458-0ded-40d1-b17b-148cde3c3d37"),
            Title = "Vietnamese Baked Banana Cake",
            Description = "Soft, creamy, and rich Vietnamese baked banana cake is an amazing way to use up some bananas and this is not your typical banana cake. You will love the creamy pudding-like texture.",
            ImageUrl = "https://whattocooktoday.com/wp-content/uploads/2023/03/banh-chuoi-nuong-4.jpg",
            Serves = 10,
            CookTime = "1h45m",
            Ingredients = [
                "800g bananas",
                "4g salt",
                "480g water",
                "60g rum",
                "½ tsp salt",
                "50g egg",
                "400g coconut cream",
                "100g condensed milk",
                "30g sugar",
                "30g rice flour",
                "60g melted butter",
                "150g stale bread slices",
                "15g honey"
            ],
            Steps = [
                new Step{
                    Content = "Preheat the oven to 300 F (180 C) for conventional oven, 280 F(140 C) for convection oven. Line the baking pan with parchment paper at the bottom"
                },
                new Step{
                    Content = "It is important to choose ripe bananas but not overripe ones because we need the bananas to hold their shapes. Combine salt and water in a large mixing bowl. Peel the bananas and leave them whole. Let them soak in the salt water for at least 15 minutes and up to 30 minutes. Soaking the bananas helps to prevent the bananas from turning brown and it also gives the bananas nice reddish color after being baked",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-1.jpg"
                    ]
                },
                new Step{
                    Content = " After soaking the banana, cut them into about 3/4 inch thick. Do not cut too thin.",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-5.jpg"
                    ]
                },
                new Step{
                    Content = "Pour rum and salt into a large skillet. Arrange the banana slices on top and cook over medium-low heat for about 5-7 minutes. Reserve about 15-20 slices to decorate the cake later",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-6.jpg"
                    ]
                },
                new Step{
                    Content = "Cut the bread slices into 1/2-inch cubes or thin slices. Combine all ingredients for the coconut milk mixture.",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-3.jpg"
                    ]
                },
                new Step{
                    Content = "Add the bread and the cooked bananas and stir to combine everything.",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-7.jpg",
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-8.jpg"
                    ]
                },
                new Step{
                    Content = "Pour into the prepared baking pan. Arrange the banana slices you reserve earlier on top",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-9.jpg"
                    ]
                },
                new Step{
                    Content = "Cover the pan with a foil and put the pan on the middle rack and bake for 90 minutes.",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-10.jpg"
                    ]
                },
                new Step{
                    Content = "Remove from the cake from the oven and remove the foil. Increase oven temperature to 350 F (180 C). Brush the surface of the cake with honey and melted butter mixture.",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-12.jpg"
                    ]
                },
                new Step{
                    Content = "Put it back into the oven and bake for another 15 minutes or until you get a nice golden brown on top",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/baked-banana-cake-11.jpg"
                    ]
                },
                new Step{
                    Content = "Place the pan on a cooling rack and let the cake cool in the pan completely before unmolding and serving. Loosen the edge of the cake, put a plate on top and then carefully invert the cake upside down, peel off the parchment paper and invert it back on to a serving plate. Cut into serving size. The cake can be served at room temperature or after being chilled in the fridge. It will have a firmer texture when it’s cold",
                    AttachedImageUrls = [
                        "https://whattocooktoday.com/wp-content/uploads/2023/03/banh-chuoi-nuong-1.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("7a3adfb0-7848-49f0-bf5c-b0db5d8f9bee"),
            Title = "Vietnamese Banana Smoothie",
            Description = "A refreshing Vietnamese banana smoothie made with ripe bananas, ice, condensed milk (or coconut milk for a vegan version), and a squeeze of lime for extra brightness.",
            ImageUrl = "https://guides.brit.co/media-library/take-a-cup-a-straw-and-it-ready-to-serve-awesome.jpg?id=23864125&width=700&quality=85",
            Serves = 2,
            CookTime = "5m",
            Ingredients = [
                "50.0g Ice cream (different flavor different taste)",
                "10.0g Sugar",
                "50.0g Condensed milk",
                "200.0ml Milk",
                "3 Bananas",
            ],
            Steps = [
                new Step{
                    Content = "Place bananas in the blender",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/place-bananas-in-the-blender.jpg?id=23864097&width=700&quality=85"
                    ]
                },
                new Step{
                    Content = "Add COLD milk, the more you add, the less thick it get.",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/add-cold-milk-the-more-you-add-the-less-thick-it-get.jpg?id=23864102&width=700&quality=85"
                    ]
                },
                new Step{
                    Content = "Add a spoon of Ice cream. Vanilla is standard, chocolate would be good, too.",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/add-a-spoon-of-ice-cream-vanilla-is-standard-chocolate-would-be-good-too.jpg?id=23864110&width=700&quality=85"
                    ]
                },
                new Step{
                    Content = "Add condensed milk, as sweet as you like and add sugar",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/add-condensed-milk-as-sweet-as-you-like.jpg?id=23864114&width=700&quality=85",
                        "https://guides.brit.co/media-library/add-sugar.jpg?id=23864117&width=700&quality=85"
                    ]
                },
                new Step{
                    Content = "Blend all of them up until you see no more banana pieces inside. Around 20 secs.",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/blend-all-of-them-up-until-you-see-no-more-banana-pieces-inside-around-20-secs.jpg?id=23864121&width=700&quality=85"
                    ]
                },
                new Step{
                    Content = "Take a cup, a straw and it ready to serve. Awesome!!!",
                    AttachedImageUrls = [
                        "https://guides.brit.co/media-library/take-a-cup-a-straw-and-it-ready-to-serve-awesome.jpg?id=23864125&width=700&quality=85"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("f06dba1e-b786-4fa0-8ff9-4261fd0dafc2"),
            Title = "White Radish Soup",
            Description = "This clear peppery White Radish Soup with beef broth is soothing accompaniment to any rice meal. Also delicious served with thick rice noodles.",
            ImageUrl = "https://www.malaysianchinesekitchen.com/wp-content/uploads/2017/09/WhiteRadishSoup-1.jpg",
            Serves = 6,
            CookTime = "1h50m",
            Ingredients = [
                "10 cups water",
                "1 lb beef neck bones",
                "2 lbs white radish",
                "1 tsp white peppercorns",
                "Salt"
            ],
            Steps = [
                new Step{
                    Content = "Fill a large pot half full of water. Bring to a boil. Add beef neck bones or ribs. Allow it to blanch for 4 to 5 minutes. Remove with thongs and rinse in cold water.",
                    AttachedImageUrls = [
                        "https://www.malaysianchinesekitchen.com/wp-content/uploads/2017/09/WhiteRadishSoup-6.jpg"
                    ]
                },
                new Step{
                    Content = "Discard water in pot and fill with 10 cups (2.4 liters) of fresh water. Bring to a boil."
                },
                new Step{
                    Content = "Add blanched beef neck bones or ribs, white radish, and crushed peppercorns.",
                    AttachedImageUrls = [
                        "https://www.malaysianchinesekitchen.com/wp-content/uploads/2017/09/WhiteRadishSoup-7.jpg"
                    ]
                },
                new Step{
                    Content = "Bring water back up to a boil. Reduce heat to low and allow soup to simmer for 1½ hours. Skim off any scum appearing on the surface."
                },
                new Step{
                    Content = "Season with salt and turn off heat. Serve warm in individual bowls.",
                    AttachedImageUrls = [
                        "https://www.malaysianchinesekitchen.com/wp-content/uploads/2017/09/WhiteRadishSoup-8.jpg"
                    ]
                }
            ],
        },
        new Recipe{
            Id = Guid.Parse("a9f86c25-5f24-45c8-a09b-5fc29dab8936"),
            Title = "Vietnamese Fried Radish Rice Cakes",
            Description = "A popular Vietnamese street food and dim sum restaurant item, these little savory rice cakes are pan-fried to crispy perfection and served with a sweet and savory soy dipping sauce.",
            ImageUrl = "https://www.vietnamesefood.com.vn/vietnamese-vegetable-recipes/xao-cu-cai-trang-toi/", // example URL
            Serves = 5,
            CookTime = "1h35m",
            Ingredients = [
                "20 grams dried shrimp",
                "1 Chinese sausage",
                "1 tablespoon vegetable oil",
                "300 grams grated daikon",
                "100 grams rice flour",
                "10 grams tapioca starch",
                "300 ml water",
                "1 teaspoon ground white pepper",
                "1/2 teaspoon granulated cane sugar",
                "1/2 teaspoon MSG (optional)",
                "1/2 teaspoon salt",
                "Thinly sliced green onions"
            ],
            Steps = [
                new Step{
                    Content = "Prepare the dried ingredients: Soak the dried shrimp in hot water for 10 minutes. Drain and rinse. Chop the dried shrimp and Chinese sausage into small pieces. Add oil (1 tablespoon) to the skillet and heat on medium-high. Add dried shrimp and sauté for about 30 seconds. Add Chinese sausage and sauté for one minute or until you get nice caramelization on the edges of the shrimp and sausage. Transfer the mixture into another bowl and set aside."
                },
                new Step{
                    Content = "Make the batter: Mix together the batter ingredient in a medium bowl until you get a paste-like consistency. Set aside."
                },
                new Step{
                    Content = "Pan fry: Combine the batter, daikon, and dried shrimp/Chinese sausage mixture back into the skillet. Pan fry for about 5 minutes to cook off the raw flour taste."
                },
                new Step{
                    Content = "Steam: Transfer mixture to an oiled pan. I’m using a 10″x5″ loaf pan. Place the pan into a steamer with plenty of water and steam over a medium simmer for 45 minutes."
                },
                new Step{
                    Content = "Slice: Carefully remove the pan from the steamer and let your rice cake set for about 30 minutes. Once cooled, loosen the sides with a knife and turn it upside down onto a cutting board and slice ½-inch thick square or rectangular pieces."
                },
                new Step{
                    Content = "Serve: To enjoy, pan-fry the radish cakes by adding a couple tablespoons oil to skillet over medium heat. Fry the cakes on both sides until golden and crispy. If serving with eggs, heat a bit of oil on the other side of the skillet and crack your desired amount of eggs into the oil. Pan fry eggs however you choose. I simply like it over-easy with no additional seasonings. Transfer the eggs and fried rice cakes to a plate. Garnish green onions (optional) and serve with your choice of soy or fish sauce dipping sauce, and a side of pickled daikon and carrot (optional)."
                }
            ],
        },
    ];
}
