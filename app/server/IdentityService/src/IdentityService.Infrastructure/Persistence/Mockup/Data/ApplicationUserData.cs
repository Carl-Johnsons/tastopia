namespace IdentityService.Infrastructure.Persistence.Mockup.Data;

public static class ApplicationAccountData
{
    public static IEnumerable<ApplicationAccount> Data =>
             [
                new ApplicationAccount{
                    Id="61c61ac7-291e-4075-9689-666ef05547ed",
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount{
                    Id="078ecc42-7643-4cff-b851-eeac5ba1bb29",
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456788",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount
                {
                    Id="1cfb7c40-cccc-4a87-88a9-ff967d8dcddb",
                    UserName = "duc",
                    Email = "taiduc@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456787",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="50e00c7f-39da-48d1-b273-3562225a5972",
                    UserName = "an",
                    Email = "minhan@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456786",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="bb06e4ec-f371-45d5-804e-22c65c77f67d",
                    UserName = "kian",
                    Email = "kianstrong@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456785",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="594a3fc8-3d24-4305-a9d7-569586d0604e",
                    UserName = "cara",
                    Email = "cararose@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456784",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="03e4b46e-b84a-43a9-a421-1b19e02023bb",
                    UserName = "raina",
                    Email = "rainaduarte@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456783",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="cd1c7fe9-3308-4afb-83f4-23fa1e9efba8",
                    UserName = "mac",
                    Email = "macnelson@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456782",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="76346f0e-a52c-4d94-a909-4a8cc59c8ede",
                    UserName = "lainey",
                    Email = "laineyhart@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456781",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                },
                new ApplicationAccount {
                    Id="e797952f-1b76-4db9-81a4-8e2f5f9152ea",
                    UserName = "willa",
                    Email = "willapark@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456780",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    IsActive = true,
                }
    ];
}
