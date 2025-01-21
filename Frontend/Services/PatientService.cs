using CSharpFunctionalExtensions;
using Frontend.Services.Interface;
using Frontend.ViewModel.NotesViewModel;
using Frontend.ViewModel.PatientViewModel;
using Frontend.ViewModel;
using Frontend.Models;
using Microsoft.IdentityModel.Tokens;
using Frontend.ViewModel.DiabeteViewModel;

public class PatientService : IPatientService
{
    private readonly IHttpService _httpService;
    private readonly INoteService _noteService;
    private readonly IDiabeteService _diabeteService;

    public PatientService(IHttpService httpService, INoteService noteService, IDiabeteService diabeteService)
    {
        _httpService = httpService;
        _noteService = noteService;
        _diabeteService = diabeteService;
    }

    public async Task<Result<PatientDetailsViewModel>> GetDetailsPatient(Guid id, string token)
    {
        var patientResult = await _httpService.GetAsync<Patient>($"/patients/{id}", token);

        if (patientResult.IsFailure)
            return Result.Failure<PatientDetailsViewModel>("Erreur lors de la récupération du patient.");

        var patientNotes = await _noteService.GetPatientNotes(id, token);

        if (patientNotes.IsFailure)
            return Result.Failure<PatientDetailsViewModel>("Erreur lors de la récupération des notes.");

        var reportDiabete = await _diabeteService.GetReportDiabeteByPatientId(id, token);

        if (reportDiabete.IsFailure)
            return Result.Failure<PatientDetailsViewModel>("Erreur Lors de la récupération du rapport de diabete");

        return Result.Success(await MergeToPatientDetailsViewModel(patientResult.Value, patientNotes.Value, reportDiabete.Value));
    }

    public async Task<Result<List<PatientListViewModel>>> GetPatientList(string token)
    {
        var patientListResult = await _httpService.GetAsync<List<Patient>>("/patients", token);

        if (patientListResult.IsFailure)
            return Result.Failure<List<PatientListViewModel>>("Erreur lors de la récupération de la liste des patients.");

        return Result.Success(await MapToPatientListViewModel(patientListResult.Value));
    }

    public async Task<Result> AddPatient(CreateOrUpdatePatientViewModel patient, string token)
    {
        patient.Adresse ??= string.Empty;
        patient.NumeroTelephone ??= string.Empty;

        var result = await _httpService.PostAsync("/patients", patient, token);

        if (result.IsFailure)
            return Result.Failure("Erreur lors de l'ajout du patient.");

        return Result.Success();
    }

    public async Task<Result> UpdatePatient(Guid id, CreateOrUpdatePatientViewModel patient, string token)
    {
        patient.Adresse ??= string.Empty;
        patient.NumeroTelephone ??= string.Empty;

        var result = await _httpService.PutAsync($"/patients/{id}", patient, token);

        if (result.IsFailure)
            return Result.Failure("Erreur lors de la mise à jour du patient.");

        return Result.Success();
    }

    public async Task<Result> DeletePatient(Guid id, string token)
    {
        var result = await _httpService.DeleteAsync($"/patients/{id}", token);

        if (result.IsFailure)
            return Result.Failure("Erreur lors de la suppression du patient.");

        var deleteNoteResult = await _noteService.DeleteAllNotesByPatientId(id, token);

        return Result.Success();
    }

    public async Task<Result<CreateOrUpdatePatientViewModel>> GetUpdatePatient(Guid id, string token)
    {
        var patientResult = await _httpService.GetAsync<Patient>($"/patients/{id}", token);

        if (patientResult.IsFailure)
            return Result.Failure<CreateOrUpdatePatientViewModel>("Erreur lors de la récupération des informations du patient.");

        var patientViewModel = MapToCreateOrUpdatePatientViewModel(patientResult.Value);

        return Result.Success(patientViewModel);
    }

    private async Task<List<PatientListViewModel>> MapToPatientListViewModel(List<Patient> patients)
    {
        return patients.Select(patientVM => new PatientListViewModel
        {
            PatientId = patientVM.PatientId,
            Nom = patientVM.Nom,
            Prenom = patientVM.Prenom,
            GenreType = patientVM.GenreType,
        }).ToList();
    }


    private async Task<PatientDetailsViewModel> MergeToPatientDetailsViewModel(Patient patient, List<NoteSummary> notes, ReportDiabeteViewModel reportDiabete)
    {
        return new PatientDetailsViewModel
        {
            PatientId = patient.PatientId,
            Adresse = patient.Adresse,
            DateDeNaissance = patient.DateDeNaissance,
            GenreType = patient.GenreType,
            Notes = notes,
            Nom = patient.Nom,
            NumeroTelephone = patient.NumeroTelephone,
            Prenom = patient.Prenom,
            ReportDiabete = reportDiabete
        };
    }

    private CreateOrUpdatePatientViewModel MapToCreateOrUpdatePatientViewModel(Patient patient)
    {
        return new CreateOrUpdatePatientViewModel
        {
            PatientId = patient.PatientId,
            Nom = patient.Nom,
            Prenom = patient.Prenom,
            DateDeNaissance = patient.DateDeNaissance,
            GenreType = patient.GenreType,
            Adresse = patient.Adresse,
            NumeroTelephone = patient.NumeroTelephone
        };
    }
}
