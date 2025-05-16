using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Empresa
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ruc { get; set; } = null!;

    public string? Logo { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Campaña> Campañas { get; set; } = new List<Campaña>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
