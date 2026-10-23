namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Avistamientos
    {
        [Key]
        public int AvistamientoId { get; set; }

        public int ExpedicionId { get; set; }

        public int EspecieId { get; set; }

        public int ObservadorId { get; set; }

        public int Cantidad { get; set; }

        public TimeSpan Hora { get; set; }

        [Required]
        [StringLength(20)]
        public string Comportamiento { get; set; }

        public bool Verificado { get; set; }

        [StringLength(250)]
        public string Observaciones { get; set; }

        public virtual Especies Especies { get; set; }

        public virtual ExpedicionParticipantes ExpedicionParticipantes { get; set; }
    }
}
