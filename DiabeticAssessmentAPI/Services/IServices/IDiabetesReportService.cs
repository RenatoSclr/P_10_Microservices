namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabetesReportService
    {
        Task GetDiabeteReportByPatientId(Guid patientId);
    }
}
