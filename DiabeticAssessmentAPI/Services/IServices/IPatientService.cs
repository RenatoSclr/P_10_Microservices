using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface IPatientService
    {
        Task<Result<InfoPatientDTO>> GetInfoPatientAsync(Guid patientId);
    }
}
