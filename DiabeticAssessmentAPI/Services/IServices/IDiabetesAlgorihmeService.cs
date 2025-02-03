using DiabeticAssessmentAPI.Domain;
using DiabeticAssessmentAPI.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabetesAlgorihmeService
    {
        ReportDiabete GetDiabeteRisk(InfoPatient infoPatient, List<ContenuNotePatient> contenuNoteDTOs);
    }
}
