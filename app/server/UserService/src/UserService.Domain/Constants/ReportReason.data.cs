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
            Eng = "Fake account",
            Vn = "Tài khoản giả mạo"
        },
        new ReportUserReason
        {
            Code = "HARASSMENT_USER",
            Eng = "Harassment user",
            Vn = "Người dùng quấy rối người khác"
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
        }
    };
}

public class ReportUserReason
{
    public string Code { get; set; } = null!;
    public string Vn { get; set; } = null!;
    public string Eng { get; set; } = null!;
}
