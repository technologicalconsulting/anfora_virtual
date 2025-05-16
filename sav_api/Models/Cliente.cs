using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
