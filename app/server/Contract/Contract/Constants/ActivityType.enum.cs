namespace Contract.Constants;

public enum ActivityType
{
    CREATE,
    UPDATE,
    DISABLE,
    RESTORE,
    MARK_COMPLETE,
    REOPEN
}

public enum ActivityEntityType
{
    RECIPE,
    COMMENT,
    TAG,
    USER,
    REPORT_RECIPE,
    REPORT_COMMENT,
    REPORT_USER
}
