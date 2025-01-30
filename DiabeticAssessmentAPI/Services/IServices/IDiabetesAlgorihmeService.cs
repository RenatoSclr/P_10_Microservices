using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabetesAlgorihmeService
    {
        ReportDiabeteDTO GetDiabeteRisk(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs);
    }
}
