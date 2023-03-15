using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRubro
    {
        IEnumerable<Rubro> GetRubros();
        Rubro GetRubroById(int id);
        Rubro Save(Rubro rubro);
    }
}
