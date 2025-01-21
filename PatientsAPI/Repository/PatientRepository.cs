using CSharpFunctionalExtensions;
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

        public async Task<Result> AddPatient(Patient patients)
        {
            try
            {
                await _context.Patients.AddAsync(patients);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while adding the patient: {ex.Message}");
            }
        }

        public async Task<Result> DeletePatient(Patient patient)
        {
            try
            {
                _context.Remove(patient);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while deleting the patient: {ex.Message}");
            }          
        }

        public async Task<Result<List<Patient>>> GetAllPatients()
        {
            try
            {
                var patients =  await _context.Patients
                .Include(p => p.Genre)
                .ToListAsync();
                return Result.Success(patients);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Patient>>($"An error occurred while fetching patients: {ex.Message}");
            }
            
        }

        public async Task<Result<Patient>> GetPatients(Guid id)
        {
            try
            {
                var patient =  await _context.Patients.Include(p => p.Genre)
                    .FirstOrDefaultAsync(p => p.PatientId == id);

                if (patient == null)
                    return Result.Failure<Patient>("Patient not found");

                return Result.Success(patient);
            }
            catch (Exception ex)
            {
                return Result.Failure<Patient>($"An error occurred while fetching the patient: {ex.Message}");
            }
        }

        public async Task<Result> UpdatePatient(Patient patients)
        {
            try
            {
                _context.Update(patients);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"An error occurred while updating the patient: {ex.Message}");
            }
            
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
