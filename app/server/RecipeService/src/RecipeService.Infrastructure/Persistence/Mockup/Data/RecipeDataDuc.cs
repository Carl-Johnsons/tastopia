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
    ];
}
