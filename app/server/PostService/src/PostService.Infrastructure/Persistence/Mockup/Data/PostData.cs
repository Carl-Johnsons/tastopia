using PostService.Domain.Entities;

namespace PostService.Infrastructure.Persistence.Mockup.Data;

public class Posts
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public List<string> tagIDs { get; set; } = null!;
}
internal class PostData
{
    public static IEnumerable<Posts> Data => [
            new Posts {
                Id = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
                Content = "Trường tôi nổi tiếng với nhiều câu chuyện ma quái, trong đó rùng rợn nhất là bóng ma thư viện. Ma nữ áo dài trắng, tóc đen, mặt nhợt nhạt, thường xuất hiện vào đêm khuya, lẩn khuất giữa giá sách, thở dài, khóc than. Nhiều học sinh khẳng định đã từng thấy bóng ma, nghe tiếng gõ cửa, tiếng lật sách.",
                UserId = "61c61ac7-291e-4075-9689-666ef05547ed",
                tagIDs = ["bea3020e-35ff-4cd7-92df-792419b6b0ae"]
            },
            new Posts {
                Id = "3b6a251f-de75-4637-8d73-d1b0d6390312",
                Content = "Con mèo cam nhà tôi nó lạ lắm, ngoài ăn với ngủ ra thì nó không làm gì nữa cả. Gặp con chuột cái nó giật mình bỏ chạy làm đổ bể hết mấy món đồ trên cao",
                UserId = "61c61ac7-291e-4075-9689-666ef05547ed",
                tagIDs = ["FBD3DA75-C148-4C2B-8F46-94453DF1DED4", "4bf09360-f3a6-4d04-b3f1-70c00fb3e8ef"]
            },
            new Posts {
                Id = "253ef8c2-2ac5-4021-8eb8-bfb7fe472b0d",
                Content = "Nay đi ăn cơm gà ở quá kia ngon lắm, giá cả hợp lý, chủ quán cũng thân thiện nữa. Mọi người cũng nên tới đó thử.",
                UserId = "61c61ac7-291e-4075-9689-666ef05547ed",
                tagIDs = ["FBD3DA75-C148-4C2B-8F46-94453DF1DED4"]
            },
        ];
}
