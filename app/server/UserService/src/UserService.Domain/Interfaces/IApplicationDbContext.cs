using Contract.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<UserReport> UserReports { get; set; }

    DbSet<UserFollow> UserFollows { get; set; }
    DbSet<Setting> Settings { get; set; }
    DbSet<UserSetting> UserSettings { get; set; }
}
