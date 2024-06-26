﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Esercitazione30AprileC_.Models
{
    public class Messaggio
    {
        [BsonId]
        public ObjectId MessaggioID { get; set; }
        
        [BsonElement("mittente")]
        public string NomeUtente { get; set; }
        
        [BsonElement("stanza")]
        public ObjectId Stanza { get; set; }

        [BsonElement("contenuto")]
        public string Contenuto { get; set; }
        
        [BsonElement("ora")]
        public DateTime Orario { get; set; }
    }
}
