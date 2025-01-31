using NotificationService.Domain.Constants;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.Mockup.Data;

internal class NotificationTemplateMockup
{
    public static readonly List<NotificationTemplate> Data = [
        new NotificationTemplate{
            Id = Guid.Parse("ba49b056-11a0-46d1-8d5e-b7547ab607b7"),
            TemplateCode = NotificationTemplateCode.USER_FOLLOW,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "{0} has followed {1}" },
                {"vi", "{0} đang theo dõi {1}" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("17e0ef01-13e4-40fa-b50c-576886258f65"),
            TemplateCode = NotificationTemplateCode.USER_REPLY,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "{0} has replied {1} comment" },
                {"vi", "{0} đã trả lời bình luận của {1}" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("123dca5f-bd13-4a6d-8742-67db09be227c"),
            TemplateCode = NotificationTemplateCode.USER_COMMENT,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "{0} has commented on {1} post" },
                {"vi", "{0} đã bình luận trên bài đăng của {1}" }
            }
        },
     ];
}
