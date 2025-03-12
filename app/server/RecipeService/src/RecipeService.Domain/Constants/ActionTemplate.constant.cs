using Contract.Constants;
namespace RecipeService.Domain.Constants;
public static class ActionTemplateConstant
{
    public static readonly List<ActionTemplate> Data = [
        new ActionTemplate{
            TemplateCode = ActionTemplateCode.USER_CREATE_RECIPE,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "Created new recipe." },
                {"vi", "Đã tạo một công thức mới." }
            }
        },
        new ActionTemplate{
            TemplateCode = ActionTemplateCode.USER_COMMENT,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "Commented." },
                {"vi", "Đã bình luận một công thức." }
            }
        },
        new ActionTemplate{
            TemplateCode = ActionTemplateCode.USER_UPVOTE,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "Upvoted." },
                {"vi", "Đã thích một công thức." }
            }
        },
        new ActionTemplate{
            TemplateCode = ActionTemplateCode.USER_DOWNVOTE,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "Downvote." },
                {"vi", "Đã không ủng hộ một công thức." }
            }
        },
        new ActionTemplate{
            TemplateCode = ActionTemplateCode.USER_BANNED,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "Account was banned." },
                {"vi", "Tài khoản đã bị chặn." }
            }
        },
        ];
    public static string GetTimeElapsed(DateTime time, string language)
    {
        if (language != "en" && language != "vn")
            throw new ArgumentException("Language must be 'en' or 'vn'");

        TimeSpan elapsed = DateTime.UtcNow - time;

        if (elapsed.TotalSeconds < 60)
            return language == "en" ? "just now" : "Vừa xong";
        if (elapsed.TotalMinutes < 60)
            return language == "en" ? $"{(int)elapsed.TotalMinutes} minutes ago" : $"{(int)elapsed.TotalMinutes} phút trước";
        if (elapsed.TotalHours < 24)
            return language == "en" ? $"{(int)elapsed.TotalHours} hours ago" : $"{(int)elapsed.TotalHours} giờ trước";
        if (elapsed.TotalDays < 30)
            return language == "en" ? $"{(int)elapsed.TotalDays} days ago" : $"{(int)elapsed.TotalDays} ngày trước";
        if (elapsed.TotalDays < 365)
            return language == "en" ? $"{(int)(elapsed.TotalDays / 30)} months ago" : $"{(int)(elapsed.TotalDays / 30)} tháng trước";

        return language == "en" ? $"{(int)(elapsed.TotalDays / 365)} years ago" : $"{(int)(elapsed.TotalDays / 365)} năm trước";
    }
}

public class ActionTemplate
{
    public ActionTemplateCode TemplateCode { get; set; }
    public Dictionary<string, string> TranslationMessages { get; set; } = null!;
}
