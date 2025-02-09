using MongoDB.EntityFrameworkCore;
using NotificationService.Domain.Constants;

namespace NotificationService.Domain.Entities;

[Collection("NotificationTemplate")]
public class NotificationTemplate : BaseMongoDBEntity
{
    public NotificationTemplateCode TemplateCode { get; set; }
    public Dictionary<string, string>? TranslationTitles { get; set; } = null!;
    public Dictionary<string, string> TranslationMessages { get; set; } = null!;
}
