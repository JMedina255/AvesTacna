using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SistemaWebAver_JMedina.Models
{
    public partial class AveTacnaContext : DbContext
    {
        public AveTacnaContext()
            : base("name=AveTacnaContext")
        {
        }

        public virtual DbSet<AccesosLog> AccesosLog { get; set; }
        public virtual DbSet<Avistamientos> Avistamientos { get; set; }
        public virtual DbSet<Especies> Especies { get; set; }
        public virtual DbSet<EspecieSitio> EspecieSitio { get; set; }
        public virtual DbSet<EstadosConservacion> EstadosConservacion { get; set; }
        public virtual DbSet<Expediciones> Expediciones { get; set; }
        public virtual DbSet<ExpedicionParticipantes> ExpedicionParticipantes { get; set; }
        public virtual DbSet<Familias> Familias { get; set; }
        public virtual DbSet<Observadores> Observadores { get; set; }
        public virtual DbSet<Ordenes> Ordenes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sitios> Sitios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccesosLog>()
                .Property(e => e.Fecha)
                .HasPrecision(0);

            modelBuilder.Entity<Avistamientos>()
                .Property(e => e.Hora)
                .HasPrecision(0);

            modelBuilder.Entity<Especies>()
                .Property(e => e.EstadoCodigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Especies>()
                .HasMany(e => e.Avistamientos)
                .WithRequired(e => e.Especies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Especies>()
                .HasMany(e => e.EspecieSitio)
                .WithRequired(e => e.Especies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EstadosConservacion>()
                .Property(e => e.Codigo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EstadosConservacion>()
                .HasMany(e => e.Especies)
                .WithRequired(e => e.EstadosConservacion)
                .HasForeignKey(e => e.EstadoCodigo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expediciones>()
                .Property(e => e.HoraInicio)
                .HasPrecision(0);

            modelBuilder.Entity<Expediciones>()
                .Property(e => e.DuracionHoras)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Expediciones>()
                .HasMany(e => e.ExpedicionParticipantes)
                .WithRequired(e => e.Expediciones)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExpedicionParticipantes>()
                .HasMany(e => e.Avistamientos)
                .WithRequired(e => e.ExpedicionParticipantes)
                .HasForeignKey(e => new { e.ExpedicionId, e.ObservadorId })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Familias>()
                .HasMany(e => e.Especies)
                .WithRequired(e => e.Familias)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Observadores>()
                .HasMany(e => e.Expediciones)
                .WithRequired(e => e.Observadores)
                .HasForeignKey(e => e.GuiaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Observadores>()
                .HasMany(e => e.ExpedicionParticipantes)
                .WithRequired(e => e.Observadores)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ordenes>()
                .HasMany(e => e.Familias)
                .WithRequired(e => e.Ordenes)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Roles)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sitios>()
                .Property(e => e.Latitud)
                .HasPrecision(9, 5);

            modelBuilder.Entity<Sitios>()
                .Property(e => e.Longitud)
                .HasPrecision(9, 5);

            modelBuilder.Entity<Sitios>()
                .HasMany(e => e.EspecieSitio)
                .WithRequired(e => e.Sitios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sitios>()
                .HasMany(e => e.Expediciones)
                .WithRequired(e => e.Sitios)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.UltimoAcceso)
                .HasPrecision(0);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.FechaCreacion)
                .HasPrecision(0);
        }
    }
}
