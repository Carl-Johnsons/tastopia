namespace PostService.Infrastructure.Persistence.Mockup.Data;

public class Comments
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string PostId { get; set; } = null!;
}
internal class CommentData
{
    public static IEnumerable<Comments> Data => [
            new Comments{
                Id = "b179a3ce-dabc-4ae8-a198-58bfc8420ed8",
                Content = "Hài quá haha",
                UserId = "078ecc42-7643-4cff-b851-eeac5ba1bb29",
                PostId = "3b6a251f-de75-4637-8d73-d1b0d6390312",
            },
            new Comments{
                Id = "37da06b0-78c4-4e81-bc4d-f262fd56db60",
                Content = "Không sợ không sợ",
                UserId = "078ecc42-7643-4cff-b851-eeac5ba1bb29",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
            new Comments{
                Id = "8e180e63-0a32-483e-b9f2-db14e6dc7a19",
                Content = "The thi chiu 🙂",
                UserId = "50e00c7f-39da-48d1-b273-3562225a5972",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
            new Comments{
                Id = "a168012a-f4bc-4ccd-92cd-e623cc76c592",
                Content = "Tin nay gần chỗ t :)",
                UserId = "bb06e4ec-f371-45d5-804e-22c65c77f67d",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
            new Comments{
                Id = "594a3fc8-3d24-4305-a9d7-569586d0604e",
                Content = "Tin nay gần chỗ t :)",
                UserId = "bb06e4ec-f371-45d5-804e-22c65c77f67d",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
            new Comments{
                Id = "0191eebc-1fe5-45bd-a44f-74a096abfb3a",
                Content = "Real or fake? 🤔",
                UserId = "594a3fc8-3d24-4305-a9d7-569586d0604e",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
            new Comments{
                Id = "fc590a6a-7fbf-4526-be7e-84b9288073ee",
                Content = "Câu chuyện về bóng ma thư viện ở trường tôi thực sự là một đề tài thú vị và đầy ám ảnh. Phân tích một cách chi tiết, ta có thể thấy câu chuyện này gợi lên rất nhiều yếu tố kinh dị cổ điển.\r\n\r\nĐầu tiên là hình ảnh ma nữ áo dài trắng với tóc đen và khuôn mặt nhợt nhạt. Đây là một mô-típ quen thuộc trong văn hóa kinh dị Á Đông, gợi nhớ đến các nhân vật ma quái trong những bộ phim kinh dị Nhật Bản và Hàn Quốc. Yếu tố này chắc chắn làm tăng thêm sự rùng rợn cho câu chuyện.\r\n\r\nThứ hai là bối cảnh thư viện – một nơi thường yên tĩnh và đầy sách vở. Khi đêm xuống, sự tĩnh lặng này dễ dàng biến thành nỗi sợ hãi khi ta tưởng tượng đến những tiếng động bất thường như tiếng thở dài, tiếng khóc than hay tiếng lật sách. Không gian thư viện với các giá sách cao và ánh đèn mờ cũng tạo ra những góc tối, nơi bóng ma có thể lẩn khuất và khiến mọi người lạnh sống lưng.\r\n\r\nĐiểm đặc biệt của câu chuyện là việc nhiều học sinh khẳng định đã nhìn thấy bóng ma và nghe những âm thanh kỳ quái. Điều này không chỉ làm tăng thêm tính chân thực và sự lan truyền của câu chuyện, mà còn tạo ra một cảm giác huyền bí, khiến người nghe cảm thấy tò mò và sợ hãi.\r\n\r\nTóm lại, câu chuyện về bóng ma thư viện ở trường tôi là một ví dụ điển hình của thể loại kinh dị học đường. Với những hình ảnh rùng rợn và bối cảnh ám ảnh, câu chuyện này chắc chắn đã và đang làm nhiều học sinh phải khiếp sợ và tưởng tượng ra những cảnh tượng đáng sợ mỗi khi đến thư viện vào buổi đêm. 🤓👆",
                UserId = "03e4b46e-b84a-43a9-a421-1b19e02023bb",
                PostId = "2a7c5790-26ef-4e5a-96ba-75fca384c7cc",
            },
        ];
}
