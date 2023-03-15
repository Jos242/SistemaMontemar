using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryInformacion
    {
        IEnumerable<Informacion> GetInformacions();
        Informacion GetInformacionById(int id);
        Informacion Save(Informacion residencia);
    }
}
