using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public partial class ResidenciaMetaData
    {
        public int Id { get; set; }
        public Nullable<int> IdUsuario { get; set; }
        public Nullable<int> CantPersonas { get; set; }
        public Nullable<int> CantCarros { get; set; }
        public System.DateTime FechaAlquiler { get; set; }
        public int Cuartos { get; set; }
        public int Banios { get; set; }
        public int Pisos { get; set; }
        public Nullable<int> Estado { get; set; }


        public virtual ICollection<Incidentes> Incidentes { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public partial class PlanCobroMetaData 
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string Descripcion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]

        [Display(Name = "Total Amount")]
        public decimal Cobro { get; set; }


        public virtual ICollection<AsignacionPlan> AsignacionPlan { get; set; }

        [Display(Name = "Payments")]
        public virtual ICollection<Rubro> Rubro { get; set; }
    }

    public partial class RubroMetaData {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string Descripcion { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Cobro { get; set; }
        public virtual ICollection<PlanCobro> PlanCobro { get; set; }

    }
}
