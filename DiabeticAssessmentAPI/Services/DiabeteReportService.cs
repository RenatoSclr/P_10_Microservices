using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;
using DiabeticAssessmentAPI.Services.IServices;

namespace DiabeticAssessmentAPI.Services
{
    public class DiabeteReportService : IDiabeteReportService
    {
        private readonly IPatientService _patientService;
        private readonly INoteService _noteService;
        private readonly IDiabetesAlgorihmeService _diabetesAlgorihmeService;
        public DiabeteReportService(IPatientService patientService, INoteService noteService, IDiabetesAlgorihmeService diabetesAlgorihmeService)
        {
            _patientService = patientService;
            _noteService = noteService;  
            _diabetesAlgorihmeService = diabetesAlgorihmeService;
        }

        public async Task<Result<ReportDiabeteDTO>> GetReportDiabete(Guid patientId)
        {
            var infoPatient = await _patientService.GetInfoPatientAsync(patientId);

            if (infoPatient.IsFailure)
                return Result.Failure<ReportDiabeteDTO>("Impossible de recuperer les informations du patient");

            var contentNotePatient = await _noteService.GetContenuNotePatientAsync(patientId);

            if(contentNotePatient.IsFailure)
                return Result.Failure<ReportDiabeteDTO>("Impossible de recuperer le contenu des notes du patient");

            var diabeteRisk = _diabetesAlgorihmeService.GetDiabeteRisk(infoPatient.Value, contentNotePatient.Value);

            return Result.Success(diabeteRisk);
        }
    }
}
