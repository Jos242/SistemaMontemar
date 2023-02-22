﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public partial class ResidenciaMetaData
    {
        [Display(Name = "House N°")]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        [Display(Name = "Inhabitants")]
        public int CantPersonas { get; set; }

        [Display(Name = "Cars")]
        public int CantCarros { get; set; }

        [Display(Name = "Date Bought")]
        public System.DateTime FechaAlquiler { get; set; }

        [Display(Name = "House Size")]
        public int Tipo { get; set; }

        public Dictionary<int, string> Tipos
        {
            get
            {
                return new Dictionary<int, string> { { 0, "Small" }, { 1, "Medium" }, { 2, "Large" } };
            }
            set { }
        }

        public byte[] Imagen { get; set; }

        [Display(Name = "Status")]
        public int Estado { get; set; }

        public Dictionary<int, string> Estados
        {
            get
            {
                return new Dictionary<int, string> { { 0, "In Construction" }, { 1, "Inhabited" } };
            }
            set { }
        }


        public virtual ICollection<Incidentes> Incidentes { get; set; }


        [Display(Name = "Owner")]
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

    public partial class RubroMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        public string Descripcion { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Cobro { get; set; }
        public virtual ICollection<PlanCobro> PlanCobro { get; set; }

    }

    public partial class AsignacionPlanMetaData
    {
        public int Id { get; set; }

        [Display(Name = "House N°")]
        public int IdResidencia { get; set; }

        public int IdPlan { get; set; }

        public int Mes { get; set; }

        public int Estado { get; set; }

        public virtual PlanCobro PlanCobro { get; set; }

        public virtual Residencia Residencia { get; set; }

        public virtual ICollection<Deuda> Deuda { get; set; }

        public virtual ICollection<Pago> Pago { get; set; }
    }

    public partial class DeudaMetaData
    {
        public int Id { get; set; }
        public int IdAsignacion { get; set; }

        [Display(Name = "Debt Date")]
        public System.DateTime FechaPago { get; set; }
        public string Fecha
        {
            get
            {
                return FechaPago.ToString("dd/MM/yyyy");
            }
            set { }
        }

        [Display(Name = "Status")]
        public int Estado { get; set; }
        public Dictionary<int, string> Estados
        {
            get
            {
                return new Dictionary<int, string> { { 0, "Not payed" }, { 1, "Payed" } };
            }
            set { }
        }

        public virtual AsignacionPlan AsignacionPlan { get; set; }
    }

    public partial class PagoMetaData
    {
        public int Id { get; set; }
        public int IdAsignacion { get; set; }

        [Display(Name = "Payment Date")]
        public System.DateTime FechaPago { get; set; }

        [Display(Name = "Status")]
        public string Fecha
        {
            get
            {
                return FechaPago.ToString("dd/MM/yyyy");
            }
            set { }
        }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        public virtual AsignacionPlan AsignacionPlan { get; set; }
    }
}
