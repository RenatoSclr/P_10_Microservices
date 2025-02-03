namespace NotesAPI.Dtos
{
    public class CreateNoteDTO
    {
        public Guid PatientId { get; set; }
        public string Contenu { get; set; }
    }
}
