namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Familias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Familias()
        {
            Especies = new HashSet<Especies>();
        }

        [Key]
        public int FamiliaId { get; set; }

        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        public int OrdenId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Especies> Especies { get; set; }

        public virtual Ordenes Ordenes { get; set; }
    }
}
