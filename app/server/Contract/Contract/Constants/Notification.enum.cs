namespace Contract.Constants;

public enum NotificationTemplateCode
{
    USER_COMMENT,
    USER_CREATE_RECIPE,
    USER_FOLLOW,
    USER_UPVOTE,
    SYSTEM_DISABLE_RECIPE,
    ADMIN_DISABLE_RECIPE,
    ADMIN_RESTORE_RECIPE,
    ADMIN_DISABLE_COMMENT,
    ADMIN_RESTORE_COMMENT
}

public enum EntityType
{
    RECIPE,
    COMMENT,
    ADMIN,
    SYSTEM,
    USER
}
