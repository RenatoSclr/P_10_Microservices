using System.ComponentModel.DataAnnotations;

namespace Frontend.ViewModel.PatientViewModel
{
    public class CreateOrUpdatePatientViewModel
    {
        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "Vous devez renseigné le nom du patient")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Vous devez renseigné le prénom du patient")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Vous devez renseigné la date de naissance du patient")]
        public DateTime DateDeNaissance { get; set; }

        [Required(ErrorMessage = "Vous devez renseigné le genre du patient")]
        public string GenreType { get; set; }
        public string? Adresse { get; set; }
        public string? NumeroTelephone { get; set; }
    }
}
