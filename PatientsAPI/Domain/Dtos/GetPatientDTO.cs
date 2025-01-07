namespace PatientsAPI.Domain.Dtos
{
    public class GetPatientDTO
    {
        public Guid PatientId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public string GenreType { get; set; }
        public string Adresse { get; set; }
        public string NumeroTelephone { get; set; }
    }
}
