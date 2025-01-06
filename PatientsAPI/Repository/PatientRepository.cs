using Microsoft.EntityFrameworkCore;
using Patient.Data;
using Patient.Domain;
using Patient.Domain.IRepository;

namespace Patient.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patients patients)
        {
            await _context.Patients.AddAsync(patients);
        }

        public async Task DeletePatient(Patients patient)
        {
            _context.Remove(patient);
        }

        public async Task<List<Patients>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patients> GetPatients(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task UpdatePatient(Patients patients)
        {
            _context.Update(patients);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
