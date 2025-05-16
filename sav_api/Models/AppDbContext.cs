using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sav_api.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Campaña> Campañas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CodigoVerificacion> CodigoVerificacions { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EntregaPremio> EntregaPremios { get; set; }

    public virtual DbSet<Ganador> Ganadors { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<RolMenu> RolMenus { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TicketHistorial> TicketHistorials { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bd_av;Username=postgres;Password=Postgre_2025");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auditoria_pkey");

            entity.ToTable("auditoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accion)
                .HasMaxLength(50)
                .HasColumnName("accion");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaOperacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_operacion");
            entity.Property(e => e.RegistroId).HasColumnName("registro_id");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(50)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("auditoria_usuario_id_fkey");
        });

        modelBuilder.Entity<Campaña>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("campaña_pkey");

            entity.ToTable("campaña");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AplicaPorMonto)
                .HasDefaultValue(false)
                .HasColumnName("aplica_por_monto");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.MaxPremiosPorPersona)
                .HasDefaultValue(1)
                .HasColumnName("max_premios_por_persona");
            entity.Property(e => e.MontoMaximo)
                .HasPrecision(12, 2)
                .HasColumnName("monto_maximo");
            entity.Property(e => e.MontoMinimo)
                .HasPrecision(12, 2)
                .HasColumnName("monto_minimo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.PermiteRegistroManual)
                .HasDefaultValue(false)
                .HasColumnName("permite_registro_manual");
            entity.Property(e => e.TipoCuentaAceptada)
                .HasMaxLength(50)
                .HasColumnName("tipo_cuenta_aceptada");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(20)
                .HasColumnName("tipo_transaccion");
            entity.Property(e => e.TotalGanadores)
                .HasDefaultValue(1)
                .HasColumnName("total_ganadores");
            entity.Property(e => e.UsaSorteador)
                .HasDefaultValue(true)
                .HasColumnName("usa_sorteador");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Campañas)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("campaña_empresa_id_fkey");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cliente_pkey");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Cedula, "cliente_cedula_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<CodigoVerificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("codigo_verificacion_pkey");

            entity.ToTable("codigo_verificacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(6)
                .HasColumnName("codigo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.Usado)
                .HasDefaultValue(false)
                .HasColumnName("usado");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.CodigoVerificacions)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("codigo_verificacion_usuario_id_fkey");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("empresa_pkey");

            entity.ToTable("empresa");

            entity.HasIndex(e => e.Ruc, "empresa_ruc_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .HasColumnName("ruc");
        });

        modelBuilder.Entity<EntregaPremio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entrega_premio_pkey");

            entity.ToTable("entrega_premio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoEntrega)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pendiente'::character varying")
                .HasColumnName("estado_entrega");
            entity.Property(e => e.EvidenciaEntrega).HasColumnName("evidencia_entrega");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.GanadorId).HasColumnName("ganador_id");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.ResponsableId).HasColumnName("responsable_id");

            entity.HasOne(d => d.Ganador).WithMany(p => p.EntregaPremios)
                .HasForeignKey(d => d.GanadorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("entrega_premio_ganador_id_fkey");

            entity.HasOne(d => d.Responsable).WithMany(p => p.EntregaPremios)
                .HasForeignKey(d => d.ResponsableId)
                .HasConstraintName("entrega_premio_responsable_id_fkey");
        });

        modelBuilder.Entity<Ganador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ganador_pkey");

            entity.ToTable("ganador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampañaId).HasColumnName("campaña_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.FechaGanador)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ganador");
            entity.Property(e => e.Medio)
                .HasMaxLength(20)
                .HasColumnName("medio");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.PremioId).HasColumnName("premio_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Campaña).WithMany(p => p.Ganadors)
                .HasForeignKey(d => d.CampañaId)
                .HasConstraintName("ganador_campaña_id_fkey");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ganadors)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("ganador_cliente_id_fkey");

            entity.HasOne(d => d.Premio).WithMany(p => p.Ganadors)
                .HasForeignKey(d => d.PremioId)
                .HasConstraintName("ganador_premio_id_fkey");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Ganadors)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ganador_ticket_id_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_pkey");

            entity.ToTable("menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Ruta)
                .HasMaxLength(150)
                .HasColumnName("ruta");
        });

        modelBuilder.Entity<Premio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("premio_pkey");

            entity.ToTable("premio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampañaId).HasColumnName("campaña_id");
            entity.Property(e => e.CantidadEntregada)
                .HasDefaultValue(0)
                .HasColumnName("cantidad_entregada");
            entity.Property(e => e.CantidadTotal)
                .HasDefaultValue(1)
                .HasColumnName("cantidad_total");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.Campaña).WithMany(p => p.Premios)
                .HasForeignKey(d => d.CampañaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("premio_campaña_id_fkey");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rol_pkey");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<RolMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rol_menu_pkey");

            entity.ToTable("rol_menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            entity.HasOne(d => d.Menu).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("rol_menu_menu_id_fkey");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolMenus)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("rol_menu_rol_id_fkey");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampañaId).HasColumnName("campaña_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.TransaccionId)
                .HasMaxLength(50)
                .HasColumnName("transaccion_id");

            entity.HasOne(d => d.Campaña).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.CampañaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ticket_campaña_id_fkey");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ticket_cliente_id_fkey");
        });

        modelBuilder.Entity<TicketHistorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_historial_pkey");

            entity.ToTable("ticket_historial");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstadoAnterior)
                .HasMaxLength(20)
                .HasColumnName("estado_anterior");
            entity.Property(e => e.EstadoNuevo)
                .HasMaxLength(20)
                .HasColumnName("estado_nuevo");
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cambio");
            entity.Property(e => e.Observaciones).HasColumnName("observaciones");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.TicketHistorials)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ticket_historial_ticket_id_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClaveHash).HasColumnName("clave_hash");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("usuario_empresa_id_fkey");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("usuario_rol_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
