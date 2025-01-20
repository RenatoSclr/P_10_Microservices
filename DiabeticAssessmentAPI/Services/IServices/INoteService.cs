using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain.Dtos;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface INoteService
    {
        Task<Result<List<ContenuNotePatientDTO>>> GetContenuNotePatientAsync(Guid patientId);
    }
}
