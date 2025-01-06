using PatientsAPI.Domain.Enum;

namespace PatientsAPI.Domain.Dtos
{
    public class PatientDTO
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public GenreType GenreType { get; set; }
        public string Adresse { get; set; }
        public string NumeroTelephone { get; set; }
    }
}
