using System;
using System.Collections.Generic;

namespace Esercitazione30AprileBackEnd.Models;

public partial class Utente
{
    public int? UtenteId { get; set; }

    public string? CodiceUtente { get; set; }

    public string? Username { get; set; } = null!;

    public string? Passw { get; set; } = null!;

    public DateTime? Deleted { get; set; }
}
