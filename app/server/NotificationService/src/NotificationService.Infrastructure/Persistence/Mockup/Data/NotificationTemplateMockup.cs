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
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} followed {IsSelf:choose(true):you|{Targets:list:{}|, }}" },
                {"vi", "{Actors:list:{}|, } đang theo dõi {IsSelf:choose(true):bạn|{Targets:list:{}|, }}" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("17e0ef01-13e4-40fa-b50c-576886258f65"),
            TemplateCode = NotificationTemplateCode.USER_REPLY,
            TranslationMessages = new Dictionary<string, string> {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} replied {IsSelf:choose(true):your comment|{Targets:list:{}|, }{Targets.Count:plural:'s comment|' comments}}" },
                {"vi", "{Actors:list:{}|, } đã trả lời bình luận của {IsSelf:choose(true):bạn|{Targets:list:{}|, }}" }
            }
        },
        new NotificationTemplate{
            Id = Guid.Parse("123dca5f-bd13-4a6d-8742-67db09be227c"),
            TemplateCode = NotificationTemplateCode.USER_COMMENT,
            TranslationMessages = new Dictionary<string, string>  {
                {"en", "{Actors:list:{}|, } {Actors.Count:choose(1):has|have} commented on {IsSelf:choose(true):your post|{Targets:list:{}|, }{Targets.Count:plural:'s post|' posts}}" },
                {"vi", "{Actors:list:{}|, } đã bình luận trên bài đăng của {IsSelf:choose(true):bạn|{Targets:list:{}|, }}" }
            }
        },
     ];
}
