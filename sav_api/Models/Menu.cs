using System;
using System.Collections.Generic;

namespace sav_api.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Ruta { get; set; }

    public string? Icono { get; set; }

    public virtual ICollection<RolMenu> RolMenus { get; set; } = new List<RolMenu>();
}
