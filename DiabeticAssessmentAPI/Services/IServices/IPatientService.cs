using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<Result<InfoPatientDTO>> GetInfoPatientAsync(Guid patientId);
    }
}
