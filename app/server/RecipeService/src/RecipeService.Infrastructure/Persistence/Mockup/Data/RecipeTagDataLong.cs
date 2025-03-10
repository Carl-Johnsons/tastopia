using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagDataLong {
  public static List<RecipeTag> Data => [
      // King Trumpet Mushroom Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("809aedc6-da3c-4d40-9e04-23fec17ced58"),
        TagId = Guid.Parse(
            "2212127c-069e-4ac5-97e5-7fca16819c7a") // King Trumpet Mushroom
      },
      new RecipeTag {
        RecipeId = Guid.Parse("3cd271e8-6da9-48e5-a86f-92a841428099"),
        TagId = Guid.Parse(
            "2212127c-069e-4ac5-97e5-7fca16819c7a") // King Trumpet Mushroom
      },

      // Shiitake Mushroom Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("698b75da-7407-4f52-b8ef-ff50abeb71dd"),
        TagId = Guid.Parse(
            "e61a8226-4c90-4f96-9b5d-b80268345623") // Shiitake Mushrooms
      },
      new RecipeTag {
        RecipeId = Guid.Parse("3e794493-78c9-4f59-b6e3-3dae0989d314"),
        TagId = Guid.Parse(
            "e61a8226-4c90-4f96-9b5d-b80268345623") // Shiitake Mushrooms
      },

      // Enoki Mushroom Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("6192c1bc-f8f4-4f15-9574-79fe3a544fd8"),
        TagId =
            Guid.Parse("e0c01336-f339-46c3-903b-89fb1b6b5505") // Enoki mushroom
      },
      new RecipeTag {
        RecipeId = Guid.Parse("adbfc35d-e0f5-471b-a97f-8762e3584aeb"),
        TagId =
            Guid.Parse("e0c01336-f339-46c3-903b-89fb1b6b5505") // Enoki mushroom
      },

      // Black Fungus Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("2c7c4a1f-f6a0-435e-9703-d30e4be80f5c"),
        TagId =
            Guid.Parse("2fa8f0fa-87d3-4ed0-8034-8a0fcb43e718") // Black fungus
      },
      new RecipeTag {
        RecipeId = Guid.Parse("c0a09d9c-299d-4214-827a-c0ce8e016c52"),
        TagId =
            Guid.Parse("2fa8f0fa-87d3-4ed0-8034-8a0fcb43e718") // Black fungus
      },

      // Flower Snails Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("b0082062-dc9f-4c8c-86c4-a991f086e97c"),
        TagId =
            Guid.Parse("c00c8daa-870b-41d3-90a8-a706e34c3096") // Flower Snails
      },
      new RecipeTag {
        RecipeId = Guid.Parse("f9a698fa-7104-47fe-afec-d983cfa8d637"),
        TagId =
            Guid.Parse("c00c8daa-870b-41d3-90a8-a706e34c3096") // Flower Snails
      },

      // Bell Pepper Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("973ce668-0ef9-474b-b1de-3de84f817075"),
        TagId =
            Guid.Parse("02131c94-db6a-470c-ad4c-507b1ff1c6aa") // Bell Pepper
      },
      new RecipeTag {
        RecipeId = Guid.Parse("a4e35661-acec-4c7d-9026-6f6c2d1d4607"),
        TagId =
            Guid.Parse("02131c94-db6a-470c-ad4c-507b1ff1c6aa") // Bell Pepper
      },

      // Lemongrass Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("1b4d8f34-5a70-4370-9255-6e7f6dd3e5f0"),
        TagId = Guid.Parse("3f5093c0-b059-4210-8a4f-bb92f80500d0") // Lemongrass
      },
      new RecipeTag {
        RecipeId = Guid.Parse("c93e0c70-8ea9-4606-9f84-bfbd3782c78b"),
        TagId = Guid.Parse("3f5093c0-b059-4210-8a4f-bb92f80500d0") // Lemongrass
      },

      // Pork Ribs Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("cc1cca39-2c3c-4dce-a487-466d52faaf39"),
        TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
      },
      new RecipeTag {
        RecipeId = Guid.Parse("50aaa4bd-d682-4df6-bb36-682915b91737"),
        TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
      },
      new RecipeTag {
        RecipeId = Guid.Parse("9e8cbee2-9275-4d8d-9e0a-7454e4cb525c"),
        TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
      },
      new RecipeTag {
        RecipeId = Guid.Parse("de3b30cd-cc23-4c6e-a1ca-262f076d009b"),
        TagId = Guid.Parse("3d66564d-4146-456c-a1e3-676aa6c872bc") // Prok Ribs
      },

      // Beef Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("b5c2bfe4-d3e9-48d5-a9a5-c92c445bc9e5"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
      },
      new RecipeTag {
        RecipeId = Guid.Parse("72bd5fac-18d8-496a-a5ab-952a2a9a43e5"),
        TagId = Guid.Parse("41d13b72-71c4-444b-b1f2-67cbdf4806ce") // Beef
      },

      // Ground Pork Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("4e1723cb-2227-43f9-a700-41485450a139"),
        TagId =
            Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
      },
      new RecipeTag {
        RecipeId = Guid.Parse("b87b86bd-7f60-407e-a106-10f049890c1d"),
        TagId =
            Guid.Parse("644283e4-982c-42b0-99d0-c0183759a820") // Ground Pork
      },

      // Chicken Heart Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("70b9fd56-2ea3-4fc9-9477-75d7749fc259"),
        TagId =
            Guid.Parse("0e24b473-9d14-40a5-b239-0666c6c0a920") // Chicken Heart
      },
      new RecipeTag {
        RecipeId = Guid.Parse("31fd92db-c271-421a-adad-0b8c8455972e"),
        TagId =
            Guid.Parse("0e24b473-9d14-40a5-b239-0666c6c0a920") // Chicken Heart
      },

      // Shrimp Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("aec08a9e-b7ce-466c-a7e1-877b7c94dcc9"),
        TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
      },
      new RecipeTag {
        RecipeId = Guid.Parse("485ce44f-4764-40e8-b531-86771e9ab0eb"),
        TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
      },
      new RecipeTag {
        RecipeId = Guid.Parse("ef388ae9-c38c-4b0b-9ef4-eb076b313b49"),
        TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
      },
      new RecipeTag {
        RecipeId = Guid.Parse("04788402-e7b0-4a60-a550-9073859a2857"),
        TagId = Guid.Parse("c5dcf428-c9a5-4839-b2b9-3f0c96fe1ece") // Shrimp
      },

      // Lettuce Recipes
      new RecipeTag {
        RecipeId = Guid.Parse("96c0c736-c0bc-464a-b57f-e98e9b36ff11"),
        TagId = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa") // Lettuce
      },
      new RecipeTag {
        RecipeId = Guid.Parse("40ecea83-f36e-413e-a5e0-e4d7a9a4190a"),
        TagId = Guid.Parse("d319ba66-c185-4f7a-9a53-509189791baa") // Lettuce
      }

  ];
}
