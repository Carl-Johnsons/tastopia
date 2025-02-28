namespace Contract.Constants;

public enum SettingDataType
{
    String,
    Integer,
    Boolean,
    DateTime
}

public enum SETTING_KEY
{
    LANGUAGE,
    DARK_MODE,
    NOTIFICATION_COMMENT,
    NOTIFICATION_VOTE,
    NOTIFICATION_FOLLOW,
}

public static class SETTING_VALUE
{
    public enum LANGUAGE
    {
        VIETNAMESE,
        ENGLISH
    }

    public enum BOOLEAN
    {
        TRUE,
        FALSE
    }
}