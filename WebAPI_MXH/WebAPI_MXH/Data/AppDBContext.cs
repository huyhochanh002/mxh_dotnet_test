using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.models;

namespace WebAPI_MXH.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}

        public DbSet<User> Users {get; set; }

        public DbSet<Post> Posts {get; set; }
        public DbSet<Comment> Comments {get; set; }
        public DbSet<Group> Groups {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("TableUser");
                entity.HasKey (e => e.Id);
                entity.HasMany<Post> (u=>u.Posts).WithOne(p=>p.Author).HasForeignKey(p=>p.AuthorId).IsRequired(false);
                entity.HasMany<Comment>(p => p.Comments).WithOne(c => c.Author).HasForeignKey(c => c.AuthorId).IsRequired(false);
            });

            modelBuilder.Entity<Post>(entity => {
                entity.ToTable("Tablepost");
                entity.HasKey(e => e.Id);
                entity.HasOne<User>(p => p.Author).WithMany(u => u.Posts).HasForeignKey(u => u.AuthorId).IsRequired(true);
                entity.HasOne<Group>(p => p.Group).WithMany(g => g.Posts).HasForeignKey(p => p.GroupId).IsRequired(false);
                entity.HasMany<Comment>(p=>p.Comments).WithOne(c=>c.post).HasForeignKey(p=>p.PostId).IsRequired(false);
            });

            modelBuilder.Entity<Comment>(entity => {
                entity.ToTable("TableComments");
                entity.HasKey(p => p.Id);
                entity.HasOne(p => p.Author).WithMany(u => u.Comments).HasForeignKey(u => u.AuthorId).IsRequired(true);
                entity.HasOne<Post>(p => p.post).WithMany(c => c.Comments).HasForeignKey(p => p.PostId).IsRequired(true);
            });

            modelBuilder.Entity<Group>(entity => {
                entity.ToTable("TableGroup");
                entity.HasKey(p => p.Id);
                entity.HasMany<Post>(g => g.Posts).WithOne(p => p.Group).HasForeignKey(p => p.GroupId).IsRequired(false);
            });


            modelBuilder.Entity<PostLiker>(entity => {
                entity.ToTable("Postliker");
                entity.HasKey(k => new {k.PostId,k.UserId }); /// Khai báo khóa kép 2 khóa
                entity.HasOne<User>(u => u.User).WithMany(u => u.PostLikers).HasForeignKey(u => u.UserId);
                entity.HasOne<Post>(p => p.Post).WithMany(u => u.PostLikers).HasForeignKey(p => p.PostId);
            });
            modelBuilder.Entity<UserGroup>(entity => {
                entity.ToTable("UserGroup");
                entity.HasKey(k => new { k.UserId, k.GroupId });
                entity.HasOne<User>(u=>u.User).WithMany(g=>g.UserGroups).HasForeignKey(u => u.UserId);
                entity.HasOne<Group>(u => u.Group).WithMany(g => g.UsersInGroup).HasForeignKey(u => u.GroupId);

            });

        }


    }
}
