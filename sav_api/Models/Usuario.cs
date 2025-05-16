using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ClaveHash { get; set; } = null!;

    public int? EmpresaId { get; set; }

    public int? RolId { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<CodigoVerificacion> CodigoVerificacions { get; set; } = new List<CodigoVerificacion>();

    public virtual Empresa? Empresa { get; set; }

    public virtual ICollection<EntregaPremio> EntregaPremios { get; set; } = new List<EntregaPremio>();

    public virtual Rol? Rol { get; set; }
}
