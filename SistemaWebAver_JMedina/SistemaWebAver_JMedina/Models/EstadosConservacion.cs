namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstadosConservacion")]
    public partial class EstadosConservacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstadosConservacion()
        {
            Especies = new HashSet<Especies>();
        }

        [Key]
        [StringLength(2)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }

        [StringLength(150)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Especies> Especies { get; set; }
    }
}
