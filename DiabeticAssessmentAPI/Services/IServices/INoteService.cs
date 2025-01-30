using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface INoteService
    {
        Task<Result<List<ContenuNotePatientDTO>>> GetContenuNotePatientAsync(Guid patientId);
    }
}
