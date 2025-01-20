using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabeteReportService : IDiabeteReportService
    {
        private readonly IPatientService _patientService;
        private readonly INoteService _noteService;
        public DiabeteReportService(IPatientService patientService, INoteService noteService)
        {
            _patientService = patientService;
            _noteService = noteService;  
        }

        public async Task<Result<InfoPatientDTO>> GetReportDiabete(Guid patientId)
        {
            var infoPatient = await _patientService.GetInfoPatientAsync(patientId);

            if (infoPatient.IsFailure)
                return Result.Failure<InfoPatientDTO>("Impossible de recuperer les informations du patient");

            return infoPatient.Value;
        }
    }
}
