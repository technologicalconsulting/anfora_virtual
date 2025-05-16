using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Premio
{
    public int Id { get; set; }

    public int? CampañaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int CantidadTotal { get; set; }

    public int? CantidadEntregada { get; set; }

    public int? Orden { get; set; }

    public virtual Campaña? Campaña { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();
}
