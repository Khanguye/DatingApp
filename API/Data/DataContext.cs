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
    }
}