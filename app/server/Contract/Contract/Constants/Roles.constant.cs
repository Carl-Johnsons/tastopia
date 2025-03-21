namespace Contract.Constants;

public static class Roles
{
    public static class AllowedRoles
    {
        public static readonly List<string> SUPER_ADMIN = [Code.SUPER_ADMIN];
        public static readonly List<string> ADMIN = [Code.ADMIN, Code.SUPER_ADMIN];
        public static readonly List<string> USER = [Code.USER, Code.ADMIN, Code.SUPER_ADMIN];
    }
    public static class Code
    {
        public static readonly string SUPER_ADMIN = "SUPER ADMIN";
        public static readonly string ADMIN = "ADMIN";
        public static readonly string USER = "USER";
    }
}
