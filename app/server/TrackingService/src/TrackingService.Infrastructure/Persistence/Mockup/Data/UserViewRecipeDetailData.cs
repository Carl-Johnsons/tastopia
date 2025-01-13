namespace TrackingService.Infrastructure.Persistence.Mockup.Data;

public static class UserViewRecipeDetailData
{
    private static readonly Random random = new Random();

    public static List<Guid> Accounts = [
        Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
        Guid.Parse("078ecc42-7643-4cff-b851-eeac5ba1bb29"),
        Guid.Parse("1cfb7c40-cccc-4a87-88a9-ff967d8dcddb"),
        Guid.Parse("50e00c7f-39da-48d1-b273-3562225a5972"),
        Guid.Parse("bb06e4ec-f371-45d5-804e-22c65c77f67d"),
        Guid.Parse("594a3fc8-3d24-4305-a9d7-569586d0604e"),
        Guid.Parse("03e4b46e-b84a-43a9-a421-1b19e02023bb"),
    ];

    public static List<Guid> Recipes = [
        Guid.Parse("aa626791-ee53-4390-a5a5-94c5b8096f87"),
        Guid.Parse("c8362fc3-5cff-4171-a78d-40613c748596"),
        Guid.Parse("068be5ad-dc4d-4a7b-bce7-24b4ed9fec57"),
        Guid.Parse("0a1ff224-99eb-442f-b043-0d00bf9fb1c2"),
        Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac"),
        Guid.Parse("9e9b3a16-42f1-40a3-9f60-e704e632b609"),
        Guid.Parse("d1672c31-64cc-44b5-9630-2e7f9f651234"),
        Guid.Parse("8e607e5c-8dbf-455b-9f5b-9c56e2d79a63"),
        Guid.Parse("d2189f90-6991-4901-8195-f0c12d24d900")
    ];

    public static DateTime GetRandomDateTime()
    {
        int year = random.Next(2022, 2025);
        int month = random.Next(1, 13);
        int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
        int hour = random.Next(0, 24);
        int minute = random.Next(0, 60);
        int second = random.Next(0, 60);
        return new DateTime(year, month, day, hour, minute, second);
    }
}
