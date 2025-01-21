using CSharpFunctionalExtensions;
using Frontend.ViewModel.DiabeteViewModel;

namespace Frontend.Services.Interface
{
    public interface IDiabeteService
    {
        Task<Result<ReportDiabeteViewModel>> GetReportDiabeteByPatientId(Guid patientId, string token);
    }
}
