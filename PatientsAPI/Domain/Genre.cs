namespace PatientsAPI.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreLabel { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
