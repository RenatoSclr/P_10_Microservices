namespace Patient.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string GenreLabel { get; set; }
        public ICollection<Patients> Patients { get; set; }
    }
}
