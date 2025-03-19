using Newtonsoft.Json;
using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

internal class CommentData
{
    private static List<string> Comments = [];
    private static List<SeedWrongComment> WrongComments = [];
    private static readonly string SeedDataPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName!, "seeds") ?? "";

    static CommentData()
    {
        var seedCommentFile = File.ReadAllText(Path.Combine(SeedDataPath, "comments.json"));
        Comments = JsonConvert.DeserializeObject<List<string>>(seedCommentFile) ?? [];

        var seedWrongCommentFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-comments.json"));
        WrongComments = JsonConvert.DeserializeObject<List<SeedWrongComment>>(seedWrongCommentFile) ?? [];
    }

    internal static string GetRandomCommentContent()
    {
        Random random = new Random();
        int index = random.Next(Comments.Count);
        return Comments[index];
    }

    internal static SeedWrongComment GetRandomWrongCommentContent()
    {
        Random random = new Random();
        int index = random.Next(WrongComments.Count);
        return WrongComments[index];
    }

    internal static DateTime GetRandomDateTime()
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

    internal static Comment GetRandomComment()
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
    internal class SeedWrongComment
    {
        public string Content { get; set; } = null!;
        public List<string> ReasonCodes { get; set; } = null!;
        public List<string> AdditionalDetails { get; set; } = null!;
    }

}
