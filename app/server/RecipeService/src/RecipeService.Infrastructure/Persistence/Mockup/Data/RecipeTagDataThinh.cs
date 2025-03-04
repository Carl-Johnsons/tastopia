using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class RecipeTagDataThinh
{
    public static List<RecipeTag> Data => [
        new RecipeTag{
            RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"), //Moi tao
            TagId = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f6150292") // TOMATO
        },
        new RecipeTag{
            RecipeId = Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"), //Moi tao
            TagId = Guid.Parse("df3f6301-3cae-480a-87da-c7b8f61925555") // EGG
        },
    ];

}
