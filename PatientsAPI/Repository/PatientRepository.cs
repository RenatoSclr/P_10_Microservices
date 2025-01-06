using Microsoft.EntityFrameworkCore;
using PatientsAPI.Data;
using PatientsAPI.Domain;
using PatientsAPI.Domain.IRepository;

namespace PatientsAPI.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patients)
        {
            await _context.Patients.AddAsync(patients);
        }

        public async Task DeletePatient(Patient patient)
        {
            _context.Remove(patient);
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatients(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task UpdatePatient(Patient patients)
        {
            _context.Update(patients);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
