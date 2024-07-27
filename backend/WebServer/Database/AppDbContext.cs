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
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>()
                .HasKey(l => l.Name);

            modelBuilder.Entity<Language>()
                .HasIndex(l => l.Code)
                .IsUnique();


            modelBuilder.Entity<User>()
                .HasOne(u => u.NativeLanguage)
                .WithMany(l => l.NativeLanguageUsers)
                .HasForeignKey(u => u.NativeLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasOne(u => u.AppLanguage)
                .WithMany(l => l.AppLanguageUsers)
                .HasForeignKey(u => u.AppLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasOne(c => c.TargetLanguage)
                .WithMany(l => l.CoursesWithTargetLanguage)
                .HasForeignKey(c => c.TargetLanguageName)
                .HasPrincipalKey(l => l.Name)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedCourses)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Course>()
                .HasMany(c => c.EnrolledUsers)
                .WithMany(u => u.EnrolledCourses)
                .UsingEntity(j => j.ToTable("CourseUsers"));

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Creator)
            //.WithMany(u => u.CreatedCourses)
            //.HasForeignKey(c => c.CreatorId)
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Creator)
            //    .WithMany(u => u.)

            //modelBuilder.Entity<Language>()
            //    .Property(l => l.Name)
            //    .HasMaxLength(100);
        }
    }
}
