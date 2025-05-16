using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Campaña
{
    public int Id { get; set; }

    public int? EmpresaId { get; set; }

    public string Nombre { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? TipoTransaccion { get; set; }

    public bool? UsaSorteador { get; set; }

    public bool? PermiteRegistroManual { get; set; }

    public int? TotalGanadores { get; set; }

    public int? MaxPremiosPorPersona { get; set; }

    public string? TipoCuentaAceptada { get; set; }

    public decimal? MontoMinimo { get; set; }

    public decimal? MontoMaximo { get; set; }

    public bool? AplicaPorMonto { get; set; }

    public virtual Empresa? Empresa { get; set; }

    public virtual ICollection<Ganador> Ganadors { get; set; } = new List<Ganador>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
