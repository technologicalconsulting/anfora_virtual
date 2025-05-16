using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class CodigoVerificacion
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Codigo { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public bool? Usado { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
