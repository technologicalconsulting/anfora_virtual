using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Auditorium
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string? Accion { get; set; }

    public string? TablaAfectada { get; set; }

    public int? RegistroId { get; set; }

    public DateTime? FechaOperacion { get; set; }

    public string? Descripcion { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
