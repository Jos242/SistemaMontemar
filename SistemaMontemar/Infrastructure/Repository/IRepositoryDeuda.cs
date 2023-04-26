using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryDeuda
    {
        IEnumerable<Deuda> GetDeudas();
        Deuda GetDeudaById(int id);
        IEnumerable<Deuda> GetDeudaByResidencia(int id);
        Deuda GetDeudaByAsignacionPlan(int id);
        Deuda Save(Deuda deuda);
    }
}
