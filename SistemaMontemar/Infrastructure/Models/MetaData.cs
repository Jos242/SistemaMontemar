using System;
using System.Collections.Generic;
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
}
