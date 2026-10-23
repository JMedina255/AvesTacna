namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Expediciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Expediciones()
        {
            ExpedicionParticipantes = new HashSet<ExpedicionParticipantes>();
        }

        [Key]
        public int ExpedicionId { get; set; }

        public int SitioId { get; set; }

        public int GuiaId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public decimal DuracionHoras { get; set; }

        [Required]
        [StringLength(20)]
        public string Clima { get; set; }

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }

        public virtual Observadores Observadores { get; set; }

        public virtual Sitios Sitios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpedicionParticipantes> ExpedicionParticipantes { get; set; }
    }
}
