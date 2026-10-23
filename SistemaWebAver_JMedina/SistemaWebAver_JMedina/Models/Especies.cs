namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Especies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especies()
        {
            Avistamientos = new HashSet<Avistamientos>();
            EspecieSitio = new HashSet<EspecieSitio>();
        }

        [Key]
        public int EspecieId { get; set; }

        [Required]
        [StringLength(80)]
        public string NombreComun { get; set; }

        [Required]
        [StringLength(80)]
        public string NombreCientifico { get; set; }

        public int FamiliaId { get; set; }

        [Required]
        [StringLength(2)]
        public string EstadoCodigo { get; set; }

        public short? LongitudCm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avistamientos> Avistamientos { get; set; }

        public virtual EstadosConservacion EstadosConservacion { get; set; }

        public virtual Familias Familias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EspecieSitio> EspecieSitio { get; set; }
    }
}
