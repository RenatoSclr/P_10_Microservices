using PatientsAPI.Domain;
using PatientsAPI.Domain.Dtos;
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

        public async Task AddPatient(CreateOrUpdatePatientDTO patientDto)
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
        public async Task<List<GetPatientDTO>> GetAllPatients() 
        {
            var patient = await _repository.GetAllPatients();
            return await MapToPatientDTOList(patient);
        }

        public async Task<Patient> GetPatientById(Guid id)
        {
            return await _repository.GetPatients(id);

        }

        public async Task<GetPatientDTO> GetPatientDTOById(Guid id)
        {
            var patient = await GetPatientById(id);
            return await MapToPatientDTO(patient);
        }

        public async Task UpdatePatient(CreateOrUpdatePatientDTO patientDto, Guid id)
        {
            var patient = await MapToPatientToUpdate(patientDto, await GetPatientById(id));
            await _repository.UpdatePatient(patient);
            await _repository.Save();
        }



        private async Task<Patient> MapToPatientToCreate(CreateOrUpdatePatientDTO patientDto) 
        {
            return new Patient
            {
                Nom = patientDto.Nom,
                Prenom = patientDto.Prenom,
                DateDeNaissance = patientDto.DateDeNaissance,
                GenreId = patientDto.GenreType == "Homme" ? 1 : 2,
                Adresse = patientDto.Adresse,
                NumeroTelephone = patientDto.NumeroTelephone
            };
        }

        private async Task<Patient> MapToPatientToUpdate(CreateOrUpdatePatientDTO patientDto, Patient existingPatient)
        {
            var patient = existingPatient;
            patient.Nom = patientDto.Nom;
            patient.Prenom = patientDto.Prenom;
            patient.DateDeNaissance = patient.DateDeNaissance;
            patient.Adresse = patientDto.Adresse;
            patient.NumeroTelephone = patientDto.NumeroTelephone;
            return patient;
        }

        private async Task<GetPatientDTO> MapToPatientDTO(Patient patient)
        {
            return new GetPatientDTO
            {
                PatientId = patient.PatientId,
                Nom = patient.Nom,
                Prenom = patient.Prenom,
                DateDeNaissance = patient.DateDeNaissance,
                GenreType = patient.Genre.GenreLabel,
                Adresse = patient.Adresse,
                NumeroTelephone = patient.NumeroTelephone
            };
        }

        private async Task<List<GetPatientDTO>> MapToPatientDTOList(List<Patient> patients)
        {
            var patientDtoList = new List<GetPatientDTO>();

            foreach (var patient in patients)
            {
                patientDtoList.Add(await MapToPatientDTO(patient));
            }

            return patientDtoList;
        }

    }
}
