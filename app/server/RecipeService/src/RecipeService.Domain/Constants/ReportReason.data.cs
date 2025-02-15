namespace RecipeService.Domain.Constants;
public class ReportReasonData
{
    public static List<RecipeReason> RecipeReportReasons = new List<RecipeReason>
    {
        new RecipeReason
        {
            Code = "INAPPROPRIATE_CONTENT",
            Eng = "Inappropriate content",
            Vn = "Nội dung không phù hợp"
        },
        new RecipeReason
        {
            Code = "SPAM_ADVERTISEMENT",
            Eng = "Spam or advertisement",
            Vn = "Quảng cáo hoặc spam"
        },
        new RecipeReason
        {
            Code = "HATE_SPEECH",
            Eng = "Hate speech",
            Vn = "Nội dung kích động thù địch"
        },
        new RecipeReason
        {
            Code = "HARASSMENT",
            Eng = "Harassment or bullying",
            Vn = "Quấy rối hoặc bắt nạt"
        },
        new RecipeReason
        {
            Code = "MISINFORMATION",
            Eng = "Misinformation or misleading content",
            Vn = "Thông tin sai lệch hoặc gây hiểu lầm"
        },
        new RecipeReason
        {
            Code = "PLAGIARISM",
            Eng = "Plagiarism or copyright infringement",
            Vn = "Sao chép hoặc vi phạm bản quyền"
        },
        new RecipeReason
        {
            Code = "SCAM_FRAUD",
            Eng = "Scam or fraud",
            Vn = "Lừa đảo hoặc gian lận"
        },
        new RecipeReason
        {
            Code = "EXPLICIT_CONTENT",
            Eng = "Explicit content (violence, 18+)",
            Vn = "Nội dung nhạy cảm (bạo lực, 18+)"
        },
        new RecipeReason
        {
            Code = "ANIMAL_CRUELTY",
            Eng = "Animal cruelty-related content",
            Vn = "Nội dung liên quan đến hành hạ động vật"
        },
        new RecipeReason
        {
            Code = "OFFENSIVE_LANGUAGE",
            Eng = "Offensive language",
            Vn = "Ngôn ngữ xúc phạm"
        },
        new RecipeReason
        {
            Code = "HEALTH_RISK",
            Eng = "Recipe may pose health risks",
            Vn = "Công thức có thể gây hại cho sức khỏe"
        },
        new RecipeReason
        {
            Code = "INCORRECT_RECIPE",
            Eng = "Incorrect or non-functional recipe",
            Vn = "Công thức sai hoặc không thể thực hiện"
        }
    };

    public static List<CommentReason> CommentReportReasons = new List<CommentReason>
    {
        new CommentReason
        {
            Code = "SPAM_COMMENT",
            Vn = "Bình luận chứa spam hoặc quảng cáo",
            Eng = "Spam or advertisement comment"
        },
        new CommentReason
        {
            Code = "HATE_SPEECH_COMMENT",
            Vn = "Bình luận mang tính kích động thù địch",
            Eng = "Hate speech comment"
        },
        new CommentReason
        {
            Code = "HARASSMENT_COMMENT",
            Vn = "Bình luận quấy rối hoặc bắt nạt",
            Eng = "Harassment or bullying comment"
        },
        new CommentReason
        {
            Code = "OFFENSIVE_COMMENT",
            Vn = "Bình luận có ngôn từ xúc phạm",
            Eng = "Offensive language comment"
        },
        new CommentReason
        {
            Code = "EXPLICIT_COMMENT",
            Vn = "Bình luận chứa nội dung nhạy cảm (bạo lực, 18+)",
            Eng = "Explicit content comment (violence, 18+)"
        },
        new CommentReason
        {
            Code = "SCAM_COMMENT",
            Vn = "Bình luận chứa nội dung lừa đảo hoặc gian lận",
            Eng = "Scam or fraudulent comment"
        },
        new CommentReason
        {
            Code = "MISINFORMATION_COMMENT",
            Vn = "Bình luận lan truyền thông tin sai lệch",
            Eng = "Misinformation or misleading comment"
        },
        new CommentReason
        {
            Code = "PLAGIARIZED_COMMENT",
            Vn = "Bình luận sao chép hoặc vi phạm bản quyền",
            Eng = "Plagiarized or copyright-infringing comment"
        },
        new CommentReason
        {
            Code = "IRRELEVANT_COMMENT",
            Vn = "Bình luận không liên quan",
            Eng = "Irrelevant or off-topic comment"
        },
        new CommentReason
        {
            Code = "PERSONAL_ATTACK",
            Vn = "Công kích cá nhân",
            Eng = "Personal attack"
        }
    };
}

public class RecipeReason
{
    public string Code { get; set; } = null!;
    public string Vn { get; set; } = null!;
    public string Eng { get; set; } = null!;
}

public class CommentReason
{
    public string Code { get; set; } = null!;
    public string Vn { get; set; } = null!;
    public string Eng { get; set; } = null!;
}
