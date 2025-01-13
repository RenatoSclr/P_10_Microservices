using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotesAPI.Domain.Dtos
{
    public class UpdateNoteDTO
    {
        public string Contenu { get; set; }
    }

}

