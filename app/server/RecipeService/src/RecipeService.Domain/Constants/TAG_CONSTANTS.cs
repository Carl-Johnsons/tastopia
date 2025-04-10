using Contract.Constants;

namespace RecipeService.Domain.Constants;

public class TAG_CONSTANTS
{
    public readonly static int TAG_LIMIT = 20;
    public readonly static int ADMIN_TAG_LIMIT = 6;
    public readonly static int TAG_RANKING_LIMIT = 10;

    private readonly static Dictionary<TagCategory, string> TAG_CATEGORY_VI_STRING = new Dictionary<TagCategory, string>
    {
        { TagCategory.All, "Tất cả" },
        { TagCategory.Ingredient, "Nguyên liệu" },
        { TagCategory.DishType, "Loại món ăn" }
    };

    public static string GetTagCategoryTranslate(TagCategory category, string lang)
    {
        if(lang == LanguageValidation.En)
        {
            return category.ToString();
        }

        if (lang == LanguageValidation.Vi)
        {
            return TAG_CATEGORY_VI_STRING[category];
        }

        return "";
    }
}
