using Contract.Constants;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.Mockup.Data;

internal class NotificationTemplateMockup
{
    public static readonly List<NotificationTemplate> Data = [
        new NotificationTemplate{
            Id = Guid.Parse("ba49b056-11a0-46d1-8d5e-b7547ab607b7"),
            TemplateCode = NotificationTemplateCode.USER_FOLLOW,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} started followed {IsSelf:choose(true):you|{Targets:list:{}|, }}" },
                {"vi", "{Actors:list:{}|, } đã bắt đầu theo dõi {IsSelf:choose(true):bạn|{Targets:list:{}|, }}" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("6c2150e7-7f34-4ce3-9701-c886145e16bb"),
            TemplateCode = NotificationTemplateCode.USER_CREATE_RECIPE,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} created a recipe \"{Targets:list:{}|, }\"." },
                {"vi", "{Actors:list:{}|, } đã tạo một công thức nấu ăn \"{Targets:list:{}|, }\"." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("123dca5f-bd13-4a6d-8742-67db09be227c"),
            TemplateCode = NotificationTemplateCode.USER_COMMENT,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} commented \"{Targets:list:{}|, }\" on your recipe." },
                {"vi", "{Actors:list:{}|, } đã bình luận \"{Targets:list:{}|, }\" trên công thức của bạn." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("3d2ff120-7ef0-4cdc-ba33-29d586bf8ccc"),
            TemplateCode = NotificationTemplateCode.USER_UPVOTE,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} liked on your recipe \"{Targets:list:{}|, }\"" },
                {"vi", "{Actors:list:{}|, } đã thích công thức \"{Targets:list:{}|, }\" của bạn" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("1cf3eeb3-1388-4335-8ef4-ee389033f335"),
            TemplateCode = NotificationTemplateCode.SYSTEM_DISABLE_RECIPE,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "Your recipe \"{Actors:list:{}|, }\" has been disabled due to a violation of community standards." },
                {"vi", "Công thức \"{Actors:list:{}|, }\" của bạn đã bị vô hiệu hóa vì vi phạm tiêu chuẩn cộng đồng." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("9fbe506c-e109-4e6e-b1ae-b2c5378cbcc6"),
            TemplateCode = NotificationTemplateCode.ADMIN_DISABLE_RECIPE,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "Your recipe \"{Actors:list:{}|, }\" has been disabled due to a violation of community standards." },
                {"vi", "Công thức \"{Actors:list:{}|, }\" của bạn đã bị vô hiệu hóa vì vi phạm tiêu chuẩn cộng đồng." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("be6fdc16-c7d5-4697-8eb7-585056df1c50"),
            TemplateCode = NotificationTemplateCode.ADMIN_RESTORE_RECIPE,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "Your recipe \"{Actors:list:{}|, }\" has been restored." },
                {"vi", "Công thức \"{Actors:list:{}|, }\" của bạn đã được khôi phục." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("bbae0bbd-70c9-43a1-8250-b114f5c9620b"),
            TemplateCode = NotificationTemplateCode.ADMIN_DISABLE_COMMENT,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "Your comment \"{Actors:list:{}|, }\" has been disabled due to a violation of community standards." },
                {"vi", "Bình luận \"{Actors:list:{}|, }\" của bạn đã bị vô hiệu hóa vì vi phạm tiêu chuẩn cộng đồng." }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("78ccbf54-edec-49f5-ae4a-c0115987bdb6"),
            TemplateCode = NotificationTemplateCode.ADMIN_RESTORE_COMMENT,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "Your comment \"{Actors:list:{}|, }\" has been restored." },
                {"vi", "Bình luận \"{Actors:list:{}|, }\" của bạn đã được khôi phục." }
            }
        },
     ];
}
