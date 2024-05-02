using System.ComponentModel.DataAnnotations;

namespace Esercitazione30AprileC_.dtos
{
    public class UtenteDTO
    {
        [StringLength(250)]
        public string? CodUte { get; set; }
        
        [StringLength(250)]
        public string Use { get; set; } = null!;
        
        [StringLength(250)]
        public string? Pas { get; set; }
    }
}
