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

    [MetadataType(typeof(PagoMetaData))]

    public partial class Pago
    {
        public int Id { get; set; }
        public int IdAsignacion { get; set; }
        public System.DateTime FechaPago { get; set; }
        public decimal Total { get; set; }
    
        public virtual AsignacionPlan AsignacionPlan { get; set; }
    }
}
