using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Esercitazione30AprileBackEnd.Models
{
    public class Room
    {
        [BsonId]
        public ObjectId RoomID { get; set; }

        [BsonElement("nome")]
        public string? Nome { get; set; }

        [BsonElement("descrizione")]
        public string? Descrizione { get; set; }

        [BsonElement("creazione")]
        public DateOnly DataCreazione { get; set; }

        [BsonElement("creatore")]
        public string? Creatore { get; set; }

        [BsonElement]
        public List<string> Partecipanti = new List<string>();

        [BsonElement]
        public List<ObjectId> Messaggi = new List<ObjectId>();

        [BsonElement]
        public DateTime? Deleted { get; set; }
    }
}
