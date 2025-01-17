using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotesAPI.Domain.Dtos
{
    public class NoteDTO
    {
        public Guid NoteId { get; set; }
        public Guid PatientId { get; set; }
        public string Contenu { get; set; }
        public DateTime DateCreatiom { get; set; }   
    }

}

