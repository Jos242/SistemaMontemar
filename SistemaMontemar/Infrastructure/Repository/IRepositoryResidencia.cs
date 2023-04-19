using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryResidencia
    {
        IEnumerable<Residencia> GetResidencias();
        Residencia GetResidenciaById(int id);
        Residencia GetResidenciaByIdUsuario(int idUsuario);
        Residencia Save(Residencia residencia);
    }
}
