using Frontend.Models;
using Frontend.ViewModel.NotesViewModel;
using Frontend.ViewModel.DiabeteViewModel;

namespace Frontend.ViewModel
{
    public class PatientDetailsViewModel
    {
        public Guid PatientId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string GenreType { get; set; }
        public string Adresse { get; set; }
        public string NumeroTelephone { get; set; }
        public IEnumerable<NoteSummary> Notes { get; set; }
        public ReportDiabeteViewModel ReportDiabete { get; set; }

    }
}