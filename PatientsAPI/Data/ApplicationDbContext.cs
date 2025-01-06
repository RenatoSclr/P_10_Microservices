using Microsoft.EntityFrameworkCore;
using Patient.Domain;

namespace Patient.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patients> Patients { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patients>()
                .HasOne(p => p.Genre) 
                .WithMany(g => g.Patients) 
                .HasForeignKey(p => p.GenreId); 
        }
    }
}
