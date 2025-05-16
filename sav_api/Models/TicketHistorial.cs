using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class TicketHistorial
{
    public int Id { get; set; }

    public int? TicketId { get; set; }

    public string? EstadoAnterior { get; set; }

    public string? EstadoNuevo { get; set; }

    public DateTime? FechaCambio { get; set; }

    public string? Observaciones { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
