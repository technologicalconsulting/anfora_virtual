using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public int? CampañaId { get; set; }

    public int? ClienteId { get; set; }

    public string? TransaccionId { get; set; }

    public string? Estado { get; set; }

    public DateTime FechaEmision { get; set; }

    public virtual Campaña? Campaña { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();

    public virtual ICollection<TicketHistorial> TicketHistorials { get; set; } = new List<TicketHistorial>();
}
