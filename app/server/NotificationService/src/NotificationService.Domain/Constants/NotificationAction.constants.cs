namespace NotificationService.Domain.Constants;

public static class NOTIFICATION_ACTION
{
    // Conversation
    public static string ADD_MEMBER_TO_GROUP { get; set; } = "ADD_MEMBER_TO_GROUP";
    public static string JOIN_CONVERSATION { get; set; } = "JOIN_CONVERSATION";
    public static string LEAVE_CONVERSATION { get; set; } = "LEAVE_CONVERSATION";

    // Post
    public static string POST_INTERACTION { get; set; } = "POST_INTERACTION";
    public static string POST_COMMENT { get; set; } = "POST_COMMENT";
    public static string POST_REPLY { get; set; } = "POST_REPLY";
    public static string POST_WARNING { get; set; } = "POST_WARNING";
    public static string POST_USER_TAG { get; set; } = "POST_USER_TAG";
    // User
    public static string SEND_FRIEND_REQUEST { get; set; } = "SEND_FRIEND_REQUEST";
    public static string ACCEPT_FRIEND_REQUEST { get; set; } = "ACCEPT_FRIEND_REQUEST";
    public static string UPDATE_PROFILE { get; set; } = "UPDATE_PROFILE";


}
