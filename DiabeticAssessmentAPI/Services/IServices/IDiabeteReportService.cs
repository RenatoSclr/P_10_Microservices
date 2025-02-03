using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabeteReportService
    {
        Task<Result<ReportDiabeteDTO>> GetReportDiabete(Guid patientId);
    }
}
