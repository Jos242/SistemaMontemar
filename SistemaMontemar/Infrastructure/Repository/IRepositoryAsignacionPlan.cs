using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryAsignacionPlan
    {
        IEnumerable<AsignacionPlan> GetEstadoCuentas();
        AsignacionPlan GetAsignacionById(int id);
        AsignacionPlan Save(AsignacionPlan asignacionPlan);
        IEnumerable<AsignacionPlan> GetAsignacionByResidencia(int idResidencia);
    }
}