using CSharpFunctionalExtensions;
using DiabeticAssessmentAPI.Domain;

namespace DiabeticAssessmentAPI.Services.IServices
{
    public interface INoteService
    {
        Task<Result<List<ContenuNotePatient>>> GetContenuNotePatientAsync(Guid patientId);
    }
}
