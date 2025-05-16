using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Ganador
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public int? ClienteId { get; set; }

    public int? CampañaId { get; set; }

    public int? PremioId { get; set; }

    public string? Medio { get; set; }

    public string? Observaciones { get; set; }

    public DateTime? FechaGanador { get; set; }

    public virtual Campaña? Campaña { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<EntregaPremio> EntregaPremios { get; set; } = new List<EntregaPremio>();

    public virtual Premio? Premio { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
