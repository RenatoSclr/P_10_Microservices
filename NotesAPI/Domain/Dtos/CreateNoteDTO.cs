namespace NotesAPI.Domain.Dtos
{
    public class CreateNoteDTO
    {
        public Guid PatientId { get; set; }
        public string NomPatient { get; set; }
        public string Contenu { get; set; }
    }
}
