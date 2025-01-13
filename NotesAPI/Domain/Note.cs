using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesAPI.Domain
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid NoteId { get; set; }

        [BsonElement("Patient_Id")]
        [BsonRepresentation(BsonType.String)]
        public Guid PatientId { get; set; }

        [BsonElement("Nom_Patient")]
        [BsonRepresentation(BsonType.String)]
        public string NomPatient { get; set; }

        [BsonElement("Contenu")]
        [BsonRepresentation(BsonType.String)]
        public string Contenu { get; set; }

        [BsonElement("Date_Creation")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateCreatiom { get; set; }
    }
}
