using DuendeIdentityServer.Models;

namespace DuendeIdentityServer.Data.Mockup.Data;

public static class ApplicationUserData
{
    public static IEnumerable<ApplicationUser> Data =>
             [
                new ApplicationUser{
                    Id="61c61ac7-291e-4075-9689-666ef05547ed",
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Alice Smith",
                    Gender = "Nữ",
                    AvatarUrl = "https://tapchithucung.vn/Uploads/images/nhung-giong-cho-it-bi-benh.jpg",
                    BackgroundUrl = "https://tapchithucung.vn/Uploads/images/nhung-giong-cho-it-bi-benh.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser{
                    Id="078ecc42-7643-4cff-b851-eeac5ba1bb29",
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456788",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Bob Smith",
                    Gender = "Nam",
                    AvatarUrl = "https://vnp.1cdn.vn/2023/01/19/anh-meo-6(1).jpeg",
                    BackgroundUrl = "https://vnp.1cdn.vn/2023/01/19/anh-meo-6(1).jpeg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser
                {
                    Id="1cfb7c40-cccc-4a87-88a9-ff967d8dcddb",
                    UserName = "duc",
                    Email = "taiduc@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456787",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Nguyen Le Tai Duc",
                    Gender = "Nam",
                    AvatarUrl = "https://i.pinimg.com/736x/e2/07/8b/e2078be54495947309e0f68b59346f68.jpg",
                    BackgroundUrl = "https://i.pinimg.com/736x/e2/07/8b/e2078be54495947309e0f68b59346f68.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="50e00c7f-39da-48d1-b273-3562225a5972",
                    UserName = "an",
                    Email = "minhan@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456786",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Minh An",
                    Gender = "Nam",
                    AvatarUrl = "https://moc247.com/wp-content/uploads/2023/12/loa-mat-voi-101-hinh-anh-avatar-meo-cute-dang-yeu-dep-mat_9.jpg",
                    BackgroundUrl = "https://moc247.com/wp-content/uploads/2023/12/loa-mat-voi-101-hinh-anh-avatar-meo-cute-dang-yeu-dep-mat_9.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="bb06e4ec-f371-45d5-804e-22c65c77f67d",
                    UserName = "kian",
                    Email = "kianstrong@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456785",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Kian Strong",
                    Gender = "Nam",
                    AvatarUrl = "https://cdn.britannica.com/70/234870-004-B1BA3958/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    BackgroundUrl = "https://cdn.britannica.com/70/234870-004-B1BA3958/Orange-colored-cat-yawns-displaying-teeth.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="594a3fc8-3d24-4305-a9d7-569586d0604e",
                    UserName = "cara",
                    Email = "cararose@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456784",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Cara Rose",
                    Gender = "Nam",
                    AvatarUrl = "https://www.usatoday.com/gcdn/authoring/authoring-images/2023/11/07/USAT/71489907007-shark-1-c-braun.jpg",
                    BackgroundUrl = "https://www.usatoday.com/gcdn/authoring/authoring-images/2023/11/07/USAT/71489907007-shark-1-c-braun.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="03e4b46e-b84a-43a9-a421-1b19e02023bb",
                    UserName = "raina",
                    Email = "rainaduarte@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456783",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Raina Duarte",
                    Gender = "Nam",
                    AvatarUrl = "https://kb.rspca.org.au/wp-content/uploads/2018/11/tabby-cat-stairs.jpg",
                    BackgroundUrl = "https://kb.rspca.org.au/wp-content/uploads/2018/11/tabby-cat-stairs.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="cd1c7fe9-3308-4afb-83f4-23fa1e9efba8",
                    UserName = "mac",
                    Email = "macnelson@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456782",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Mac Nelson",
                    Gender = "Nam",
                    AvatarUrl = "https://cdn2.steamgriddb.com/icon_thumb/7d66788d9bbbdc992995a492075269a5.png",
                    BackgroundUrl = "https://cdn2.steamgriddb.com/icon_thumb/7d66788d9bbbdc992995a492075269a5.png",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="76346f0e-a52c-4d94-a909-4a8cc59c8ede",
                    UserName = "lainey",
                    Email = "laineyhart@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456781",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Lainey Hart",
                    Gender = "Nam",
                    AvatarUrl = "https://lohas.nicoseiga.jp/thumb/1794731i?",
                    BackgroundUrl = "https://lohas.nicoseiga.jp/thumb/1794731i?",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                },
                new ApplicationUser {
                    Id="e797952f-1b76-4db9-81a4-8e2f5f9152ea",
                    UserName = "willa",
                    Email = "willapark@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456780",
                    PhoneNumberConfirmed = true,
                    /* Custom attribute */
                    Name = "Willa Park",
                    Gender = "Nam",
                    AvatarUrl = "https://hedwig-cf.netmarble.com/forum-common/mherosgb/futurefight_en/thumbnail/3f5b37a14e6d44b18ca7e72588ca631d_1557418504970_d.jpg",
                    BackgroundUrl = "https://hedwig-cf.netmarble.com/forum-common/mherosgb/futurefight_en/thumbnail/3f5b37a14e6d44b18ca7e72588ca631d_1557418504970_d.jpg",
                    Active = true,
                    Dob = new DateTime(2005, 12, 3)
                }
    ];
}
