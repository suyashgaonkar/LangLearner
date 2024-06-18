using LangLearner.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LangLearner.Database
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>()
                .HasKey(l => l.Name);

            modelBuilder.Entity<Language>()
                .HasIndex(l => l.Code)
                .IsUnique();



            //modelBuilder.Entity<Language>()
            //    .Property(l => l.Name)
            //    .HasMaxLength(100);
        }
    }
}
