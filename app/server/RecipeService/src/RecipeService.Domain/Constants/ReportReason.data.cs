namespace RecipeService.Domain.Constants;
public class ReportReasonData
{
    public static List<RecipeReason> RecipeReportReasons = new List<RecipeReason>
    {
        new RecipeReason
        {
            Code = "INAPPROPRIATE_CONTENT",
            En = "Inappropriate content",
            Vi = "Nội dung không phù hợp"
        },
        new RecipeReason
        {
            Code = "SPAM_ADVERTISEMENT",
            En = "Spam or advertisement",
            Vi = "Quảng cáo hoặc spam"
        },
        new RecipeReason
        {
            Code = "HARASSMENT",
            En = "Harassment or bullying",
            Vi = "Quấy rối hoặc bắt nạt"
        },
        new RecipeReason
        {
            Code = "PLAGIARISM",
            En = "Plagiarism or copyright infringement",
            Vi = "Sao chép hoặc vi phạm bản quyền"
        },
        new RecipeReason
        {
            Code = "SCAM_FRAUD",
            En = "Scam or fraud",
            Vi = "Lừa đảo hoặc gian lận"
        },
        new RecipeReason
        {
            Code = "EXPLICIT_CONTENT",
            En = "Explicit content (violence, 18+)",
            Vi = "Nội dung nhạy cảm (bạo lực, 18+)"
        },
        new RecipeReason
        {
            Code = "OFFENSIVE_LANGUAGE",
            En = "Offensive language",
            Vi = "Ngôn ngữ xúc phạm"
        },
        new RecipeReason
        {
            Code = "HEALTH_RISK",
            En = "Recipe may pose health risks",
            Vi = "Công thức có thể gây hại cho sức khỏe"
        },
    };

    public static List<CommentReason> CommentReportReasons = new List<CommentReason>
    {
        new CommentReason
        {
            Code = "SPAM_COMMENT",
            Vi = "Bình luận chứa spam hoặc quảng cáo",
            En = "Spam or advertisement comment"
        },
        new CommentReason
        {
            Code = "OFFENSIVE_COMMENT",
            Vi = "Bình luận có ngôn từ xúc phạm",
            En = "Offensive language comment"
        },
        new CommentReason
        {
            Code = "EXPLICIT_COMMENT",
            Vi = "Bình luận chứa nội dung nhạy cảm (bạo lực, 18+)",
            En = "Explicit content comment (violence, 18+)"
        },
        new CommentReason
        {
            Code = "SCAM_COMMENT",
            Vi = "Bình luận chứa nội dung lừa đảo hoặc gian lận",
            En = "Scam or fraudulent comment"
        },
        new CommentReason
        {
            Code = "MISINFORMATION_COMMENT",
            Vi = "Bình luận lan truyền thông tin sai lệch",
            En = "Misinformation or misleading comment"
        },
        new CommentReason
        {
            Code = "PLAGIARIZED_COMMENT",
            Vi = "Bình luận sao chép hoặc vi phạm bản quyền",
            En = "Plagiarized or copyright-infringing comment"
        },
        new CommentReason
        {
            Code = "IRRELEVANT_COMMENT",
            Vi = "Bình luận không liên quan",
            En = "Irrelevant or off-topic comment"
        },
        new CommentReason
        {
            Code = "PERSONAL_ATTACK",
            Vi = "Công kích cá nhân",
            En = "Personal attack"
        }
    };
}

public class RecipeReason
{
    public string Code { get; set; } = null!;
    public string Vi { get; set; } = null!;
    public string En { get; set; } = null!;
}

public class CommentReason
{
    public string Code { get; set; } = null!;
    public string Vi { get; set; } = null!;
    public string En { get; set; } = null!;
}
