using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IDiabeteReportService
    {
        Task<Result<InfoPatientDTO>> GetReportDiabete(Guid patientId);
    }
}
