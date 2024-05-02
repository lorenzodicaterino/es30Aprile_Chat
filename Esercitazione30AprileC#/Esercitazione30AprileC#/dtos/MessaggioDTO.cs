using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Esercitazione30AprileC_.dtos
{
    public class MessaggioDTO
    {
        [Required]
        [StringLength(250)]
        public string? NomUte { get; set; }

        [StringLength(250)]
        public ObjectId? Sta { get; set; }

        [Required]
        public string? Con { get; set; }

        [Required]
        public DateTime? Ora { get; set; } = DateTime.Now;
    }
}
