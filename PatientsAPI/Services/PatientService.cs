using Patient.Domain;
using Patient.Domain.IRepository;
using Patient.Services.IServices;

namespace Patient.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPatient(Patients patient)
        {
            await _repository.AddPatient(patient);
            await _repository.Save();
        }

        public async Task DeletePatient(Patients patient)
        {
           await _repository.DeletePatient(patient);
           await _repository.Save();
        }
        public async Task<List<Patients>> GetAllPatients() 
        {
           return await _repository.GetAllPatients();
        }

        public async Task<Patients> GetPatientById(int id)
        {
            return await _repository.GetPatients(id);
        }

        public async Task UpdatePatient(Patients patient)
        {
            await _repository.UpdatePatient(patient);
            await _repository.Save();
        }
    }
}
