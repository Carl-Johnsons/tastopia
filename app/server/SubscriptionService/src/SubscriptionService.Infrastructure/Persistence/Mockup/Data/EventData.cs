using SubscriptionService.Domain.Entities;
namespace SubscriptionService.Infrastructure.Persistence.Mockup.Data;
public class EventData
{
    public static List<Event> Data = [
        new Event
        {
            Id = Guid.Parse("2a187a3a-6d4f-46fb-b0db-984949cc61ec"),
            StartDate = new DateTime(2025, 3, 1),
            EndDate = new DateTime(2025, 5, 31),
            Name = "Spring Festival",
            Description = "Celebrate the beauty of spring with exclusive discounts!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/spring.png_ogkwyv.jpg",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 15,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Event
        {
            Id = Guid.Parse("b76da361-c8aa-48a2-9ae2-7228720f7a36"),
            StartDate = new DateTime(2025, 6, 1),
            EndDate = new DateTime(2025, 8, 31),
            Name = "Summer Sale",
            Description = "Hot summer deals just for you!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/summer.png_bqono9.jpg",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 20,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Event
        {
            Id = Guid.Parse("6759cd44-f271-46e7-8915-f7236eee9042"),
            StartDate = new DateTime(2025, 9, 1),
            EndDate = new DateTime(2025, 11, 30),
            Name = "Autumn Festival",
            Description = "Enjoy the autumn vibes with our special offers!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/spring.png_ogkwyv.jpg",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 10,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Event
        {
            Id = Guid.Parse("563d5e09-2c3b-4cae-b51d-8caa260d7a1d"),
            StartDate = new DateTime(2025, 12, 1),
            EndDate = new DateTime(2026, 2, 28),
            Name = "Winter Wonderland Sale",
            Description = "Cozy up this winter with amazing discounts!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/winter.png_rjrnqg.jpg",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 25,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Event
        {
            Id = Guid.Parse("22970897-604c-4688-9e05-74c4e3217774"),
            StartDate = new DateTime(2026, 3, 1),
            EndDate = new DateTime(2026, 3, 31),
            Name = "Easter Special",
            Description = "Celebrate Easter with exclusive discounts!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/spring.png_ogkwyv.jpg",
            ReductionType = EventReductionType.MONEY,
            PriceReduction = 50,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        },
        new Event
        {
            Id = Guid.Parse("03c636c2-035b-4e3a-b127-a4d49630d945"),
            StartDate = new DateTime(2026, 4, 1),
            EndDate = new DateTime(2026, 4, 30),
            Name = "April Fool's Deal",
            Description = "Surprise discounts for April Fool's month!",
            BannerImageUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1737621157/default_storage/event/summer.png_bqono9.jpg",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 30,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }
    ];
}
