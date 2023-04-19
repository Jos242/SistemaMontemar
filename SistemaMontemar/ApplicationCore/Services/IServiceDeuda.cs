using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceDeuda
    {
        IEnumerable<Deuda> GetDeudas();
        IEnumerable<Deuda> GetDeudaByResidencia(int id);
        Deuda Save(Deuda deuda);
    }
}
