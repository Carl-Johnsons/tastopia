using RecipeService.Domain.Entities;

namespace RecipeService.Infrastructure.Persistence.Mockup.Data;

public class TagData
{
    public static List<Tag> Data => [
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "FRIED",
            Value = "Fried",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "BOILED",
            Value = "Boiled",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "STEAMED",
            Value = "Steamed",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "BAKED",
            Value = "Baked",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "SPICY",
            Value = "Spicy",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "VEGETARIAN",
            Value = "Vegetarian",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "VEGAN",
            Value = "Vegan",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "GRILLED",
            Value = "Grilled",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "SWEET",
            Value = "Sweet",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "SALTY",
            Value = "Salty",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "ASIAN",
            Value = "Asian",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "ITALIAN",
            Value = "Italian",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "COMFORT",
            Value = "Comfort Food",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "APPETIZER",
            Value = "Appetizer",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        },
        new Tag
        {
            Id = Guid.NewGuid(),
            Code = "MAINCOURSE",
            Value = "Main Course",
            ImageUrl = "https://cdn-icons-png.freepik.com/256/737/737967.png?semt=ais_hybrid"
        }
    ];
}
