namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EspecieSitio")]
    public partial class EspecieSitio
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EspecieId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SitioId { get; set; }

        [Required]
        [StringLength(12)]
        public string Abundancia { get; set; }

        [Required]
        [StringLength(12)]
        public string Estacionalidad { get; set; }

        public virtual Especies Especies { get; set; }

        public virtual Sitios Sitios { get; set; }
    }
}
