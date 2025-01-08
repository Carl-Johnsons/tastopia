using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class CommentData
{
    private static readonly List<string> Comments = new List<string>
    {
        "This recipe looks amazing! Can't wait to try it.",
        "I tried this yesterday, and it turned out perfect. Thanks for sharing!",
        "This is such a creative idea. Love it!",
        "The instructions are so clear and easy to follow. Great job!",
        "Can I make this ahead of time?",
        "I'm excited to try this recipe—it looks delicious!",
        "This reminds me of a dish my grandmother used to make. Thank you!",
        "Looks delicious! Any tips for making it spicier?",
        "This was a hit with my family! Thank you for the recipe.",
        "Perfect for the weekend! Can't wait to serve it to my guests.",
        "I love how simple and easy this is. Great for busy days!",
        "The combination of flavors in this recipe is fantastic!",
        "This is going to be my go-to dish for potlucks. Thanks!",
        "Such a comforting and hearty meal. Exactly what I needed!",
        "I appreciate the detailed steps—it made cooking this so much easier.",
        "The presentation in the picture is stunning! Inspiring to try plating like this.",
        "I've been looking for a recipe like this for ages. Thank you!",
        "My kids absolutely loved this dish. It's going into our regular rotation!",
        "Such a unique recipe! It was a fun challenge to make.",
        "This recipe is so versatile. I can see myself using it in many variations!"
    };

    public static string GetRandomCommentContent()
    {
        Random random = new Random();
        int index = random.Next(Comments.Count);
        return Comments[index];
    }

    public static DateTime GetRandomDateTime()
    {
        Random random = new Random();
        int year = random.Next(2023, 2025);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        int hour = random.Next(0, 24);
        int minute = random.Next(0, 60);
        int second = random.Next(0, 60);
        return new DateTime(year, month, day, hour, minute, second);
    }

    public static Comment GetRandomComment()
    {
        var time = GetRandomDateTime();
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = GetRandomCommentContent(),
            CreatedAt = time,
            UpdatedAt = time,
            IsActive = true,
        };
        return comment;
    }

}
