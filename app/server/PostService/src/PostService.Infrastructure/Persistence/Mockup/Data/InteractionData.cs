using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Mockup.Data;


internal class InteractionData
{
    public static IEnumerable<Interaction> Data => [
            new Interaction {
                Id = Guid.Parse("4d48e460-2629-4ffe-877d-71e1683e159d"),
                Value = "👍",
                Code = "LIKE",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Hand%20gestures/Thumbs%20Up.png"
            },
            new Interaction {
                Id = Guid.Parse("82d364e5-bf8b-490a-915a-a7916960ea60"),
                Value = "💗",
                Code = "HEART",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Growing%20Heart.png"
            },
            new Interaction {
                Id = Guid.Parse("14fcfc59-6e7c-4c39-ac6d-1c9dcdf61de6"),
                Value = "😊",
                Code = "SMILE",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Slightly%20Smiling%20Face.png"
            },
            new Interaction {
                Id = Guid.Parse("84bf3baa-50b4-4ea9-870d-0100998c21ce"),
                Value = "😂",
                Code = "HAHA",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Face%20with%20Tears%20of%20Joy.png"
            },
            new Interaction {
                Id = Guid.Parse("bacd2868-eb5e-4956-9f7a-94a7ba806cd0"),
                Value = "😲",
                Code = "WOW",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Face%20with%20Open%20Mouth.png"
            },
            new Interaction {
                Id = Guid.Parse("e2c3876b-3f40-456e-8f5f-a5cd248b19f8"),
                Value = "😢",
                Code = "SAD",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Crying%20Face.png"
            },
            new Interaction {
                Id = Guid.Parse("94220781-aa32-4d52-a627-6bcbb248a390"),
                Value = "😠",
                Code = "ANGRY",
                Gif = "https://raw.githubusercontent.com/Tarikul-Islam-Anik/Animated-Fluent-Emojis/master/Emojis/Smilies/Angry%20Face.png"
            },
        ];
}
