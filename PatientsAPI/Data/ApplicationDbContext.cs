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
                  new Genre { GenreId = 1, GenreLabel = "Masculin" },
                  new Genre { GenreId = 2, GenreLabel = "Féminin" }
              );

            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = new Guid("ccc9e063-c800-43d5-924a-08dd2e6bc8f4"), Nom = "TestNone", Prenom = "Test", DateDeNaissance = new DateTime(1966, 12, 31), GenreId = 2, Adresse = "1 Brookside St", NumeroTelephone = "100-222-3333" },
                new Patient { PatientId = new Guid("157d6514-89de-431b-924b-08dd2e6bc8f4"), Nom = "TestBorderline", Prenom = "Test", DateDeNaissance = new DateTime(1945, 06, 24), GenreId = 1, Adresse = "2 High St", NumeroTelephone = "200-333-4444" },
                new Patient { PatientId = new Guid("415b6bbb-bd43-4d04-924c-08dd2e6bc8f4"), Nom = "TestInDanger", Prenom = "Test", DateDeNaissance = new DateTime(2004, 06, 18), GenreId = 1, Adresse = "3 Club Road", NumeroTelephone = "300-444-5555" },
                new Patient { PatientId = new Guid("94a687bd-5ad7-4596-924d-08dd2e6bc8f4"), Nom = "TestEarlyOnset", Prenom = "Test", DateDeNaissance = new DateTime(2002, 06, 28), GenreId = 2, Adresse = "4 Valley Dr", NumeroTelephone = "400-555-6666" }
             );
        }
    }
}
