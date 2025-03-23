namespace Contract.Constants;

public static class Roles
{
    public static class AllowedRoles
    {
        public static readonly List<string> SUPER_ADMIN = [Code.SUPER_ADMIN.ToString()];
        public static readonly List<string> ADMIN = [Code.ADMIN.ToString(), Code.SUPER_ADMIN.ToString()];
        public static readonly List<string> USER = [Code.USER.ToString(), Code.ADMIN.ToString(), Code.SUPER_ADMIN.ToString()];
    }
    public enum Code
    {
        SUPER_ADMIN,
        ADMIN,
        USER,
        GUEST
    }
}
