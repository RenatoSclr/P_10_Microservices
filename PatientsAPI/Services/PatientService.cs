using PatientsAPI.Domain;
using PatientsAPI.Domain.IRepository;
using PatientsAPI.Services.IServices;

namespace PatientsAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPatient(Patient patient)
        {
            await _repository.AddPatient(patient);
            await _repository.Save();
        }

        public async Task DeletePatient(Patient patient)
        {
           await _repository.DeletePatient(patient);
           await _repository.Save();
        }
        public async Task<List<Patient>> GetAllPatients() 
        {
           return await _repository.GetAllPatients();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _repository.GetPatients(id);
        }

        public async Task UpdatePatient(Patient patient)
        {
            await _repository.UpdatePatient(patient);
            await _repository.Save();
        }
    }
}
