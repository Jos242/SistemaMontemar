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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SistemaMontemarEntities : DbContext
    {
        public SistemaMontemarEntities()
            : base("name=SistemaMontemarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<AsignacionPlan> AsignacionPlan { get; set; }
        public virtual DbSet<Deuda> Deuda { get; set; }
        public virtual DbSet<Incidencia> Incidencia { get; set; }
        public virtual DbSet<Informacion> Informacion { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<PlanCobro> PlanCobro { get; set; }
        public virtual DbSet<Reservacion> Reservacion { get; set; }
        public virtual DbSet<Residencia> Residencia { get; set; }
        public virtual DbSet<Rubro> Rubro { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}
