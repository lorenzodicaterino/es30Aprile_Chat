using Esercitazione30AprileC_.Models;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Esercitazione30AprileC_.dtos
{
    public class RoomDTO
    {

        [StringLength(250)]
        public string? Nom { get; set; }
        

        [StringLength(250)]
        public string? Des { get; set; }

        public DateOnly? Dat { get; set; }

        public string? Cre { get; set; }

        public List<string>? Par { get; set; }

        public List<ObjectId>? Mes { get; set; }
    }
}
