﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoCRM.Models
{
    public partial class CRMContext : DbContext
    {
        public CRMContext()
        {
        }

        public CRMContext(DbContextOptions<CRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividads { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Contacto> Contactos { get; set; } = null!;
        public virtual DbSet<CotizacionDenegadum> CotizacionDenegada { get; set; } = null!;
        public virtual DbSet<Cotizacione> Cotizaciones { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Ejecucion> Ejecucions { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Etapa> Etapas { get; set; } = null!;
        public virtual DbSet<FamiliaProducto> FamiliaProductos { get; set; } = null!;
        public virtual DbSet<Inflacion> Inflacions { get; set; } = null!;
        public virtual DbSet<Monedum> Moneda { get; set; } = null!;
        public virtual DbSet<Probabilidad> Probabilidads { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductosXcotizacion> ProductosXcotizacions { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<TipoContacto> TipoContactos { get; set; } = null!;
        public virtual DbSet<TipoCotizacion> TipoCotizacions { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<ZonaSector> ZonaSectors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
       //To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        optionsBuilder.UseSqlServer("Server=localhost ;Database=CRM;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.ToTable("actividad");

                entity.HasIndex(e => e.Id, "UQ__activida__3213E83E01B3847F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fechaInicio");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.NombreCuenta)
                    .HasName("PK__cliente__7E5CF2B8461E42E6");

                entity.ToTable("cliente");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cuenta");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Celular)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("celular");

                entity.Property(e => e.ContactoPrincipal)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contacto_principal");

                entity.Property(e => e.Correo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Idmoneda).HasColumnName("IDmoneda");

                entity.Property(e => e.Idzona).HasColumnName("IDzona");

                entity.Property(e => e.Sitio)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("sitio");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__asesor__4222D4EF");

                entity.HasOne(d => d.IdmonedaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idmoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__IDmoned__440B1D61");

                entity.HasOne(d => d.IdzonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Idzona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cliente__IDzona__4316F928");
            });

            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__contacto__72AFBCC721FA42E2");

                entity.ToTable("contacto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cliente");

                entity.Property(e => e.Correo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Motivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("motivo");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoContacto).HasColumnName("tipoContacto");

                entity.Property(e => e.Zona).HasColumnName("zona");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__asesor__4E88ABD4");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__client__4CA06362");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__estado__5070F446");

                entity.HasOne(d => d.TipoContactoNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.TipoContacto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__tipoCo__4F7CD00D");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Contactos)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__contacto__zona__4D94879B");

                entity.HasMany(d => d.Actividads)
                    .WithMany(p => p.Contactos)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadesXcontacto",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("Actividad").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__5441852A"),
                        r => r.HasOne<Contacto>().WithMany().HasForeignKey("Contacto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__conta__534D60F1"),
                        j =>
                        {
                            j.HasKey("Contacto", "Actividad").HasName("PK__activida__387C62636650F438");

                            j.ToTable("actividadesXcontacto");

                            j.IndexerProperty<string>("Contacto").HasMaxLength(20).IsUnicode(false).HasColumnName("contacto");

                            j.IndexerProperty<short>("Actividad").HasColumnName("actividad");
                        });

                entity.HasMany(d => d.Tareas)
                    .WithMany(p => p.Contactos)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXcontacto",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("Tarea").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcon__tarea__5812160E"),
                        r => r.HasOne<Contacto>().WithMany().HasForeignKey("Contacto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcon__conta__571DF1D5"),
                        j =>
                        {
                            j.HasKey("Contacto", "Tarea").HasName("PK__tareaXco__80667CCD3E30544F");

                            j.ToTable("tareaXcontacto");

                            j.IndexerProperty<string>("Contacto").HasMaxLength(20).IsUnicode(false).HasColumnName("contacto");

                            j.IndexerProperty<short>("Tarea").HasColumnName("tarea");
                        });
            });

            modelBuilder.Entity<CotizacionDenegadum>(entity =>
            {
                entity.HasKey(e => e.NumeroCotizacion)
                    .HasName("PK__cotizaci__9FB24E0EE450167A");

                entity.ToTable("cotizacionDenegada");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Razon)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("razon");

                entity.HasOne(d => d.NumeroCotizacionNavigation)
                    .WithOne(p => p.CotizacionDenegadum)
                    .HasForeignKey<CotizacionDenegadum>(d => d.NumeroCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__numer__70DDC3D8");
            });

            modelBuilder.Entity<Cotizacione>(entity =>
            {
                entity.HasKey(e => e.NumeroCotizacion)
                    .HasName("PK__cotizaci__9FB24E0EC6F53E0A");

                entity.ToTable("cotizaciones");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.ContactoAsociado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("contacto_asociado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Etapa).HasColumnName("etapa");

                entity.Property(e => e.FechaCierra)
                    .HasColumnType("date")
                    .HasColumnName("fecha_cierra");

                entity.Property(e => e.FechaCotizacion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_cotizacion");

                entity.Property(e => e.Inflacion).HasColumnName("inflacion");

                entity.Property(e => e.Moneda).HasColumnName("moneda");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cuenta");

                entity.Property(e => e.NombreOportunidad)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre_oportunidad");

                entity.Property(e => e.OrdenDeCompra)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("orden_de_compra");

                entity.Property(e => e.Probabilidad).HasColumnName("probabilidad");

                entity.Property(e => e.Tipo).HasColumnName("tipo");

                entity.Property(e => e.Zona).HasColumnName("zona");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__aseso__693CA210");

                entity.HasOne(d => d.ContactoAsociadoNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.ContactoAsociado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__conta__68487DD7");

                entity.HasOne(d => d.EtapaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Etapa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__etapa__6B24EA82");

                entity.HasOne(d => d.InflacionNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Inflacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__infla__6E01572D");

                entity.HasOne(d => d.MonedaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Moneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__moned__6754599E");

                entity.HasOne(d => d.NombreCuentaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.NombreCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__nombr__6A30C649");

                entity.HasOne(d => d.ProbabilidadNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Probabilidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacio__proba__6C190EBB");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacion__tipo__6D0D32F4");

                entity.HasOne(d => d.ZonaNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Zona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cotizacion__zona__66603565");

                entity.HasMany(d => d.ActividadCotizacions)
                    .WithMany(p => p.NumeroCotizacions)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadXcotizacion",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("ActividadCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__7C4F7684"),
                        r => r.HasOne<Cotizacione>().WithMany().HasForeignKey("NumeroCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__numer__7B5B524B"),
                        j =>
                        {
                            j.HasKey("NumeroCotizacion", "ActividadCotizacion").HasName("PK__activida__E60993CCA65B9165");

                            j.ToTable("actividadXcotizacion");

                            j.IndexerProperty<string>("NumeroCotizacion").HasMaxLength(10).IsUnicode(false).HasColumnName("numero_cotizacion");

                            j.IndexerProperty<short>("ActividadCotizacion").HasColumnName("actividad_cotizacion");
                        });

                entity.HasMany(d => d.TareaCotizacions)
                    .WithMany(p => p.NumeroCotizacions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXcotizacion",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("TareaCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcot__tarea__787EE5A0"),
                        r => r.HasOne<Cotizacione>().WithMany().HasForeignKey("NumeroCotizacion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXcot__numer__778AC167"),
                        j =>
                        {
                            j.HasKey("NumeroCotizacion", "TareaCotizacion").HasName("PK__tareaXco__78BCABAD6F8B0B03");

                            j.ToTable("tareaXcotizacion");

                            j.IndexerProperty<string>("NumeroCotizacion").HasMaxLength(10).IsUnicode(false).HasColumnName("numero_cotizacion");

                            j.IndexerProperty<short>("TareaCotizacion").HasColumnName("tarea_cotizacion");
                        });
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("departamento");

                entity.HasIndex(e => e.Id, "UQ__departam__3213E83EF7E1436A")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ejecucion>(entity =>
            {
                entity.HasKey(e => e.Idejecucion)
                    .HasName("PK__ejecucio__7F8C9D192BED05B7");

                entity.ToTable("ejecucion");

                entity.HasIndex(e => e.Idejecucion, "UQ__ejecucio__7F8C9D1878C82DF7")
                    .IsUnique();

                entity.Property(e => e.Idejecucion)
                    .ValueGeneratedNever()
                    .HasColumnName("IDejecucion");

                entity.Property(e => e.Asesor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("asesor");

                entity.Property(e => e.Departamento).HasColumnName("departamento");

                entity.Property(e => e.FechaCierra)
                    .HasColumnType("date")
                    .HasColumnName("fecha_cierra");

                entity.Property(e => e.FechaEjecucion)
                    .HasColumnType("date")
                    .HasColumnName("fecha_ejecucion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreCuenta)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre_cuenta");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Propietario)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("propietario");

                entity.HasOne(d => d.AsesorNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.Asesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__aseso__01142BA1");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__depar__02FC7413");

                entity.HasOne(d => d.NombreCuentaNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.NombreCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__nombr__02084FDA");

                entity.HasOne(d => d.NumeroCotizacionNavigation)
                    .WithMany(p => p.Ejecucions)
                    .HasForeignKey(d => d.NumeroCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ejecucion__numer__00200768");

                entity.HasMany(d => d.Actividads)
                    .WithMany(p => p.Ejecucions)
                    .UsingEntity<Dictionary<string, object>>(
                        "ActividadesXejecucion",
                        l => l.HasOne<Actividad>().WithMany().HasForeignKey("Actividad").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__activ__06CD04F7"),
                        r => r.HasOne<Ejecucion>().WithMany().HasForeignKey("Ejecucion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__actividad__ejecu__05D8E0BE"),
                        j =>
                        {
                            j.HasKey("Ejecucion", "Actividad").HasName("PK__activida__410CFF918AE7B8C7");

                            j.ToTable("actividadesXejecucion");

                            j.IndexerProperty<short>("Ejecucion").HasColumnName("ejecucion");

                            j.IndexerProperty<short>("Actividad").HasColumnName("actividad");
                        });

                entity.HasMany(d => d.Tareas)
                    .WithMany(p => p.Ejecucions)
                    .UsingEntity<Dictionary<string, object>>(
                        "TareaXejecucion",
                        l => l.HasOne<Tarea>().WithMany().HasForeignKey("Tarea").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXeje__tarea__0A9D95DB"),
                        r => r.HasOne<Ejecucion>().WithMany().HasForeignKey("Ejecucion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tareaXeje__ejecu__09A971A2"),
                        j =>
                        {
                            j.HasKey("Ejecucion", "Tarea").HasName("PK__tareaXej__F916E13F6EAA811B");

                            j.ToTable("tareaXejecucion");

                            j.IndexerProperty<short>("Ejecucion").HasColumnName("ejecucion");

                            j.IndexerProperty<short>("Tarea").HasColumnName("tarea");
                        });
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado");

                entity.HasIndex(e => e.Id, "UQ__estado__3213E83E31BECF6B")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Etapa>(entity =>
            {
                entity.ToTable("etapa");

                entity.HasIndex(e => e.Id, "UQ__etapa__3213E83E3D19CF77")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Etapa1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("etapa");
            });

            modelBuilder.Entity<FamiliaProducto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__familia___40F9A207E4A4C9B9");

                entity.ToTable("familia_producto");

                entity.HasIndex(e => e.Codigo, "UQ__familia___40F9A206A1829495")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Inflacion>(entity =>
            {
                entity.ToTable("inflacion");

                entity.HasIndex(e => e.Id, "UQ__inflacio__3213E83E90302583")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            });

            modelBuilder.Entity<Monedum>(entity =>
            {
                entity.ToTable("moneda");

                entity.HasIndex(e => e.Id, "UQ__moneda__3213E83E99E83AAC")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.NombreMoneda)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Probabilidad>(entity =>
            {
                entity.ToTable("probabilidad");

                entity.HasIndex(e => e.Id, "UQ__probabil__3213E83E76DCCAD5")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Etapa).HasColumnName("etapa");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__producto__40F9A20771CE06AF");

                entity.ToTable("producto");

                entity.HasIndex(e => e.Codigo, "UQ__producto__40F9A2060F774476")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.CodigoFamilia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo_familia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.ObjCodigoFamilia)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CodigoFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__producto__codigo__34C8D9D1");
            });

            modelBuilder.Entity<ProductosXcotizacion>(entity =>
            {
                entity.HasKey(e => new { e.CodigoProducto, e.NumeroCotizacion })
                    .HasName("PK__producto__E9AA2349408AFA0A");

                entity.ToTable("productosXcotizacion");

                entity.Property(e => e.CodigoProducto)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigo_producto");

                entity.Property(e => e.NumeroCotizacion)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("numero_cotizacion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.PrecioNegociado)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("precioNegociado");

                entity.HasOne(d => d.CodigoProductoNavigation)
                    .WithMany(p => p.ProductosXcotizacions)
                    .HasForeignKey(d => d.CodigoProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__productos__codig__73BA3083");

                entity.HasOne(d => d.NumeroCotizacionNavigation)
                    .WithMany(p => p.ProductosXcotizacions)
                    .HasForeignKey(d => d.NumeroCotizacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__productos__numer__74AE54BC");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.HasIndex(e => e.Id, "UQ__rol__3213E83E9FDD21C6")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TipoRol)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("tipoRol");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("tarea");

                entity.HasIndex(e => e.Id, "UQ__tarea__3213E83E52B73E49")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaFinalizacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaFinalizacion");

                entity.Property(e => e.Informacion)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("informacion");
            });

            modelBuilder.Entity<TipoContacto>(entity =>
            {
                entity.ToTable("tipoContacto");

                entity.HasIndex(e => e.Id, "UQ__tipoCont__3213E83EC50E218F")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<TipoCotizacion>(entity =>
            {
                entity.ToTable("tipoCotizacion");

                entity.HasIndex(e => e.Id, "UQ__tipoCoti__3213E83ED9A2D8E6")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__usuario__415B7BE43468710F");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Cedula, "UQ__usuario__415B7BE5AFBE95BF")
                    .IsUnique();

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido1");

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("apellido2");

                entity.Property(e => e.Clave)
                    .HasMaxLength(500)
                    .HasColumnName("clave");

                entity.Property(e => e.Departamento).HasColumnName("departamento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Rol).HasColumnName("rol");

                entity.HasOne(d => d.ObjDepartamento)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuario__departa__3C69FB99");

                entity.HasOne(d => d.ObjRol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Rol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__usuario__rol__3B75D760");
            });

            modelBuilder.Entity<ZonaSector>(entity =>
            {
                entity.ToTable("zonaSector");

                entity.HasIndex(e => e.Id, "UQ__zonaSect__3213E83ED640A8DB")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Sector)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sector");

                entity.Property(e => e.Zona)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("zona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
