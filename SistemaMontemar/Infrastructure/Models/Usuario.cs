//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(UsuarioMetaData))]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Incidencia = new HashSet<Incidencia>();
            this.Reservacion = new HashSet<Reservacion>();
            this.Residencia = new HashSet<Residencia>();
        }

        public int Id { get; set; }
        public int IdTipoUsuario { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido01 { get; set; }
        public string Apellido02 { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public int DiaPagar { get; set; }
        public int Estado { get; set; }

        public string FullName
        {
            get
            {
                return Nombre + " " + Apellido01;
            }
            set { }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Incidencia> Incidencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservacion> Reservacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Residencia> Residencia { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }
}
