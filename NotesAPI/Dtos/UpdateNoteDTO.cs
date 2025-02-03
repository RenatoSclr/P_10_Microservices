using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NotesAPI.Dtos
{
    public class UpdateNoteDTO
    {
        public string Contenu { get; set; }
    }

}

