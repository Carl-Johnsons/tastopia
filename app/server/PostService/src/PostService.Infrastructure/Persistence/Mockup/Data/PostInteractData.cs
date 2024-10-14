namespace PostService.Infrastructure.Persistence.Mockup.Data;

public class PostInteraction
{
    public string PostId { get; set; } = null!;
    public string InteractionId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
internal class PostInteractData
{
    public static IEnumerable<PostInteraction> Data => [
            new PostInteraction {
                PostId = "3b6a251f-de75-4637-8d73-d1b0d6390312",
                InteractionId = "84bf3baa-50b4-4ea9-870d-0100998c21ce",
                UserId = "078ecc42-7643-4cff-b851-eeac5ba1bb29"
            },
            new PostInteraction {
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
                InteractionId = "e2c3876b-3f40-456e-8f5f-a5cd248b19f8",
                UserId = "078ecc42-7643-4cff-b851-eeac5ba1bb29"
            },
            new PostInteraction {
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
                InteractionId = "e2c3876b-3f40-456e-8f5f-a5cd248b19f8",
                UserId = "e797952f-1b76-4db9-81a4-8e2f5f9152ea"
            },
            new PostInteraction {
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
                InteractionId = "BACD2868-EB5E-4956-9F7A-94A7BA806CD0",
                UserId = "bb06e4ec-f371-45d5-804e-22c65c77f67d"
            },
            new PostInteraction {
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
                InteractionId = "4D48E460-2629-4FFE-877D-71E1683E159D",
                UserId = "61c61ac7-291e-4075-9689-666ef05547ed"
            }
        ];
}
