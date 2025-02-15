namespace UserService.Domain.Constants;
public class ReportReasonData
{
    public static List<ReportUserReason> ReportUserReasons = new List<ReportUserReason>
    {
        new ReportUserReason
        {
            Code = "SPAM_USER",
            Eng = "Spam or advertisement account",
            Vn = "Tài khoản spam hoặc quảng cáo"
        },
        new ReportUserReason
        {
            Code = "FAKE_USER",
            Eng = "Fake or impersonation account",
            Vn = "Tài khoản giả mạo hoặc danh tính không có thật"
        },
        new ReportUserReason
        {
            Code = "HATE_SPEECH_USER",
            Eng = "Hate speech user",
            Vn = "Người dùng đăng nội dung kích động thù địch"
        },
        new ReportUserReason
        {
            Code = "HARASSMENT_USER",
            Eng = "Harassment or bullying user",
            Vn = "Người dùng quấy rối hoặc bắt nạt người khác"
        },
        new ReportUserReason
        {
            Code = "OFFENSIVE_USER",
            Eng = "Offensive language user",
            Vn = "Người dùng sử dụng ngôn từ xúc phạm"
        },
        new ReportUserReason
        {
            Code = "SCAM_USER",
            Eng = "Scam or fraudulent user",
            Vn = "Tài khoản lừa đảo hoặc gian lận"
        },
        new ReportUserReason
        {
            Code = "EXPLICIT_USER",
            Eng = "Explicit content user (18+)",
            Vn = "Người dùng đăng nội dung nhạy cảm (18+)"
        },
        new ReportUserReason
        {
            Code = "MISINFORMATION_USER",
            Eng = "Misinformation or misleading user",
            Vn = "Người dùng lan truyền thông tin sai lệch"
        },
        new ReportUserReason
        {
            Code = "MULTI_ACCOUNT_ABUSE",
            Eng = "Multi-account abuse",
            Vn = "Lạm dụng nhiều tài khoản"
        },
        new ReportUserReason
        {
            Code = "BOT_USER",
            Eng = "Bot or automated account",
            Vn = "Tài khoản có dấu hiệu là bot"
        }
    };
}

public class ReportUserReason
{
    public string Code { get; set; } = null!;
    public string Vn { get; set; } = null!;
    public string Eng { get; set; } = null!;
}
