namespace SistemaWebAver_JMedina.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;

    public partial class Observadores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Observadores()
        {
            Expediciones = new HashSet<Expediciones>();
            ExpedicionParticipantes = new HashSet<ExpedicionParticipantes>();
            Usuarios = new HashSet<Usuarios>();
        }

        [Key]
        public int ObservadorId { get; set; }

        [Required]
        [StringLength(60)]
        public string Nombres { get; set; }

        [Required]
        [StringLength(80)]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(15)]
        public string Nivel { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaIngreso { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expediciones> Expediciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpedicionParticipantes> ExpedicionParticipantes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }


        public List<Observadores> Listar()
        {
            using (var db = new AveTacnaContext())
            {
                return db.Observadores
                         .OrderBy(o => o.Apellidos).ThenBy(o => o.Nombres)
                         .ToList();
            }
        }

        // Devuelve un registro, o null si no existe
        public Observadores Obtener(int id)
        {
            using (var db = new AveTacnaContext())
            {
                return db.Observadores.SingleOrDefault(o => o.ObservadorId == id);
            }
        }



        // Busca el criterio en nombres, apellidos, correo y nivel
        public List<Observadores> Buscar(string criterio)
        {
            using (var db = new AveTacnaContext())
            {
                return db.Observadores
                         .Where(o => o.Nombres.Contains(criterio)
                                  || o.Apellidos.Contains(criterio)
                                  || o.Email.Contains(criterio)
                                  || o.Nivel.Contains(criterio))
                         .OrderBy(o => o.Apellidos).ThenBy(o => o.Nombres)
                         .ToList();
            }
        }

        // Inserta (Id = 0) o actualiza (Id > 0) este objeto
        public void Guardar()
        {
            using (var db = new AveTacnaContext())
            {
                db.Entry(this).State = ObservadorId > 0
                    ? EntityState.Modified
                    : EntityState.Added;
                db.SaveChanges();
            }
        }

        // Elimina el registro cuyo ObservadorId tiene este objeto
        public void Eliminar()
        {
            using (var db = new AveTacnaContext())
            {
                db.Entry(this).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
