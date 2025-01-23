using SubscriptionService.Domain.Entities;
namespace SubscriptionService.Infrastructure.Persistence.Mockup.Data;


public class EventData
{
    public static List<Event> Data = [
        new Event{
            Id = Guid.Parse("f77bbadb-8456-4229-b586-789540cfebcc"),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Name = "",
            Description = "",
            BannerImageUrl = "",
            ReductionType = EventReductionType.PERCENT,
            PriceReduction = 20,
            IsActive = true,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        }
    ];
}
