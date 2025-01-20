using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabetesAlgorihmeService
    {
        string GetDiabeteReportByPatientId(InfoPatientDTO infoPatient, List<ContenuNotePatientDTO> contenuNoteDTOs);
    }
}
