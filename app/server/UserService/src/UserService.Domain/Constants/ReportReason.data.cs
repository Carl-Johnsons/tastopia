namespace UserService.Domain.Constants;
public class ReportReasonData
{
    public static List<ReportUserReason> ReportUserReasons = new List<ReportUserReason>
    {
        new ReportUserReason
        {
            Code = "SPAM_USER",
            En = "Spam or advertisement account",
            Vi = "Tài khoản spam hoặc quảng cáo"
        },
        new ReportUserReason
        {
            Code = "FAKE_USER",
            En = "Fake account",
            Vi = "Tài khoản giả mạo"
        },
        new ReportUserReason
        {
            Code = "HARASSMENT_USER",
            En = "Harassment user",
            Vi = "Người dùng quấy rối người khác"
        },
        new ReportUserReason
        {
            Code = "OFFENSIVE_USER",
            En = "Offensive language user",
            Vi = "Người dùng sử dụng ngôn từ xúc phạm"
        },
        new ReportUserReason
        {
            Code = "SCAM_USER",
            En = "Scam or fraudulent user",
            Vi = "Tài khoản lừa đảo hoặc gian lận"
        },
        new ReportUserReason
        {
            Code = "EXPLICIT_USER",
            En = "Explicit content user (18+)",
            Vi = "Người dùng đăng nội dung nhạy cảm (18+)"
        }
    };
}

public class ReportUserReason
{
    public string Code { get; set; } = null!;
    public string Vi { get; set; } = null!;
    public string En { get; set; } = null!;
}
