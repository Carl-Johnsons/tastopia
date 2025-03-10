using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagDataNhan
{
    public static List<RecipeTag> Data => [
        // Stir-Fried Chicken with Bitter Melon
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"),
            TagId = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f") // Bitter Melon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("74c4e637-117c-4b66-ab6b-1bb3f16108d4"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Chicken Soup with Bamboo Shoots
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"),
            TagId = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79") // Bamboo Shoots
        },
        new RecipeTag{
            RecipeId = Guid.Parse("5c47a4c1-bca7-4a40-8a8c-6eb79e9f3b56"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Grilled Lemongrass Chicken
        new RecipeTag{
            RecipeId = Guid.Parse("34f37c02-feb4-4189-b56f-50158aab1c81"), // Newly created recipe
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("34f37c02-feb4-4189-b56f-50158aab1c81"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Stewed Silkie Chicken with Chinese Herbs
        new RecipeTag{
            RecipeId = Guid.Parse("e47f7b1d-8382-4578-9c6f-ce771f709c27"), // Newly created recipe
            TagId = Guid.Parse("c6e9d13a-c6fb-456b-8f4b-90912b91ed7e") // Silkie Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("e47f7b1d-8382-4578-9c6f-ce771f709c27"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },


        // Steamed Black Chicken with Ginseng
        new RecipeTag{
            RecipeId = Guid.Parse("96b13b8d-959e-4b39-98ba-efeeebb1cf94"), // Recipe ID
            TagId = Guid.Parse("c6e9d13a-c6fb-456b-8f4b-90912b91ed7e") // Black Chicken (Gà ác)
        },

        // Stir-fried Rice Noodles with Shrimp and Bean Sprouts
        new RecipeTag{
            RecipeId = Guid.Parse("d41111aa-0737-44ac-8a56-8ad74c700cdb"), // Recipe ID
            TagId = Guid.Parse("2449f48e-71f4-4ecc-b70e-1fa51c2e107f") // Mungbean Sprout (Giá)
        },
        new RecipeTag{
            RecipeId = Guid.Parse("d41111aa-0737-44ac-8a56-8ad74c700cdb"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp (Tôm)
        },
        new RecipeTag{
            RecipeId = Guid.Parse("d41111aa-0737-44ac-8a56-8ad74c700cdb"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot (Hành tím)
        },

        // Stir-Fried Bean Sprouts with Shrimp
        new RecipeTag{
            RecipeId = Guid.Parse("10c3a583-c700-4911-9893-531727fb4645"), // Newly created recipe
            TagId = Guid.Parse("2449f48e-71f4-4ecc-b70e-1fa51c2e107f") // Mungbean Sprout
        },
        new RecipeTag{
            RecipeId = Guid.Parse("10c3a583-c700-4911-9893-531727fb4645"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },
        new RecipeTag{
            RecipeId = Guid.Parse("10c3a583-c700-4911-9893-531727fb4645"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("10c3a583-c700-4911-9893-531727fb4645"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Steamed Chicken with Ginger
        new RecipeTag{
            RecipeId = Guid.Parse("0d0557df-80b9-421e-a951-fb64fe5ed4e8"),
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0d0557df-80b9-421e-a951-fb64fe5ed4e8"),
            TagId = Guid.Parse("c52ce7e5-7470-4f33-b25f-9fc05f5411c2") // Ginger
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0d0557df-80b9-421e-a951-fb64fe5ed4e8"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("0d0557df-80b9-421e-a951-fb64fe5ed4e8"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },


        // Caramelized Catfish with Ginger
        new RecipeTag{
            RecipeId = Guid.Parse("f00a1613-6c69-43e1-b3b5-acd96efbf92a"),
            TagId = Guid.Parse("31dc282f-d553-43f6-8ba4-ca050e1b8343") // Catfish
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f00a1613-6c69-43e1-b3b5-acd96efbf92a"),
            TagId = Guid.Parse("c52ce7e5-7470-4f33-b25f-9fc05f5411c2") // Ginger
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f00a1613-6c69-43e1-b3b5-acd96efbf92a"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Steamed Egg with Green Onion
        new RecipeTag{
            RecipeId = Guid.Parse("e36fa6bc-f2e9-400b-a06f-ea63edabce71"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("e36fa6bc-f2e9-400b-a06f-ea63edabce71"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot (optional garnish)
        },

        // Grilled Shrimp with Green Onion Oil
        new RecipeTag{
            RecipeId = Guid.Parse("2f38f470-ac7f-4da5-b2c3-d9bc12135833"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2f38f470-ac7f-4da5-b2c3-d9bc12135833"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },

        // Vietnamese Pickled Shallots
        new RecipeTag{
            RecipeId = Guid.Parse("82254e93-9eff-4dfd-8263-cce3c4a2289a"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Stuffed Bitter Melon Soup
        new RecipeTag{
            RecipeId = Guid.Parse("2ab34191-4b0a-494b-9920-c1b12c618b57"),
            TagId = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f") // Bitter Melon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2ab34191-4b0a-494b-9920-c1b12c618b57"),
            TagId = Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2ab34191-4b0a-494b-9920-c1b12c618b57"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("2ab34191-4b0a-494b-9920-c1b12c618b57"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Stir-Fried Bitter Melon with Eggs
        new RecipeTag{
            RecipeId = Guid.Parse("646f74bf-c84c-4a8c-996c-fa9861e4c8a2"),
            TagId = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f") // Bitter Melon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("646f74bf-c84c-4a8c-996c-fa9861e4c8a2"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },
        new RecipeTag{
            RecipeId = Guid.Parse("646f74bf-c84c-4a8c-996c-fa9861e4c8a2"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },

        // Bitter Melon Soup with Pork Ribs
        new RecipeTag{
            RecipeId = Guid.Parse("eefcfb9f-0c17-4698-806d-2b333e8c1f18"),
            TagId = Guid.Parse("09543e02-e6de-4dc9-bb74-86f829b8db8f") // Bitter Melon
        },
        new RecipeTag{
            RecipeId = Guid.Parse("eefcfb9f-0c17-4698-806d-2b333e8c1f18"),
            TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Pork Ribs
        },
        new RecipeTag{
            RecipeId = Guid.Parse("eefcfb9f-0c17-4698-806d-2b333e8c1f18"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("eefcfb9f-0c17-4698-806d-2b333e8c1f18"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Taro and Pork Rib Soup
        new RecipeTag{
            RecipeId = Guid.Parse("c0c1a561-1f43-4d38-b166-7633d0c96e85"),
            TagId = Guid.Parse("8fba2203-72cb-4870-a4f1-1189f804100b") // Taro
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c0c1a561-1f43-4d38-b166-7633d0c96e85"),
            TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Pork Ribs
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c0c1a561-1f43-4d38-b166-7633d0c96e85"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c0c1a561-1f43-4d38-b166-7633d0c96e85"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Taro and Chicken Curry
        new RecipeTag{
            RecipeId = Guid.Parse("d8ca76cf-fa39-4fb1-aa38-4477c503afd8"),
            TagId = Guid.Parse("8fba2203-72cb-4870-a4f1-1189f804100b") // Taro
        },
        new RecipeTag{
            RecipeId = Guid.Parse("d8ca76cf-fa39-4fb1-aa38-4477c503afd8"),
            TagId = Guid.Parse("bb1bba5e-3364-4e13-85f3-bb20ded937e6") // Chicken
        },
        new RecipeTag{
            RecipeId = Guid.Parse("d8ca76cf-fa39-4fb1-aa38-4477c503afd8"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("d8ca76cf-fa39-4fb1-aa38-4477c503afd8"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Beef and Potato Stir-Fry
        new RecipeTag{
            RecipeId = Guid.Parse("f71a70ca-7f8a-4b2b-8141-9a68eba845b3"),
            TagId = Guid.Parse("704fb7c2-bd4c-426e-9cec-f86711385e36") // Potato
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f71a70ca-7f8a-4b2b-8141-9a68eba845b3"),
            TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f71a70ca-7f8a-4b2b-8141-9a68eba845b3"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("f71a70ca-7f8a-4b2b-8141-9a68eba845b3"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Garlic Butter Roasted Potatoes
        new RecipeTag{
            RecipeId = Guid.Parse("3d8ba8d0-34a0-48c2-9f1d-cb1a9ec402c2"),
            TagId = Guid.Parse("704fb7c2-bd4c-426e-9cec-f86711385e36") // Potato
        },

        // Kiwi Yogurt Smoothie
        new RecipeTag{
            RecipeId = Guid.Parse("2c901acf-88df-45a7-a0c9-bdae174c7b2d"),
            TagId = Guid.Parse("9c31dc1f-d9d5-4e71-90ae-23b5c666a90b") // Kiwi
        },

        // Vietnamese Bamboo Shoot Soup
        new RecipeTag{
            RecipeId = Guid.Parse("c4e37757-9525-4f5e-ae77-abf2a9a143a5"),
            TagId = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79") // Bamboo Shoot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c4e37757-9525-4f5e-ae77-abf2a9a143a5"),
            TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Pork Ribs
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c4e37757-9525-4f5e-ae77-abf2a9a143a5"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("c4e37757-9525-4f5e-ae77-abf2a9a143a5"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Stir-Fried Bamboo Shoots with Beef
        new RecipeTag{
            RecipeId = Guid.Parse("1d4dfc1b-46fe-42c0-a8b1-1103bc0a3cae"),
            TagId = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79") // Bamboo Shoot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("1d4dfc1b-46fe-42c0-a8b1-1103bc0a3cae"),
            TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
        },
        new RecipeTag{
            RecipeId = Guid.Parse("1d4dfc1b-46fe-42c0-a8b1-1103bc0a3cae"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("1d4dfc1b-46fe-42c0-a8b1-1103bc0a3cae"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Spicy Bamboo Shoot and Pork Stir-Fry
        new RecipeTag{
            RecipeId = Guid.Parse("03ba2096-70fe-4d97-9837-f0736aaa790c"),
            TagId = Guid.Parse("57fc64be-4067-4303-9ee4-e255978dbc79") // Bamboo Shoot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("03ba2096-70fe-4d97-9837-f0736aaa790c"),
            TagId = Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
        },
        new RecipeTag{
            RecipeId = Guid.Parse("03ba2096-70fe-4d97-9837-f0736aaa790c"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("03ba2096-70fe-4d97-9837-f0736aaa790c"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Stir-Fried Luffa with Shrimp
        new RecipeTag{
            RecipeId = Guid.Parse("32fe68f0-c2be-4409-8df8-3d448fc6e324"),
            TagId = Guid.Parse("a5b81db6-56bf-4a07-876b-4b4286b46f8d") // Luffa
        },
        new RecipeTag{
            RecipeId = Guid.Parse("32fe68f0-c2be-4409-8df8-3d448fc6e324"),
            TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
        },
        new RecipeTag{
            RecipeId = Guid.Parse("32fe68f0-c2be-4409-8df8-3d448fc6e324"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("32fe68f0-c2be-4409-8df8-3d448fc6e324"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },

        // Luffa Soup with Minced Pork
        new RecipeTag{
            RecipeId = Guid.Parse("b1933e92-e1ed-4fe2-837d-e77bd22c89ca"),
            TagId = Guid.Parse("a5b81db6-56bf-4a07-876b-4b4286b46f8d") // Luffa
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b1933e92-e1ed-4fe2-837d-e77bd22c89ca"),
            TagId = Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b1933e92-e1ed-4fe2-837d-e77bd22c89ca"),
            TagId = Guid.Parse("a92d1705-a5d6-45bd-80cb-1e6349a6604d") // Shallot
        },
        new RecipeTag{
            RecipeId = Guid.Parse("b1933e92-e1ed-4fe2-837d-e77bd22c89ca"),
            TagId = Guid.Parse("b4888cb4-6612-4856-8b0c-91112f78230a") // Green Onion
        },
    ];

}
