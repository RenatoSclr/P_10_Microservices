using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabetesAlgorihmeService
    {
        string GetDiabeteRisk(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs);
        IEnumerable<string> getTriggers(List<ContenuNotePatientDTO> contenuNoteDTOs);
    }
}
