using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.LogTo(System.Console.WriteLine);
        public DbSet<AppUser> Users{get;set;}

        public DbSet<UserLike> Likes{set;get;}

        public DbSet<Message> Messages{set;get;}
        protected override void OnModelCreating(ModelBuilder builder){

            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
            .HasKey(k => new {k.SourceUserId, k.LikedUserId});

            builder.Entity<UserLike>()
            .HasOne(s => s.SourceUser)
            .WithMany(l=> l.LikedUsers)
            .HasForeignKey(s=> s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
            .HasOne(s => s.LikedUser)
            .WithMany(l=> l.LikedByUsers)
            .HasForeignKey(s=> s.LikedUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Message>()
            .HasOne(u=> u.Sender)
            .WithMany(u=> u.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
            .HasOne(u=> u.Recipient)
            .WithMany(u=> u.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}