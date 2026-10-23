namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        public Guid Sal { get; set; }

        [Required]
        [MaxLength(32)]
        public byte[] ClaveHash { get; set; }

        public int RolId { get; set; }

        public int? ObservadorId { get; set; }

        public bool Activo { get; set; }

        public byte IntentosFallidos { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UltimoAcceso { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        public virtual Observadores Observadores { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
