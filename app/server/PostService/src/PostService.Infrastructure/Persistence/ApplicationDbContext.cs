using PostService.Domain.Entities;
using Interaction = PostService.Domain.Entities.Interaction;

namespace PostService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
      public ApplicationDbContext()
      {
      }
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
      {
      }

      public DbContext Instance => this;

      public DbSet<Post> Posts { get; set; }
      public DbSet<Comment> Comments { get; set; }
      public DbSet<PostComment> PostComments { get; set; }
      public DbSet<Tag> Tags { get; set; }
      public DbSet<Interaction> Interactions { get; set; }
      public DbSet<PostInteract> PostInteracts { get; set; }
      public DbSet<PostTag> PostTags { get; set; }
      public DbSet<PostReport> PostReports { get; set; }
      public DbSet<CommentReplies> CommentReplies { get; set; }
      public DbSet<UserWarning> UserWarnings { get; set; }
      public DbSet<UserContentRestrictions> UserContentRestrictions { get; set; }
      public DbSet<UserContentRestrictionsType> UserContentRestrictionsTypes { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
            DotNetEnv.Env.Load();
            var server = DotNetEnv.Env.GetString("DB_SERVER", "localhost, 2001").Trim();
            var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
            var pwd = DotNetEnv.Env.GetString("SA_PASSWORD", "Not found").Trim();

            var connectionString = $"Server={server};Database={db};User Id=sa;Password='{pwd}';TrustServerCertificate=true;MultipleActiveResultSets=True";
            Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
            {
                  sqlOptions.EnableRetryOnFailure(
                  maxRetryCount: 10,
                  maxRetryDelay: TimeSpan.FromSeconds(5),
                  errorNumbersToAdd: null);
            });
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Post>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<PostReport>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });


            modelBuilder.Entity<Interaction>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<UserWarning>(entity =>
            {
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<UserContentRestrictions>(entity =>
            {
                  entity.Property(e => e.ExpiredAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.CreatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                  entity.Property(e => e.UpdatedAt)
                    .HasConversion(v => v.ToUniversalTime(),
                                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            });

            modelBuilder.Entity<CommentReplies>(entity =>
            {
                  entity.HasOne(cr => cr.Comment)
               .WithMany()
               .HasForeignKey(ub => ub.CommentId)
               .HasConstraintName("Comment_CommentReplies")
               .OnDelete(DeleteBehavior.Cascade);

                  entity.HasOne(ub => ub.ReplyComment)
               .WithMany()
               .HasForeignKey(ub => ub.ReplyCommentId)
               .HasConstraintName("ReplyComment_CommentReplies")
               .OnDelete(DeleteBehavior.Restrict);
            });
      }
}
