using Microsoft.EntityFrameworkCore;
using Potter.Domain.Entities;

namespace Potter.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().Property(x => x.Id);
            modelBuilder.Entity<Character>().Property(x => x.Name).IsRequired().HasMaxLength(120);
            modelBuilder.Entity<Character>().Property(x => x.Role).IsRequired().HasMaxLength(60);
            modelBuilder.Entity<Character>().Property(x => x.School).IsRequired().HasMaxLength(300);
            modelBuilder.Entity<Character>().Property(x => x.House).IsRequired();
            modelBuilder.Entity<Character>().Property(x => x.Patronus);
        }
    }
}
