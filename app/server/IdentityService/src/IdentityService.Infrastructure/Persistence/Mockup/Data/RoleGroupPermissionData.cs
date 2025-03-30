namespace IdentityService.Infrastructure.Persistence.Mockup.Data;

public static class RoleGroupPermissionData
{
    public static string[] ROLES_DATA = Enum.GetNames(typeof(Contract.Constants.Roles.Code));

    public static Group[] GROUPS_DATA = [
            new(){
                Code="STATISTICS",
                Name="Statistics",
            },
            new(){
                Code="ADMINISTER RECIPES",
                Name="Administer recipes"
            },
            new(){
                Code="ADMINISTER TAGS",
                Name="Administer tags"
            },
            new(){
                Code="ADMINISTER USERS",
                Name="Administer users"
            },
            new(){
                Code="ADMINISTER REPORTS",
                Name="Administer reports"
            },
            new(){
                Code="ADMINISTER ADMINS",
                Name="Administer admins"
            },
            new(){
                Code="ADMINISTER INVOICES",
                Name="Administer invoices"
            },
            new(){
                Code="ADMINISTER SUBSCRIPTIONS",
                Name="Administer subscriptions"
            },
            new(){
                Code="ADMINISTER EVENTS",
                Name="Administer events"
            },
            new(){
                Code="ADMINISTER ROLE PERMISSIONS",
                Name="Administer role permissions"
            },
            new(){
                Code="ACTIVITY LOG",
                Name="Activity log"
            },
        ];

    public static Permission[] PERMISSIONS_DATA = [
            new(){
                Code="CREATE",
                Value="Create",
                Description="Create",
            },
            new(){
                Code="READ",
                Value="Read",
                Description="Read"
            },
            new(){
                Code="UPDATE",
                Value="Update",
                Description="Update",
            },
            new(){
                Code="DELETE",
                Value="Delete",
                Description="Delete",
            },
        ];
}
