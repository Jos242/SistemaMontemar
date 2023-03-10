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

    [MetadataType(typeof(IncidenciaMetdaData))]
    public partial class Incidencia
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Estado { get; set; }
        public Dictionary<int, string> Estados
        {
            get
            {
                return new Dictionary<int, string> { { 0, "Pending" }, { 1, "Completed" } };
            }
            set { }
        }


        public virtual Usuario Usuario { get; set; }
    }
}
