namespace Contract.Constants;
public enum TrackingAction
{
    CREATE,
    UPDATE,
    DELETE,
    BAN,
    RESTORE,
}

public enum TrackedEntityType
{
    USER,
    ADMIN,
    SUPER_ADMIN,
    TAG,
    RECIPE,
    COMMENT
}
