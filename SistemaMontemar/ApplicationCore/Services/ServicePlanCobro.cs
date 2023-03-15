using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePlanCobro : IServicePlanCobro
    {
        public PlanCobro GetPlanCobroById(int id)
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.GetPlanCobroById(id);
        }

        public IEnumerable<PlanCobro> GetPlanCobros()
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.GetPlanCobros();
        }

        public PlanCobro Save(PlanCobro planCobro, string[] selectedRubros)
        {
            IRepositoryPlanCobro repository = new RepositoryPlanCobro();
            return repository.Save(planCobro, selectedRubros);
        }
    }
}
