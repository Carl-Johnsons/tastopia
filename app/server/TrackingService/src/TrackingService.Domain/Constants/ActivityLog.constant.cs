using Contract.Constants;

namespace TrackingService.Domain.Constants;

public static class ActivityLogConstant
{
    public static string GetActivityTypeEn(ActivityType activity)
    {
        return activity.ToString().Replace("_", " ").ToLower();
    }

    public static string GetActivityTypeVi(ActivityType activity)
    {
        return ActivityTypeVi[activity].ToLower();
    }

    public static string GetActivityEntityTypeEn(ActivityEntityType entity)
    {
        return entity.ToString().Replace("_", " ").ToLower();
    }

    public static string GetActivityEntityTypeVi(ActivityEntityType entity)
    {
        return ActivityEntityVi[entity].ToLower();
    }

    private static Dictionary<ActivityType, string> ActivityTypeVi = new Dictionary<ActivityType, string>{
        {ActivityType.CREATE, "Tạo" },
        {ActivityType.UPDATE, "Cập nhật" },
        {ActivityType.DISABLE, "Vô hiệu hóa" },
        {ActivityType.RESTORE, "Khôi phục" },
        {ActivityType.MARK_COMPLETE, "Đánh dấu hoàn thành" },
        {ActivityType.REOPEN, "Mở lại" }
    };

    private static Dictionary<ActivityEntityType, string> ActivityEntityVi = new Dictionary<ActivityEntityType, string>{
        {ActivityEntityType.RECIPE, "Công thức" },
        {ActivityEntityType.COMMENT, "Bình luận" },
        {ActivityEntityType.USER, "Người dùng" },
        {ActivityEntityType.ADMIN, "Quản trị viên" },
        {ActivityEntityType.TAG, "Thẻ" },
        {ActivityEntityType.REPORT_USER, "Báo cáo người dùng" },
        {ActivityEntityType.REPORT_RECIPE, "Báo cáo công thức" },
        {ActivityEntityType.REPORT_COMMENT, "Báo cáo bình luận" },
    };

    public static string GetActivityEnDescription(ActivityType activity, ActivityEntityType entity)
    {
        return (activity.ToString() + entity.ToString()).Replace("_", " ").ToLower();
    }

    public static string GetActivityViDescription(ActivityType activity, ActivityEntityType entity)
    {
        return (ActivityTypeVi[activity] + ActivityEntityVi[entity]).ToLower();
    }

}
