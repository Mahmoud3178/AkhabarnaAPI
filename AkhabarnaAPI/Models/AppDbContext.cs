using Microsoft.EntityFrameworkCore;

namespace AkhabarnaAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Source> Sources { get; set; }
        public DbSet<SavedArticle> SavedArticles { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<UserSource> UserSources { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category -> Articles
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // SavedArticle composite key
            modelBuilder.Entity<SavedArticle>()
                .HasKey(sa => new { sa.UserId, sa.ArticleId });

            modelBuilder.Entity<SavedArticle>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.SavedArticles)
                .HasForeignKey(sa => sa.UserId);

            modelBuilder.Entity<SavedArticle>()
                .HasOne(sa => sa.Article)
                .WithMany(a => a.SavedArticles)
                .HasForeignKey(sa => sa.ArticleId);

            modelBuilder.Entity<UserCategory>()
         .HasKey(uc => uc.Id);
            modelBuilder.Entity<UserCategory>()
    .HasIndex(uc => new { uc.UserId, uc.CategoryId })
    .IsUnique();
            modelBuilder.Entity<UserCategory>()
             .HasOne(uc => uc.User)
             .WithMany()
             .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany()
                .HasForeignKey(uc => uc.CategoryId);


            modelBuilder.Entity<UserSource>()
         .HasKey(us => us.Id);
            modelBuilder.Entity<UserSource>()
                .HasIndex(us => new { us.UserId, us.SourceId })
                .IsUnique();
            modelBuilder.Entity<UserSource>()
          .HasOne(us => us.User)
          .WithMany()
          .HasForeignKey(us => us.UserId)
          .OnDelete(DeleteBehavior.Restrict); ;

            modelBuilder.Entity<UserSource>()
                .HasOne(us => us.Source)
                .WithMany()
                .HasForeignKey(us => us.SourceId)
                .OnDelete(DeleteBehavior.Restrict); ;
            //// RefreshToken -> User
            //modelBuilder.Entity<RefreshToken>()
            //    .HasOne(r => r.User)
            //    .WithMany(u => u.RefreshTokens)
            //    .HasForeignKey(r => r.UserId);

            //// DeviceToken -> User
            //modelBuilder.Entity<DeviceToken>()
            //    .HasOne(d => d.User)
            //    .WithMany(u => u.DeviceTokens)
            //    .HasForeignKey(d => d.UserId);

            // UserPreference -> User (One To One)
            modelBuilder.Entity<UserPreference>()
                .HasOne(up => up.User)
                .WithOne(u => u.Preference)
                .HasForeignKey<UserPreference>(up => up.UserId);

            modelBuilder.Entity<Notification>()
    .HasOne(n => n.User)
    .WithMany(u => u.Notifications)
    .HasForeignKey(n => n.UserId)
    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
