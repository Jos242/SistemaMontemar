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
    
    public partial class Reservacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdArea { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFinal { get; set; }
        public int Estado { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
