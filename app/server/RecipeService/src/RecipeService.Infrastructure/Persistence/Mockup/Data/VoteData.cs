using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class VoteData
{
    public static List<Vote> Data => [
        new Vote{
            Id = Guid.NewGuid(),
            Code = "UP_VOTE",
            Value = "Upvote",
            Gif = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExNXhmbGc0Z2I0ODF0bG1naXhnaDY2dzIzbG51Ym42dTV6OGZ0NWtoNSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/lQ6KHKqQaPrx7CBhPD/giphy.webp"
        },
        new Vote{
            Id = Guid.NewGuid(),
            Code = "DOWN_VOTE",
            Value = "Downvote",
            Gif = "https://media1.giphy.com/media/v1.Y2lkPTc5MGI3NjExNXhmbGc0Z2I0ODF0bG1naXhnaDY2dzIzbG51Ym42dTV6OGZ0NWtoNSZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/lQ6KHKqQaPrx7CBhPD/giphy.webp"
        }
    ]; 
}
