using Contract.Constants;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Persistence.Mockup.Data;

internal class NotificationMockup
{
    private static List<string> RecipientIds = [
        "61c61ac7-291e-4075-9689-666ef05547ed",
        "936c85f2-6958-40fd-a201-74485ac917e0",
        "078ecc42-7643-4cff-b851-eeac5ba1bb29",
        "1cfb7c40-cccc-4a87-88a9-ff967d8dcddb",
        "50e00c7f-39da-48d1-b273-3562225a5972",
        "bb06e4ec-f371-45d5-804e-22c65c77f67d",
        "594a3fc8-3d24-4305-a9d7-569586d0604e",
        "03e4b46e-b84a-43a9-a421-1b19e02023bb",
    ];

    private static List<string> UserIds = [
        "078ecc42-7643-4cff-b851-eeac5ba1bb29",
        "1cfb7c40-cccc-4a87-88a9-ff967d8dcddb",
        "50e00c7f-39da-48d1-b273-3562225a5972",
        "bb06e4ec-f371-45d5-804e-22c65c77f67d",
        "594a3fc8-3d24-4305-a9d7-569586d0604e",
        "03e4b46e-b84a-43a9-a421-1b19e02023bb",
    ];

    private static List<string> TemplateIds = [
        "ba49b056-11a0-46d1-8d5e-b7547ab607b7",
        "17e0ef01-13e4-40fa-b50c-576886258f65",
        "123dca5f-bd13-4a6d-8742-67db09be227c"
    ];

    public static List<Notification> GenerateRandomNotifications()
    {
        Random random = new();
        List<Notification> list = [];
        int userIdIndex = 0;
        int templateIdIndex;
        for (int i = 0; i < 5; i++)
        {
            foreach (var recipientId in RecipientIds)
            {
                while (UserIds[userIdIndex] == recipientId)
                {
                    userIdIndex = random.Next(UserIds.Count);
                }
                templateIdIndex = random.Next(TemplateIds.Count);

                list.Add(new Notification
                {
                    PrimaryActors = [new Actor {
                    ActorId = UserIds[userIdIndex],
                    Type = EntityType.USER
                }],
                    SecondaryActors = [],
                    TemplateId = Guid.Parse(TemplateIds[templateIdIndex]),
                    JsonData = "{\"redirectUri\":\"" + CLIENT_URI.MOBILE.NOTIFICATION + "\"}",
                    Recipients = [new Recipient { RecipientId = Guid.Parse(recipientId), IsViewed = false }]
                });
            }
        }
        return list;
    }
}
