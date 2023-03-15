using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryRubro
    {
        IEnumerable<Rubro> GetRubros();
        Rubro GetRubroById(int id);

        Rubro Save(Rubro rubro);

    }
}
