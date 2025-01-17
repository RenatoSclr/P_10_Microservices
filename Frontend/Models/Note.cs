namespace Frontend.Models
{
    public class Note
    {
        public Guid NoteId { get; set; }
        public Guid PatientId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCreatiom { get; set; }
    }
}
