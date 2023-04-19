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
        [Display(Name = "House N°")]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        [Display(Name = "Inhabitants")]
        public int CantPersonas { get; set; }

        [Display(Name = "Cars")]
        public int CantCarros { get; set; }

        [Display(Name = "Date Bought")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
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



        [Display(Name = "Owner")]
        public virtual Usuario Usuario { get; set; }
    }

    public partial class PlanCobroMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        public string Descripcion { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]

        [RegularExpression(@"^[0-9]+(,[0-9]{1,2})?$", ErrorMessage = "Numbers only, with 2 decimals")]

        [Display(Name = "Total Amount")]
        [Required(ErrorMessage = "{0} is required")]
        public decimal Cobro { get; set; }


        public virtual ICollection<AsignacionPlan> AsignacionPlan { get; set; }

        [Display(Name = "Payments")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual ICollection<Rubro> Rubro { get; set; }
    }

    public partial class RubroMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        public string Descripcion { get; set; }

        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression(@"^[0-9]+(,[0-9]{1,2})?$", ErrorMessage = "Numbers only, with 2 decimals")]
        public decimal Cobro { get; set; }
        public virtual ICollection<PlanCobro> PlanCobro { get; set; }

    }

    public partial class AsignacionPlanMetaData
    {
        public int Id { get; set; }

        [Display(Name = "House N°")]
        public int IdResidencia { get; set; }

        public int IdPlan { get; set; }

        [Display(Name = "Date")]
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime FechaPago { get; set; }

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime FechaPago { get; set; }

        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }

        public virtual AsignacionPlan AsignacionPlan { get; set; }
    }

    public partial class IncidenciaMetaData
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "{0} is required")]
        public string Mensaje { get; set; }

        [Display(Name = "Incident Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime Fecha { get; set; }

        [Display(Name = "Status")]
        public int Estado { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public partial class UsuarioMetaData
    {
        public int Id { get; set; }
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Name")]
        public string Nombre { get; set; }

        [Display(Name = "Last Name")]
        public string Apellido01 { get; set; }
        public string Apellido02 { get; set; }

        [Display(Name = "Phone Number")]
        public string Telefono { get; set; }
        public string Password { get; set; }

        [Display(Name = "Pay Day")]
        public int DiaPagar { get; set; }

        [Display(Name = "Status")]
        public int Estado { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return Nombre + " " + Apellido01;
            }
            set { }
        }

        public virtual ICollection<Incidencia> Incidencia { get; set; }
        public virtual ICollection<Reservacion> Reservacion { get; set; }
        public virtual ICollection<Residencia> Residencia { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
    }

    public partial class InformacionMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} is required")]
        public string Titulo { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is required")]
        public string Descripcion { get; set; }

        [Display(Name = "Type")]
        public int Tipo { get; set; }
        public Dictionary<int, string> Tipos
        {
            get
            {
                return new Dictionary<int, string> { { 0, "Notices" }, { 1, "Announcements" }, { 2, "Rules" }, { 3, "Proceedings " }, { 4, "Financial State" } };
            }
            set { }
        }
    }

    public partial class ReservacionMetaData 
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        [Display(Name = "Area Requested")]
        public int IdArea { get; set; }

        [Display(Name = "Start Date & Time")]
        public System.DateTime FechaInicio { get; set; }

        [Display(Name = "End Time")]
        public System.DateTime FechaFinal { get; set; }

        [Display(Name = "Status")]
        public int Estado { get; set; }

        public virtual Area Area { get; set; }
        public virtual Usuario Usuario { get; set; }

    }


}
