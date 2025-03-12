using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeDataVuong
{
    public static List<Recipe> Recipes = [
        // Cabbage
        new Recipe{
            Id = Guid.Parse("da9af38a-4662-4d36-b871-b7aa9dad945d"),
            Title = "Taiwanese Golden Pickled Cabbage",
            Description = "Taiwanese Golden Pickled Cabbage is a delicious and refreshing Taiwanese-style side dish that is easy to find in many places.Unlike Korean kimchi, Golden Pickled Cabbage isn’t fermented. Instead, it is marinated with fermented bean curd as a key ingredient in the sauce, along with garlic, vinegar, apple, and carrot, which give it its signature golden color. Its flavor is fruity, refreshing, and rich, making it a perfect pairing with rice, noodles, or grilled dishes. It’s light, tasty, and adds a refreshing touch to any meal!",
            ImageUrl = "https://img-global.cpcdn.com/recipes/d9149772fc222974/640x640sq70/photo.webp",
            Serves = 4,
            CookTime = "30 mins",
            Ingredients = ["500g Cabbage", "15g Salt", "30ml Sesame oil", "120g Carrot", "150g Apple", "20g Garlic", "80g Fermented bean curd", "100g Apple cider vinegar"],
            Steps = [
                new Step{
                    Content = "Rinse the cabbage and chop it into large chunks.",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/6d9517d614f6e06c/640x640sq70/photo.webp"
                    }
                },
                new Step{
                    Content = "Add salt equal to 3% of the total cabbage weight (15g). Mix well until the cabbage starts to soften slightly, then let it sit for 30 minutes.",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/39c667d44076c6eb/640x640sq70/photo.webp"
                    }
                },
                new Step{
                    Content = "Now, let's make the marinade: Chop the carrot and apple into chunks. Place the rest of the ingredients into a food processor or use an immersion blender to blend them into a puree.",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/aa259ad3e93ace7e/640x640sq70/photo.webp"
                    }
                },
                new Step{
                    Content = "Make sure to do a taste test and season to your liking.",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/355659dc61edc801/640x640sq70/photo.webp"
                    }
                },
                new Step{
                    Content = "Gently squeeze the cabbage after resting to remove excess water. (Remember to do a taste test! If you prefer a milder flavor, you can quickly rinse the cabbage with drinking water to wash away some of the salt.)",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/a0b00721ae8853bf/640x640sq70/photo.webp"
                    }
                },
                new Step{
                    Content = "Mix the cabbage with the marinade, ensuring it’s evenly coated.",
                    AttachedImageUrls = new List<string>{
                        "https://img-global.cpcdn.com/steps/9f1e81ccd77d97d3/640x640sq70/photo.webp"
                    }
                },
            ]
        },
        new Recipe{
            Id = Guid.Parse("15289b6e-b1d8-498e-91e3-44e4fa1d84a2"),
            Title = "Sauteed Cabbage",
            Description = "A simple and delicious stir-fried cabbage dish with glass noodles, eggs, and carrots, seasoned with soy sauce and aromatics.",
            ImageUrl = "https://cooktoria.com/wp-content/uploads/2021/01/sauteed-cabbage-1.jpg",
            Serves = 2,
            CookTime = "15 mins",
            Ingredients = [
                "1 small cabbage",
                "1/2 carrot",
                "2 packs glass noodles",
                "3 eggs",
                "1 tbsp soy sauce",
                "1 tsp dark soy sauce",
                "3 tbsp cooking oil",
                "5 garlic cloves, minced",
                "3 shallots, sliced"
            ],
            Steps = [
                new Step{
                    Content = "Wash the cabbage well, then slice it and remove the hard part. Peel the carrot and slice it. Soak the glass noodles.",
                },
                new Step{
                    Content = "Heat the pan, add oil, then scramble the eggs. Set aside.",
                },
                new Step{
                    Content = "In the same pan, add oil, then drop in the garlic and shallots. Stir-fry until fragrant.",
                },
                new Step{
                    Content = "Add the cabbage and carrot, mix well, then add a little water. Cover and simmer.",
                },
                new Step{
                    Content = "When the cabbage is almost half-cooked, add in the soaked glass noodles and season with soy sauce. Mix, cover, and cook until done.",
                },
            ]
        },

        // Purple Cabbage
        new Recipe{
            Id = Guid.Parse("ca5c3915-9965-4cb9-b939-9c940e21c23c"),
            Title = "California Farm Red Cabbage Sauerkraut",
            Description = "Red Cabbage, sweet red apples, strong yellow onions, special spices, you will love this red cabbage sauerkraut. As a cold summer salad or mixed with mashed potatoes to make a warm meal. The special spices used are available on the internet, but here is the recipe to make them yourself anytime.",
            ImageUrl = "https://ketogenic.com/wp-content/uploads/2021/12/Red-cabbage-ginger-sauerkraut-2000x1334.jpg",
            Serves = 2,
            CookTime = "3 days",
            Ingredients = [
                "1 small red cabbage (2 lbs after removing core and outer leaves)",
                "4 sweet apples (Honeycrisp, unpeeled)",
                "4 yellow onions (outer peels removed)",
                "4 bay leaves",
                "2 tbsp sea salt"
            ],
            Steps = [
                new Step{
                    Content = "Make the spice blend. You only need two tablespoons for this recipe, but it's best to prepare a small jar for future use. Grind together: 8 tsp cinnamon, 2 tsp nutmeg, 1 tsp each ginger, star anise, mace, cardamom, cloves, pepper, and coriander.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/a0d38e6d59641a7d/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Use a large plastic tub with a lid to keep insects out. Use a mandolin or sharp knife to shred the cabbage, apples, and onions. Place them in the tub, add salt and spices, and let stand for 10 minutes. The mix will start to release its juices.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c62e7d4d751302e6/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/2c661bdacdfb2b58/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Take a handful of the mixture and squeeze it between your hands until the apples release their juice.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/7d9540aceb0b8747/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "You can also use a home wine press or a wooden mallet to bruise (but not pulverize) the vegetables."
                },
                new Step{
                    Content = "Find a lid or plate that fits inside the tub to keep the sauerkraut submerged for fermentation. Use weights like boiled rocks if needed. The sauerkraut juice will turn purple.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/1ce811992f870461/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/c49cf4628819b636/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Keep the tub covered to prevent insects. The sauerkraut is ready in 3 days and reaches peak flavor in 6 weeks. To stop fermentation, refrigerate or store in a cool basement. Stays fresh for a year.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/1afeb1fc350b32a2/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/0079ab708a483cb8/640x640sq70/photo.webp" }
                },
            ]
        },
        new Recipe{
            Id = Guid.Parse("f0d8a591-274c-4da1-b0f0-311c700646ca"),
            Title = "Warm Red Cabbage Slaw",
            Description = "This recipe was inspired by Skunkmonkey101’s Warm Red Cabbage Salad. I modified it a little and added a sweet onion. Very tasty side.",
            ImageUrl = "https://christinebailey.co.uk/wp-content/uploads/2020/11/red-cabbage-with-apple-in-bowl-1-1.jpg",
            Serves = 4,
            CookTime = "30 minutes",
            Ingredients = [
                "1 small head red cabbage (1.5 lbs), core removed and sliced thinly",
                "1 sweet onion, sliced thinly",
                "4-6 strips bacon",
                "1/2 cup apple cider vinegar",
                "1/2 tsp black pepper",
                "2 tbsp sugar"
            ],
            Steps = [
                new Step{
                    Content = "In a large skillet, cook the bacon until crispy. Transfer the cooked bacon to a plate lined with a paper towel to drain. Crumble the bacon.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c0ffc5e478f43e26/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/fb25bf5354312b91/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/77ed9a9f88b7ee62/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Add sliced onion to the skillet and cook until just barely softened. Add vinegar, sugar, and black pepper. Mix well and add in the cabbage. Cook until just wilted but still slightly firm. Cover the skillet if needed to help wilt the cabbage. Mix in crumbled bacon.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/3ff188bde0d6604a/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/9b5277811842c95a/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Serve warm and enjoy!",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/8f9b449018f74891/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/f58b1c1531ae2a26/640x640sq70/photo.webp" }
                },
            ]
        },

        // Pumpkin
        new Recipe{
            Id = Guid.Parse("be7c6ff0-8eda-4f20-b1a4-3b19212abdf5"),
            Title = "Pumpkin Chicken Chili",
            Description = "Bit of a mix of two recipes from A Taste of Home' soups stews and more with my own tweaks",
            ImageUrl = "https://nourishingmeals.com/sites/default/files/styles/fullscreen_banner/public/media/SLOW%20COOKER%20PUMPKIN%20CHICKEN%20CHILI-1.jpg?h=10177e57&itok=zPfjiK7J",
            Serves = 4,
            CookTime = "1hr 30min",
            Ingredients = [
                "1 cup dried Navy Beans",
                "2 tbsp unsalted butter",
                "1 yellow onion, diced",
                "1 tbsp minced garlic",
                "1 chicken breast, diced",
                "4.5 tsp chili powder",
                "1 tsp salt",
                "1/2 tsp ground pepper",
                "1 (14.5 oz) can diced tomatoes",
                "1 (4 oz) can green chilis",
                "3 cups chicken broth",
                "1 can pumpkin puree",
                "1 cup half and half",
                "1 bay leaf",
                "1 cup grated mild white cheese (Asiago, Havarti, or Gouda)",
                "More salt to taste"
            ],
            Steps = [
                new Step{
                    Content = "Take the cup of dried beans, cover with water, and let sit overnight.",
                },
                new Step{
                    Content = "Dice the chicken into small cubes or process it in a food processor.",
                },
                new Step{
                    Content = "Dice the onion. Melt butter in a Dutch oven over medium-high heat. Add the onion and stir occasionally until it sweats. Add the garlic and cook until fragrant (about 30 seconds).",
                },
                new Step{
                    Content = "Once the veggies are cooked, add in the diced chicken. When the chicken is halfway cooked, add all spices except for the bay leaf. Cook until the chicken is done.",
                },
                new Step{
                    Content = "Pour in diced tomatoes and green chilis with their juices. Cook, stirring constantly, until the juices are reduced by half.",
                },
                new Step{
                    Content = "Turn to high heat. Add chicken broth, half and half, pumpkin puree, and beans. Stir to combine. Bring to a boil, then reduce heat to medium-low. Cook for 30 minutes, stirring once halfway through.",
                },
                new Step{
                    Content = "At the 30-minute mark, add in the bay leaf and cheese. Stir well and continue cooking, stirring every 15 minutes, until the chili thickens to the desired consistency (about 45 more minutes). Adjust salt to taste.",
                },
                new Step{
                    Content = "Serve with optional shredded cheddar or white cheddar, sour cream, and enjoy!",
                },
            ]
        },
        new Recipe{
            Id = Guid.Parse("d8f3a1c4-7b9a-4e3b-8c2d-6f5a9d8e1234"),
            Title = "🍂🎃 Pumpkin Soup 🎃🍂",
            Description = "A warm and creamy pumpkin soup, perfect for the transition from fall to winter. Made with roasted squash, fresh herbs, and a hint of coconut milk.",
            ImageUrl = "https://www.healthyfood.com/wp-content/uploads/2019/07/Creamy-pumpkin-soup-1024x638.jpg",
            Serves = 4,
            CookTime = "45 minutes",
            Ingredients = [
                "1 butternut squash pumpkin",
                "1/4 acorn squash",
                "1 cup carrots",
                "1 tomato",
                "1/2 yellow onion",
                "3 garlic cloves",
                "3 cups vegetable broth",
                "1/3 cup coconut milk",
                "4 sage leaves",
                "1 tsp thyme",
                "Olive oil",
                "Salt and pepper"
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 400°F. Cut the butternut squash lengthwise and peel/cut the acorn squash into pieces. Gather the remaining vegetables (carrots, onion, garlic, tomato).",
                },
                new Step{
                    Content = "Place pumpkin and vegetables on a baking tray. Sprinkle thyme, salt, and pepper evenly. Add sage leaves and drizzle generously with olive oil.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/95a0863413ee561c/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Bake for 30 minutes. After 30 minutes, cover with aluminum foil and bake for another 10 minutes.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/ade231aa571fa3ab/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Using a spoon, scoop out the pumpkin from the skin. In batches, add pumpkin, vegetables, and vegetable broth to a blender. Blend until smooth. Adjust broth amount for desired consistency.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c089e302dc2ac627/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Transfer the blended soup to a pot and bring to a boil. Add coconut milk and stir. Once bubbling, turn off heat and set aside.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/3b4c602aa3ac5985/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/b1e063b040b3e9bc/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Serve with a grilled cheese sandwich or buttered toast. Enjoy! 😋",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/d8dda2b36549365c/640x640sq70/photo.webp" }
                },
            ]
        },

        // Cauliflower
        new Recipe{
            Id = Guid.Parse("4f547a28-662d-4049-9764-38a02e07e628"),
            Title = "Cauliflower Steak (or Tofu Slices) with Chimichurri and Feta",
            Description = "A vibrant and flavorful dish featuring roasted cauliflower or tofu, topped with a fresh chimichurri sauce and creamy feta.",
            ImageUrl = "https://a-vegan-gourmet.com/wp-content/uploads/2021/08/Cauliflower-Steaks-min-1024x683.jpg",
            Serves = 4,
            CookTime = "45 minutes",
            Ingredients = [
                "1 medium cauliflower (or 1 block of extra-firm tofu)",
                "1 tablespoon honey",
                "Pinch chili powder",
                "3 tablespoons soy sauce",
                "1 drizzle of extra-virgin olive oil",
                "1/2 tablespoon butter",
                "1 lemon, juiced",
                "2 cloves garlic, minced",
                "20 centiliters extra-virgin olive oil",
                "6 centiliters balsamic vinegar",
                "2 shallots, minced",
                "2 tablespoons chopped cilantro",
                "1/2 teaspoon chili powder, if desired",
                "2 tablespoons chopped parsley",
                "1/2 teaspoon oregano",
                "1 tablespoon herbes de Provence",
                "150 grams feta, crumbled",
                "200 grams Greek yogurt",
                "1 lemon, juiced",
                "30 grams feta, crumbled",
                "100 grams sun-dried tomatoes",
                "Extra-virgin olive oil"
            ],
            Steps = [
                new Step{
                    Content = "Cut the cauliflower or tofu into thick slices. Mix the honey, chili powder, and soy sauce to create a marinade. Coat the slices and let them marinate.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/636475e38fe13753/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Mix lemon juice and garlic, and let rest for 10 minutes. Then, combine with the remaining chimichurri sauce ingredients.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/f49dea0d71ecf376/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/4b1edca213a92747/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/dde4e779e7b26eb6/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Blend the feta cream ingredients together until somewhat smooth.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/3e100c3b60576bfe/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/d9d5cdbed7bacce3/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Heat butter and olive oil in a frying pan. Sauté the cauliflower or tofu slices until they are tender and golden brown.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/fa715e61728fbe81/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/8b82210f1b4f840c/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/1b1302edca48c2ae/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "On a plate, spread the feta cream. Place a slice of roasted cauliflower or tofu on top. Add chimichurri sauce, then sprinkle with crumbled feta and sun-dried tomatoes. Drizzle extra-virgin olive oil and serve immediately.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/1b010d7c0f386399/640x640sq70/photo.webp" }
                },
            ]
        },
        new Recipe{
            Id = Guid.Parse("b7c66c1b-5762-4dc0-95e1-d51034dc3567"),
            Title = "Cauliflower and Broccoli",
            Description = "A simple and healthy dish featuring boiled cauliflower and broccoli with garlic and ginger for extra flavor.",
            ImageUrl = "https://www.taylorfarms.com/wp-content/uploads/2023/11/roasted-broccoli-cauliflower.webp",
            Serves = 2,
            CookTime = "15 minutes",
            Ingredients = [
                "1 piece broccoli",
                "1 small cauliflower",
                "1 teaspoon salt",
                "5 cloves garlic",
                "3 slices ginger",
                "2 glasses water"
            ],
            Steps = [
                new Step{
                    Content = "Wash the broccoli and cauliflower thoroughly. Cut them into small florets.",
                },
                new Step{
                    Content = "Bring 2 glasses of water to a boil in a pot. Add garlic, ginger, and salt.",
                },
                new Step{
                    Content = "Add the cauliflower and broccoli to the boiling water. Cook until they are tender but still slightly crisp.",
                },
                new Step{
                    Content = "Drain the vegetables and serve warm.",
                },
            ]
        },

        // Broccoli
        new Recipe{
            Id = Guid.Parse("5e0bbc37-9c4d-4469-b0dc-3964ec4c3f06"),
            Title = "Broccoli Cheddar Risotto",
            Description = "An easy and delicious weeknight meal featuring creamy risotto with broccoli and cheddar cheese.",
            ImageUrl = "https://img.hellofresh.com/f_auto,fl_lossy,q_auto,w_1200/hellofresh_s3/image/66c3865d5cb721784e6369a4-9e7207e9.jpeg",
            Serves = 3,
            CookTime = "30 minutes",
            Ingredients = [
                "1 cup Arborio rice",
                "4 cups chicken broth",
                "Pinch of salt and pepper",
                "2 tablespoons butter",
                "2 heads broccoli",
                "1 cup cheddar cheese"
            ],
            Steps = [
                new Step{
                    Content = "In one pan, melt the butter with salt and pepper on medium heat. In a separate pan, keep the chicken broth warm over low heat.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/60a3c1f36fdf6a88/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Chop the broccoli into bite-sized pieces.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/e343733e535f8536/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Add Arborio rice to the buttered pan and cook until aromatic, about 3 minutes.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/a8a8d3fa277aab80/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Slowly add one scoop of warm broth at a time to the risotto, stirring continuously. Allow each scoop to fully absorb before adding more. Repeat until all broth is used (about 20 minutes).",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/b7c51b893a27d8ce/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/3caa7380cd650f18/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Add broccoli and cheddar cheese to the risotto. Stir and cook together for another 5 minutes.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/98982f2523841462/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/61d073662f31cc58/640x640sq70/photo.webp" }
                }
            ]
        },

        // Tomato
        new Recipe{
            Id = Guid.Parse("48c4dc32-922c-41f3-8910-480214271e06"),
            Title = "Burrata and Cherry Tomato Gnocchi",
            Description = "My favorite gnocchi recipe and so easy for anyone to recreate!",
            ImageUrl = "https://recipecontent.fooby.ch/25239_3-2_1920-1280.jpg",
            Serves = 4,
            CookTime = "30 minutes",
            Ingredients = [
                "2 packages premade potato gnocchi",
                "2 pints cherry tomatoes",
                "5 cloves garlic",
                "1 teaspoon salt",
                "1 tablespoon pepper",
                "1 cup heavy cream",
                "1 tablespoon Italian seasoning",
                "8 oz Burrata",
                "Fresh basil",
                "Optional: Red pepper flakes"
            ],
            Steps = [
                new Step{
                    Content = "Cook the garlic on medium heat for 3 minutes. Add in cherry tomatoes and occasionally mix until jam-like (about 12 minutes).",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/18be6d827e34e9b3/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/76cff54b66010a3f/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/8f7a6b1effe07180/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "In a separate pot, prepare the gnocchi according to the package instructions. Save one cup of the pasta water before straining.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/1525c79a2ddc9031/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Add heavy cream, salt, pepper, and Italian seasoning to the cherry tomato mixture and stir well.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/34efdcb053c68e8a/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Add Burrata, cooked gnocchi, reserved pasta water, and fresh basil. Optionally, top with red pepper flakes for extra heat.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c521721fef25c930/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/c022364938dab5e0/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/22808b657cdfc341/640x640sq70/photo.webp" }
                }
            ]
        },

        // Cherry Tomato
        new Recipe{
            Id = Guid.Parse("49e3d289-e7a4-4811-be9b-75433e69096e"),
            Title = "Cherry Tomato Feta Pasta",
            Description = "A simple yet delicious pasta dish with roasted cherry tomatoes, garlic, and feta cheese.",
            ImageUrl = "https://iamafoodblog.b-cdn.net/wp-content/uploads/2021/02/baked-feta-pasta-0974.jpg",
            Serves = 4,
            CookTime = "30 minutes",
            Ingredients = [
                "1 lb cherry tomatoes",
                "1/2 lb solid feta",
                "8 cloves garlic (whole)",
                "1 lb short pasta",
                "1/4 cup olive oil",
                "Handful of basil",
                "Salt and pepper to taste",
                "1 teaspoon oregano"
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 400°F (200°C). Mix cherry tomatoes, whole garlic cloves, oregano, olive oil, salt, and pepper in a baking dish.",
                },
                new Step{
                    Content = "Place the block of feta cheese in the center of the baking dish. Drizzle with olive oil and sprinkle more oregano on top.",
                },
                new Step{
                    Content = "Bake for 25 minutes. At the same time, boil the pasta according to package instructions.",
                },
                new Step{
                    Content = "Remove the baking dish from the oven. Peel and mash the roasted garlic, then mix it with the cherry tomatoes and melted feta.",
                },
                new Step{
                    Content = "Add the cooked pasta and toss everything together. If needed, add 1/2 cup of reserved pasta water to blend the sauce. Garnish with fresh basil and serve.",
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("b8837675-31d2-4d5b-ba45-972f8b4ac38f"),
            Title = "Cherry Tomato Baked Cod",
            Description = "Buttery, flakey, and juicy cod with sweet and tart cherry tomatoes! The garlic herb butter definitely brings the cod to the next level! Try it for a busy weekday dinner!",
            ImageUrl = "https://static01.nyt.com/images/2021/05/16/dining/21help/merlin_159811149_9e520eb6-31c2-44fa-aba8-bcc56a2eb34c-jumbo.jpg",
            Serves = 1,
            CookTime = "30 minutes",
            Ingredients = [
                "2 cod fillets",
                "1/2 cup cherry tomatoes",
                "1 tbsp butter",
                "1 tbsp olive oil",
                "1/8 tsp salt",
                "1/8 tsp paprika powder",
                "1 garlic clove, chopped",
                "1 tbsp fresh chopped parsley",
                "1 tbsp lemon juice"
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 400°F (200°C).",
                },
                new Step{
                    Content = "Use a paper towel to pat dry the cod fillets.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/77f8a01f33c8fd2c/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Cut the cherry tomatoes in half. Place them in a baking dish along with the cod fillets.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/2810befadc9d9acc/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "In a small bowl, mix together butter, olive oil, salt, paprika, chopped garlic, parsley, and lemon juice.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c0fa555892058170/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/3d12daa189e88c22/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Spread the seasoning mixture evenly over the cod fillets. Bake for 15-20 minutes until the cod is cooked through and flaky.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/39a98af2f2ac5d49/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Serve hot and enjoy your delicious Cherry Tomato Baked Cod!",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/02db1e83298794ea/640x640sq70/photo.webp" }
                }
            ]
        },

        // Salmon
        new Recipe{
            Id = Guid.Parse("81349bdd-9713-4325-bd0d-930daaa26e8e"),
            Title = "Best Baked Salmon",
            Description = "A simple and flavorful baked salmon recipe with butter, lemon, and fresh herbs.",
            ImageUrl = "https://assets.epicurious.com/photos/62d6c5146b6e74298a39d06a/4:3/w_4031,h_3023,c_limit/BakedSalmon_RECIPE_04142022_9780_final.jpg",
            Serves = 4,
            CookTime = "20 minutes",
            Ingredients = [
                "4 (6 oz) salmon fillets",
                "2 tbsp melted butter",
                "1/2 tbsp lemon juice",
                "4 garlic cloves, minced",
                "Salt & pepper, to taste",
                "1 tsp finely chopped parsley",
                "1 tsp finely chopped dill"
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 375°F (190°C).",
                },
                new Step{
                    Content = "Let salmon come to room temperature for 15 minutes.",
                },
                new Step{
                    Content = "In a small bowl, mix melted butter and lemon juice. Brush the mixture over the salmon fillets.",
                },
                new Step{
                    Content = "Top the salmon with minced garlic, salt, and pepper.",
                },
                new Step{
                    Content = "Bake for 12 to 15 minutes, or until the salmon flakes easily with a fork.",
                },
                new Step{
                    Content = "Garnish with fresh parsley and dill before serving. Enjoy!",
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("a9ad629d-6229-4017-87c7-dc30adf12d98"),
            Title = "Salmon Sushi Cups",
            Description = "Easy and delicious baked salmon sushi cups with crispy nori and creamy yum yum sauce.",
            ImageUrl = "https://san-j.com/wp-content/uploads/2023/04/Salmon-Sushi-Cups-2.jpg",
            Serves = 2,
            CookTime = "20 minutes",
            Ingredients = [
                "2 sheets nori seaweed",
                "Sticky rice",
                "Salmon",
                "Yum Yum sauce (I use G Hughes)",
                "Avocado oil spray",
                "Avocado (optional)"
            ],
            Steps = [
                new Step{
                    Content = "Preheat oven to 400°F (200°C). Use kitchen scissors to cut 2 seaweed sheets into 4 pieces each, making 8 total. Cut salmon into cubes.",
                },
                new Step{
                    Content = "Using a muffin tin, add squares of seaweed, top with a spoonful of rice, then top with cubed salmon. Spray each sushi cup lightly with avocado oil.",
                },
                new Step{
                    Content = "Bake for 20 minutes. Top with yum yum sauce and optionally avocado. Enjoy!",
                }
            ]
        },

        // Snakehead Fish
        new Recipe{
            Id = Guid.Parse("d6d25c35-1b01-4c6b-b76b-3269e825bf3d"),
            Title = "Snakehead Fish with Green Pea and Tomato Curry",
            Description = "A flavorful Bengali-style curry with snakehead fish, green peas, and tomatoes.",
            ImageUrl = "https://static1.squarespace.com/static/5c089f01f93fd410a92e642a/5c0bfe22032be4a394196a85/5f334cef1d14c20ff579662c/1625019848667/DSCF3280.jpeg?format=1500w",
            Serves = 4,
            CookTime = "30 minutes",
            Ingredients = [
                "8 pieces Snakehead fish",
                "1.5 cup boiled peas",
                "1 large chopped tomato",
                "1/4 cup tomato puree",
                "2 tablespoons chopped onions",
                "3-4 green chilies",
                "2 tablespoons oil",
                "Water (to taste)",
                "Salt (to taste)",
                "1.5 teaspoons turmeric powder",
                "2 teaspoons red chili powder",
                "2 teaspoons ginger garlic paste",
                "3 tablespoons chopped coriander leaves"
            ],
            Steps = [
                new Step{
                    Content = "Heat oil in a frypan. Add chopped onions and salt, frying for 1 minute. Add ginger garlic paste and fry for another minute. Then add tomato puree, turmeric powder, red chili powder, and some water. Cover and sauté until oil separates.",
                },
                new Step{
                    Content = "Add boiled peas and Snakehead fish into the pan. Stir-fry on low flame for 5 minutes while covered.",
                },
                new Step{
                    Content = "Add boiled water as needed and cook for 3-4 minutes on medium-high flame.",
                },
                new Step{
                    Content = "Once the fish is fully cooked, add green chilies, chopped tomatoes, and coriander leaves on top.",
                },
                new Step{
                    Content = "Cover the pan and cook for 2 more minutes to infuse flavors.",
                },
                new Step{
                    Content = "Turn off the heat. Serve hot with steamed rice and enjoy this delicious Bengali delicacy.",
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("0e892d99-767e-4b8b-8d7c-03858628c4f3"),
            Title = "Grilled Snakehead Fish",
            Description = "A delicious and aromatic grilled fish dish infused with turmeric, lemongrass, and dill.",
            ImageUrl = "https://gcs.tripi.vn/public-tripi/tripi-feed/img/474352laE/image-138.jpeg",
            Serves = 2,
            CookTime = "30 minutes",
            Ingredients = [
                "1 whole snakehead fish",
                "Galangal, finely chopped",
                "Lemongrass, finely chopped",
                "Garlic, minced",
                "Chili, minced",
                "Dill, chopped (reserve some whole leaves)",
                "Turmeric powder",
                "Shallots, minced",
                "Seasoning powder",
                "MSG (optional)",
                "A little cooking oil",
                "A little fish sauce"
            ],
            Steps = [
                new Step{
                    Content = "Clean the fish thoroughly, rub with salt, and rinse well. Cut into smaller pieces. Finely chop galangal, lemongrass, garlic, chili, and shallots. Chop most of the dill, leaving some whole leaves aside.",
                },
                new Step{
                    Content = "Prepare the marinade: mix turmeric powder, chopped galangal, lemongrass, garlic, chili, shallots, chopped dill, seasoning powder, MSG, a little cooking oil, and a bit of fish sauce. Marinate the fish for 20 minutes.",
                },
                new Step{
                    Content = "Preheat the oven or air fryer. Place a sheet of aluminum foil on the tray, arrange the fish on top, and pour the remaining marinade over it. Add a few whole dill sprigs. Grill in two stages: 200°C for 10 minutes, then flip and continue at 130°C for another 10 minutes, adjusting as needed until golden brown and cooked through.",
                },
                new Step{
                    Content = "Prepare the dipping sauce: finely chop garlic, chili, and a little dill. Mix with sugar, fresh lime juice, and fish sauce to taste. Enjoy the grilled fish with this flavorful sauce!",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/d4c1fc5e7a3b9556/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/e64da44503fa92c5/640x640sq70/photo.webp" }
                }
            ]
        },

        // Carrot
        new Recipe{
            Id = Guid.Parse("ffa68fa4-4145-43f9-b476-58131b5a9936"),
            Title = "Carrot Cake Blended Overnight Oats",
            Description = "A creamy, delicious breakfast treat that tastes just like carrot cake! Perfect for a quick and nutritious morning meal.",
            ImageUrl = "https://www.nzherald.co.nz/resizer/E4bOkj4Dfgxq5yeVivekONO06vM=/arc-anglerfish-syd-prod-nzme/public/RJBSWUGWTFCRDLJBKPLFFKP6UY.jpg",
            Serves = 1,
            CookTime = "4 hours (including chilling time)",
            Ingredients = [
                "1/3 cup rolled oats",
                "1/2 cup vanilla Greek yogurt",
                "1/3 cup cottage cheese",
                "1/3 cup milk of choice",
                "1/4 cup baby carrots",
                "1/2 tablespoon agave or sweetener of choice",
                "1 tablespoon cinnamon",
                "1/2 tablespoon nutmeg",
                "1/2 tablespoon salt"
            ],
            Steps = [
                new Step{
                    Content = "Add the oats, milk, cottage cheese, baby carrots, and 1/4 cup vanilla Greek yogurt to a blender. Blend until smooth.",
                },
                new Step{
                    Content = "Add the cinnamon, nutmeg, salt, and sweetener to the blender. Blend again until all the seasonings are fully incorporated.",
                },
                new Step{
                    Content = "Pour the mixture into a container of choice and top with the remaining vanilla Greek yogurt.",
                },
                new Step{
                    Content = "Let sit in the fridge for at least 3 hours before enjoying!",
                }
            ]
        },

        // Eggplant
        new Recipe{
            Id = Guid.Parse("6c6167d7-cdc6-4bf8-a363-4f3a8f4634b9"),
            Title = "Eggplant 🍆 with Tahini and Chickpeas",
            Description = "A delicious and nutritious dish made with roasted eggplants, chickpeas, and tahini. Perfect for a healthy meal!",
            ImageUrl = "https://www.foodandwine.com/thmb/sLivLrjDuasUcWw5Bdpwia47bYA=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/FWCOOKS_CharredEggplant_FT_RECIPE_085-fa1634b0ac3e4b40bcb1b57daaee6a33.jpg",
            Serves = 3,
            CookTime = "40 minutes (plus soaking time for chickpeas)",
            Ingredients = [
                "3 Eggplants 🍆",
                "200 g chickpeas",
                "3 tbsp tahini",
                "3 tbsp lemon juice",
                "3 tbsp tamari sauce",
                "1 small piece of ginger 🫚",
                "1 bunch parsley",
                "6 cucumbers 🥒",
                "3 tomatoes 🍅",
                "1 pinch ground black pepper",
                "1 pinch salt"
            ],
            Steps = [
                new Step{
                    Content = "Soak chickpeas overnight and cook them until tender.",
                },
                new Step{
                    Content = "Preheat the oven to 400°F. Wash the eggplants and poke them with a fork. Bake for 30-40 minutes until soft.",
                },
                new Step{
                    Content = "Peel the roasted eggplants and dice them.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/f75cafab7f2a73fc/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/dc20c8fa7a0a48b2/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Mix diced eggplants with cooked chickpeas. Add lemon juice, tahini, tamari sauce, finely shredded ginger, chopped parsley, black pepper, and salt to taste. Store the mixture until ready to serve.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/6fe8ea97d85a502f/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Before serving, add 1 chopped tomato and 2 cucumbers per serving. Adjust salt and pepper to taste.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/0d1ea1444088efc8/640x640sq70/photo.webp" }
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("81b2a7fe-8dc4-473e-851d-50d535a17e46"),
            Title = "California Farm Venison Eggplant Moussaka",
            Description = "Lean flavorfull meat like ground lamb or ground venison, deer meat, is a great way to make a three layer greek moussaka with eggplant. A delicacy, usually only available to hunters. New Zealand Farmers now sell excellent frozen venison worldwide.",
            ImageUrl = "https://api.photon.aremedia.net.au/wp-content/uploads/sites/17/media/107372/9526757e-4b24-4a0e-a826-cf7d75878968.jpg?fit=480%2C436",
            Serves = 2,
            CookTime = "Prep: 30 minutes, Bake: 1 hour",
            Ingredients = [
                "1 lb eggplant, sliced 1/4” thick, salted",
                "4 tbsp California extra virgin olive oil",
                "1 lb ground venison",
                "1 large yellow onion, diced",
                "4 Roma tomatoes, diced",
                "1 tsp oregano",
                "1/2 tsp ground cinnamon",
                "1 tsp sweet paprika",
                "1 cup red cooking wine",
                "1 tsp sugar",
                "1 cup vegetable bouillon",
                "2 tbsp California extra virgin olive oil",
                "1 heaping tbsp Greek Bechamel sauce flour (like cake flour)",
                "1 tsp nutmeg",
                "1 cup heavy cream",
                "1 tsp black pepper",
                "4 fresh farm eggs",
                "Breadcrumbs for topping"
            ],
            Steps = [
                new Step{
                    Content = "Slice eggplant into 1/4” thick slices, keeping the peel on. Salt the slices and let them sit for 30 minutes to remove bitterness. Rinse and pat dry.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/6739b8601412ad0d/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/081f6ef29f958e14/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Spray eggplant slices with olive oil. Broil or bake until they develop color, but keep them soft, not crispy.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/047fa09387b79e4e/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/715e2ffb12db0226/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Prepare Greek Bechamel sauce: In a pan, fry flour with pepper and nutmeg in oil. Gradually whisk in cream until smooth. Whisk in eggs and cook until thickened.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/eca2fb5f9b176bd1/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Make the moussaka sauce: Fry diced onions and tomatoes with oregano, cinnamon, and paprika in olive oil. Add wine, broth, and sugar. Reduce the sauce until thickened.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/71e4763555895848/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/ee9c180539849d42/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Brown the ground venison in the moussaka sauce, stirring until the meat is fully cooked and the sauce is rich.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/f46af933a52bd5f2/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/48307c0cd7b31b47/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Assemble the moussaka in layers in a deep baking dish: 1/3 eggplant, 1/3 meat sauce, 1/3 Bechamel. Repeat twice. Sprinkle breadcrumbs on top.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/d36221cb249943d3/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Bake at 350°F for 45 minutes. Broil the top until crispy. Let rest for 15 minutes before serving. Enjoy!",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/df3e5bd26afac2dc/640x640sq70/photo.webp" }
                }
            ]
        },

        // Catfish
        new Recipe{
            Id = Guid.Parse("3ec16a93-2f7b-4890-88c5-3de27c49416f"),
            Title = "Baked Lemon Herb Catfish",
            Description = "A light and fresh alternative to fried catfish, baked with butter, lemon, and garden herbs for a flavorful dish.",
            ImageUrl = "https://www.lanascooking.com/wp-content/uploads/2018/09/baked-catfish-herbs-new-alt-horz-hero-1024x683.jpg",
            Serves = 1,
            CookTime = "30 minutes",
            Ingredients = [
                "1 catfish fillet (8 oz), skin removed",
                "1 Tbsp fresh herbs, finely chopped (parsley, oregano, thyme)",
                "1/2 tsp ground black pepper",
                "1/2 tsp Weber’s garlic sriracha seasoning",
                "1/8 tsp salt",
                "2 Tbsp butter, melted",
                "2 Tbsp lemon juice"
            ],
            Steps = [
                new Step{
                    Content = "Mix the fresh herbs, black pepper, salt, and garlic sriracha seasoning. Apply the mixture evenly to both sides of the catfish fillet.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/937db017e332ec11/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/bc58a5f7e845fd63/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Preheat oven to 350°F. Line a small baking pan with non-stick foil and spray with cooking spray.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/742988736ae4193d/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Place the seasoned fish in the prepared pan. Mix the melted butter and lemon juice together, then pour over the fish.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/33513c5db6721a9f/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/76311a85ab83fa64/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "Bake for 15-20 minutes until the fish flakes easily and reaches an internal temperature of 150°F. Plate the fish and spoon the butter-lemon sauce over it. Serve with your choice of sides and enjoy!",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/c14db9693f1081e0/640x640sq70/photo.webp", "https://img-global.cpcdn.com/steps/85c1632ffd9f9a73/640x640sq70/photo.webp" }
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("085cd76a-b166-4eef-8382-2aad91e86fbd"),
            Title = "Southern Fried Catfish",
            Description = "Crispy, golden-brown catfish fried to perfection with a flavorful cornmeal coating.",
            ImageUrl = "https://www.southernliving.com/thmb/YylCmjLMydgvU3DtWM18Z6btoQU=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Classic-Fried-Catfish-Step6-34024-5300ff7fdd46494babaa4d61711e1265.jpg",
            Serves = 4,
            CookTime = "1 hour 15 minutes",
            Ingredients = [
                "Catfish fillets",
                "Buttermilk",
                "Mustard",
                "1 1/2 cup Cornmeal",
                "1 Tbsp Flour",
                "2 tsp Cajun seasoning",
                "1 tsp Paprika",
                "1 tsp Onion powder",
                "1 tsp Garlic powder",
                "1 tsp Lemon pepper",
                "1 tsp Seafood boil seasoning",
                "1/4 tsp Cayenne pepper",
                "Vegetable oil (for frying)"
            ],
            Steps = [
                new Step{
                    Content = "Place the catfish fillets in a bowl with buttermilk and refrigerate for an hour.",
                },
                new Step{
                    Content = "Heat vegetable oil in a skillet over medium heat until it reaches the right frying temperature.",
                },
                new Step{
                    Content = "In a separate bowl, mix cornmeal, flour, and all seasonings until well combined.",
                },
                new Step{
                    Content = "Remove the catfish from the refrigerator, pour some mustard over it, and mix well. Dredge each fillet in the seasoned cornmeal mixture.",
                },
                new Step{
                    Content = "Fry each fillet for about 3 minutes on one side, then flip and continue cooking for 2 more minutes, until golden and crispy.",
                },
                new Step{
                    Content = "Remove from skillet, let cool slightly, garnish with parsley, and serve hot. Enjoy!",
                }
            ]
        },
        new Recipe{
            Id = Guid.Parse("f56e3110-dc48-4919-a463-12a3fda85724"),
            Title = "Pecan Sauce Catfish",
            Description = "Delicious catfish cooked in a flavorful pecan-based sauce with aromatic spices and basil.",
            ImageUrl = "https://www.dandydon.com/wp-content/uploads/2020/11/Pecan-encrusted-Catish.jpg",
            Serves = 4,
            CookTime = "45 minutes",
            Ingredients = [
                "1 kg Catfish",
                "2 Red chili peppers",
                "5 Onions",
                "2 Garlic cloves",
                "7 Candlenuts",
                "Basil leaves",
                "Fish spice mix",
                "1 tsp Salt",
                "1/4 tsp Turmeric",
                "1/2 tsp Coriander",
                "1 Glass of water"
            ],
            Steps = [
                new Step{
                    Content = "Mix the fish spices with a little bit of water to create a seasoning paste.",
                },
                new Step{
                    Content = "Use the spice mix to season the catfish evenly.",
                },
                new Step{
                    Content = "Fry the catfish until it is half-cooked, then set it aside.",
                },
                new Step{
                    Content = "Blend the red chili peppers, garlic, onions, and candlenuts into a smooth paste, then stir-fry until the color changes.",
                },
                new Step{
                    Content = "Add a glass of water and a pinch of salt to the stir-fried spice mixture and bring it to a boil.",
                },
                new Step{
                    Content = "Once the sauce is boiling, add the catfish back into the pan and let it cook through.",
                },
                new Step{
                    Content = "Add fresh basil leaves at the end for extra aroma and flavor.",
                }
            ]
        },

        // Celery
        new Recipe{
            Id = Guid.Parse("1ae9fafd-9446-4106-99df-de1517f8606b"),
            Title = "Stuffed Celery",
            Description = "A simple and refreshing appetizer made with crisp celery, sweet pickles, and creamy cheese.",
            ImageUrl = "https://zalliefamilymarkets.com/wp-content/uploads/Celery-Stuffed-with-Fig-Blue-Cheese-Spread-2000x1858.jpg",
            Serves = 2,
            CookTime = "10 minutes",
            Ingredients = [
                "Celery stalks",
                "Sweet pickles",
                "Cream cheese"
            ],
            Steps = [
                new Step{
                    Content = "Crisp celery in cold water. Chop sweet pickles combine wit cream cheese. Spread mixture in celery.",
                },
            ]
        },
        new Recipe{
            Id = Guid.Parse("9c57378a-0e5f-4def-bb60-59e404c1cf9d"),
            Title = "Buttered Beef Broth with Celery",
            Description = "A warm and comforting buttered beef broth infused with celery and spices, perfect for soothing the senses.",
            ImageUrl = "https://img.sndimg.com/food/image/upload/v1/img/recipes/58/90/1/Dh3UpyxpQUiJiZF48cew_beef%20soup%20SITE-2.jpg",
            Serves = 2,
            CookTime = "20 minutes",
            Ingredients = [
                "1 pint beef broth",
                "2 ribs celery with leaves, chopped",
                "1/2 stick butter",
                "1/2 teaspoon ground black pepper",
                "1/2 teaspoon granulated garlic powder",
                "Salt to taste",
                "2 tablespoons seasoned rice vinegar"
            ],
            Steps = [
                new Step{
                    Content = "My sinuses have been acting up really bad due to the weather changes. So it was craving these flavors. Then I was not feeling so well last night. So, I took the celery and chopped it into a bunch of little pieces, along with the leaves. Then I took, the half stick of butter and salt and melted it. I sauteed the celery inside the butter so it was a little bit soft. Then I added the rest of the spices.",
                    AttachedImageUrls = new List<string>{ "https://img-global.cpcdn.com/steps/15ce48a589092ea4/640x640sq70/photo.webp" }
                },
                new Step{
                    Content = "After the celery was tender, I added the beef broth, and simmered it along with the vinegar, for about 15 minutes. Serve hot, I hope you enjoy!!!",
                    AttachedImageUrls = new List<string>{ "https://img.sndimg.com/food/image/upload/v1/img/recipes/58/90/1/Dh3UpyxpQUiJiZF48cew_beef%20soup%20SITE-2.jpg" }
                }
            ]
        },

    ];
}
