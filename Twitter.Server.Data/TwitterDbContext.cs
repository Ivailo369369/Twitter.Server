namespace Twitter.Server.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Twitter.Server.Data.Models;
    using Twitter.Server.Data.Models.Base;

    public class TwitterDbContext : IdentityDbContext<User>
    {
        public TwitterDbContext(
            DbContextOptions<TwitterDbContext> options) 
            :base(options)
        {
                
        }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConfigurationData.ConnectionString); 
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Post>()
                .HasQueryFilter(p => p.IsDeleted)
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<Profile>(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Follow>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany()
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasQueryFilter(c => c.IsDeleted)
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Comment>()
                .HasQueryFilter(c => c.IsDeleted)
                .HasOne(c => c.Post)
                .WithMany()
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .Entity<Like>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Like>()
                .HasOne(l => l.Post)
                .WithMany()
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInformation()
          => this.ChangeTracker
              .Entries()
              .ToList()
              .ForEach(entry =>
              {

                  if (entry.Entity is IDeletableEntity deletableEntity)
                  {
                      if (entry.State == EntityState.Deleted)
                      {
                          deletableEntity.DeletedOn = DateTime.UtcNow;
                          deletableEntity.IsDeleted = true;

                          entry.State = EntityState.Modified;

                          return;
                      }
                  }

                  if (entry.Entity is IEntity entity)
                  {
                      if (entry.State == EntityState.Added)
                      {
                          entity.CreatedOn = DateTime.UtcNow;
                      }
                      else if (entry.State == EntityState.Modified)
                      {
                          entity.ModifiedOn = DateTime.UtcNow;
                      }
                  }
              });
    }
}
