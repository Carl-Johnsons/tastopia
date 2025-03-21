namespace RecipeService.Domain.Constants;
public class ReportReasonData
{
    public static List<RecipeReason> RecipeReportReasons =
    [
        new() {
            Code = "INAPPROPRIATE_CONTENT",
            En = "Inappropriate content",
            Vi = "Nội dung không phù hợp"
        },
        new() {
            Code = "SPAM_ADVERTISEMENT",
            En = "Spam or advertisement",
            Vi = "Quảng cáo hoặc spam"
        },
        new() {
            Code = "HARASSMENT",
            En = "Harassment or bullying",
            Vi = "Quấy rối hoặc bắt nạt"
        },
        new() {
            Code = "PLAGIARISM",
            En = "Plagiarism or copyright infringement",
            Vi = "Sao chép hoặc vi phạm bản quyền"
        },
        new() {
            Code = "SCAM_FRAUD",
            En = "Scam or fraud",
            Vi = "Lừa đảo hoặc gian lận"
        },
        new() {
            Code = "EXPLICIT_CONTENT",
            En = "Explicit content (violence, 18+)",
            Vi = "Nội dung nhạy cảm (bạo lực, 18+)"
        },
        new() {
            Code = "OFFENSIVE_LANGUAGE",
            En = "Offensive language",
            Vi = "Ngôn ngữ xúc phạm"
        },
        new RecipeReason
        {
            Code = "HEALTH_RISK",
            En = "May pose health risks",
            Vi = "Có thể gây hại cho sức khỏe"
        },
    ];

    public static List<CommentReason> CommentReportReasons =
    [
        new() {
            Code = "SPAM_COMMENT",
            Vi = "Spam hoặc quảng cáo",
            En = "Spam or advertisement"
        },
        new() {
            Code = "OFFENSIVE_COMMENT",
            Vi = "Ngôn từ xúc phạm",
            En = "Offensive language"
        },
        new() {
            Code = "EXPLICIT_COMMENT",
            Vi = "Nội dung nhạy cảm (bạo lực, 18+)",
            En = "Explicit content (violence, 18+)"
        },
        new() {
            Code = "SCAM_COMMENT",
            Vi = "Nội dung lừa đảo hoặc gian lận",
            En = "Scam or fraudulent"
        },
        new() {
            Code = "MISINFORMATION_COMMENT",
            Vi = "Thông tin sai lệch",
            En = "Misinformation or misleading"
        },
        new() {
            Code = "PLAGIARIZED_COMMENT",
            Vi = "Sao chép hoặc vi phạm bản quyền",
            En = "Plagiarized or copyright-infringing"
        },
        new() {
            Code = "IRRELEVANT_COMMENT",
            Vi = "Không liên quan",
            En = "Irrelevant or off-topic"
        },
        new() {
            Code = "PERSONAL_ATTACK",
            Vi = "Công kích cá nhân",
            En = "Personal attack"
        }
    ];
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
