using Microsoft.EntityFrameworkCore;
using PatientsAPI.Domain;

namespace PatientsAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Genre) 
                .WithMany(g => g.Patients) 
                .HasForeignKey(p => p.GenreId);

            modelBuilder.Entity<Genre>().HasData(
                  new Genre { GenreId = 1, GenreLabel = "Homme" },
                  new Genre { GenreId = 2, GenreLabel = "Femme"}
              );
        }
    }
}
