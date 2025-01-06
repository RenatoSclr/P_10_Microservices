namespace Patient.Domain
{
    public class Patients
    {
        public Guid PatientId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public string NumeroTelephone { get; set; }
    }
}
