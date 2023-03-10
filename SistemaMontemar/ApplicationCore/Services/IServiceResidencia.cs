using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IServiceResidencia
    {
        IEnumerable<Residencia> GetResidencias();
        Residencia GetResidenciaById(int id);
        Residencia Save(Residencia residencia);
    }
}
