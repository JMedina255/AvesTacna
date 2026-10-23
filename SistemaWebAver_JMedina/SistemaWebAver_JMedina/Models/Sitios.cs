namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sitios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sitios()
        {
            EspecieSitio = new HashSet<EspecieSitio>();
            Expediciones = new HashSet<Expediciones>();
        }

        [Key]
        public int SitioId { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(30)]
        public string Provincia { get; set; }

        public int AltitudMsnm { get; set; }

        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EspecieSitio> EspecieSitio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expediciones> Expediciones { get; set; }
    }
}
