using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Frontend.ViewModel.DiabeteViewModel;

namespace Frontend.Services
{
    public class DiabeteService : IDiabeteService
    {
        private readonly IHttpService _httpService;
        public DiabeteService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<Result<ReportDiabeteViewModel>> GetReportDiabeteByPatientId(Guid patientId, string token)
        {
            var result = await _httpService.GetAsync<ReportDiabeteViewModel>($"/DiabeteReport/{patientId}", token);

            if (result.IsFailure)
                return Result.Failure<ReportDiabeteViewModel>("Erreur lors de la génération du rapport de diabète du patient");

            return Result.Success(result.Value);
        }
    }
}
