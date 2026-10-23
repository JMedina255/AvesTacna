namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccesosLog")]
    public partial class AccesosLog
    {
        [Key]
        public long AccesoId { get; set; }

        [Required]
        [StringLength(30)]
        public string NombreUsuario { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Fecha { get; set; }

        public bool Exitoso { get; set; }

        [StringLength(40)]
        public string Motivo { get; set; }
    }
}
