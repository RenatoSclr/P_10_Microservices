using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;
using PatientsAPI.Domain.Enum;
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

        public async Task AddPatient(PatientDTO patientDto)
        {
            var patient = await MapToPatientToCreate(patientDto);
            await _repository.AddPatient(patient);
            await _repository.Save();
        }

        public async Task DeletePatient(Patient patient)
        {
           await _repository.DeletePatient(patient);
           await _repository.Save();
        }
        public async Task<List<PatientDTO>> GetAllPatients() 
        {
            var patient = await _repository.GetAllPatients();
            return await MapToPatientDTOList(patient);
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await _repository.GetPatients(id);

        }

        public async Task<PatientDTO> GetPatientDTOById(Guid id)
        {
            var patient = await GetPatientById(id);
            return await MapToPatientDTO(patient);
        }

        public async Task UpdatePatient(PatientDTO patientDto, Guid id)
        {
            var patient = await MapToPatientToUpdate(patientDto, await GetPatientById(id));
            await _repository.UpdatePatient(patient);
            await _repository.Save();
        }



        private async Task<Patient> MapToPatientToCreate(PatientDTO patientDto) 
        {
            return new Patient
            {
                Nom = patientDto.Nom,
                Prenom = patientDto.Prenom,
                DateDeNaissance = patientDto.DateDeNaissance,
                GenreId = (int)patientDto.GenreType,
                Adresse = patientDto.Adresse,
                NumeroTelephone = patientDto.NumeroTelephone
            };
        }

        private async Task<Patient> MapToPatientToUpdate(PatientDTO patientDto, Patient existingPatient)
        {
            var patient = existingPatient;
            patient.Nom = patientDto.Nom;
            patient.Prenom = patientDto.Prenom;
            patient.DateDeNaissance = patient.DateDeNaissance;
            patient.Adresse = patientDto.Adresse;
            patient.NumeroTelephone = patientDto.NumeroTelephone;
            return patient;
        }

        private async Task<PatientDTO> MapToPatientDTO(Patient patient)
        {
            return new PatientDTO
            {
                Nom = patient.Nom,
                Prenom = patient.Prenom,
                DateDeNaissance = patient.DateDeNaissance,
                GenreType = (GenreType)patient.GenreId,
                Adresse = patient.Adresse,
                NumeroTelephone = patient.NumeroTelephone
            };
        }

        private async Task<List<PatientDTO>> MapToPatientDTOList(List<Patient> patients)
        {
            var patientDtoList = new List<PatientDTO>();

            foreach (var patient in patients)
            {
                patientDtoList.Add(await MapToPatientDTO(patient));
            }

            return patientDtoList;
        }

    }
}
