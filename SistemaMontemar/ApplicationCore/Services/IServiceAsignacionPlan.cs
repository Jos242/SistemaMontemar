using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceAsignacionPlan
    {
        IEnumerable<AsignacionPlan> GetEstadoCuentas();
        IEnumerable<AsignacionPlan> GetAsignaciones();
        AsignacionPlan GetAsignacionById(int id);
        AsignacionPlan Save(AsignacionPlan asignacionPlan);
        IEnumerable<AsignacionPlan> GetAsignacionByResidencia(int idResidencia);
    }
}
