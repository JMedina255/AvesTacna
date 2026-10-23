using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SistemaWebAver_JMedina.Models
{
    [MetadataType(typeof(SitioMetadata))]
    public partial class Sitio
    {
    }
    public class SitioMetadata
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(80, ErrorMessage = "Máximo 80 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Seleccione el tipo de sitio.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Seleccione la provincia.")]
        public string Provincia { get; set; }

        [Display(Name = "Altitud (msnm)")]
        [Required(ErrorMessage = "La altitud es obligatoria.")]
        [Range(0, 6800, ErrorMessage = "La altitud debe estar entre 0 y 6800 msnm.")]
        public int AltitudMsnm { get; set; }

        [Required(ErrorMessage = "La latitud es obligatoria.")]
        [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "La longitud es obligatoria.")]
        [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
        public decimal Longitud { get; set; }
    }
}

