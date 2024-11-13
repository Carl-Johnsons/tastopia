using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;


public interface IApplicationDbContext : IDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<UserSearchTracking> UserSearchTrackings { get; set; }

    DbSet<UserTimeTracking> UserTimeTrackings { get; set; }
    DbSet<Setting> Settings { get; set; }
    DbSet<UserSetting> UserSettings { get; set; }
}
