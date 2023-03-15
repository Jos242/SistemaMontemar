using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryPlanCobro
    {
        IEnumerable<PlanCobro> GetPlanCobros();
        PlanCobro GetPlanCobroById(int id);
        PlanCobro Save(PlanCobro planCobro, string[] selectedRubros);

    }
}
