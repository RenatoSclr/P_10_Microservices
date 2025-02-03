using CSharpFunctionalExtensions;
using PatientsAPI.Domain;
using PatientsAPI.Domain.IRepository;
using PatientsAPI.Dtos;
using PatientsAPI.Services.IServices;
using System.Collections.Generic;

namespace PatientsAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddPatient(CreateOrUpdatePatientDTO patientDto)
        {
            var patient = MapToPatientToCreate(patientDto);
            var result = await _repository.AddPatient(patient);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _repository.Save();
            return result;
        }

        public async Task<Result> DeletePatient(Patient patient)
        {
           var result  = await _repository.DeletePatient(patient);

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await _repository.Save();
            return result;
        }
        public async Task<Result<List<GetPatientDTO>>> GetAllPatients() 
        {
            var patient = await _repository.GetAllPatients();
            if (patient.IsFailure)
                return Result.Failure<List<GetPatientDTO>>(patient.Error);
            
            return Result.Success(MapToPatientDTOList(patient.Value));
        }

        public async Task<Result<Patient>> GetPatientById(Guid id)
        {
            var result = await _repository.GetPatients(id);

            if (result.IsFailure)
                return Result.Failure<Patient>(result.Error);

            return result;
        }

        public async Task<Result<GetPatientDTO>> GetPatientDTOById(Guid id)
        {
            var patient = await GetPatientById(id);

            if (patient.IsFailure)
                return Result.Failure<GetPatientDTO>(patient.Error);

            return Result.Success(MapToPatientDTO(patient.Value));
        }

        public async Task<Result<PatientMinimalInfoDTO>> GetPatientMinimalInfoDTOById(Guid id)
        {
            var patient = await GetPatientById(id);

            if (patient.IsFailure)
                return Result.Failure<PatientMinimalInfoDTO>(patient.Error);

            return Result.Success(MapToPatientMinimalInfoDTO(patient.Value));
        }

        public async Task<Result> UpdatePatient(CreateOrUpdatePatientDTO patientDto, Guid id)
        {
            var patient = await GetPatientById(id);

            if (patient.IsFailure)
                return Result.Failure<GetPatientDTO>(patient.Error);

            var patientUpdated = MapToPatientToUpdate(patientDto, patient.Value);
            var result = await _repository.UpdatePatient(patientUpdated);

            if(result.IsFailure)
                return Result.Failure(result.Error);

            await _repository.Save();
            return result;
        }



        private Patient MapToPatientToCreate(CreateOrUpdatePatientDTO patientDto) 
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

        private Patient MapToPatientToUpdate(CreateOrUpdatePatientDTO patientDto, Patient existingPatient)
        {
            var patient = existingPatient;
            patient.Nom = patientDto.Nom;
            patient.Prenom = patientDto.Prenom;
            patient.DateDeNaissance = patientDto.DateDeNaissance;
            patient.Adresse = patientDto.Adresse;
            patient.NumeroTelephone = patientDto.NumeroTelephone;
            patient.GenreId = patientDto.GenreType == "Homme" ? 1 : 2;
            return patient;
        }

        private GetPatientDTO MapToPatientDTO(Patient patient)
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

        private PatientMinimalInfoDTO MapToPatientMinimalInfoDTO(Patient patient)
        {
            return new PatientMinimalInfoDTO
            {
                DateNaissance = patient.DateDeNaissance,
                Genre = patient.Genre.GenreLabel.ToString(),
            };
        }

        private List<GetPatientDTO> MapToPatientDTOList(List<Patient> patients)
        {
            var patientDtoList = new List<GetPatientDTO>();

            foreach (var patient in patients)
            {
                patientDtoList.Add(MapToPatientDTO(patient));
            }

            return patientDtoList;
        }

    }
}
