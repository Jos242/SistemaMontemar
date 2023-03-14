using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryIncidencia
    {
        IEnumerable<Incidencia> GetIncidencias();
        Incidencia GetIncidenciaById(int id);
        Incidencia Save(Incidencia incidencia);
    }
}
