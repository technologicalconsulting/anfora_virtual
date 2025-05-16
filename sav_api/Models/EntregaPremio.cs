using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class EntregaPremio
{
    public int Id { get; set; }

    public int? GanadorId { get; set; }

    public string? EstadoEntrega { get; set; }

    public DateTime? FechaAsignacion { get; set; }

    public DateTime? FechaEntrega { get; set; }

    public int? ResponsableId { get; set; }

    public string? EvidenciaEntrega { get; set; }

    public string? Observaciones { get; set; }

    public virtual Ganador? Ganador { get; set; }

    public virtual Usuario? Responsable { get; set; }
}
