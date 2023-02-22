using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAsignacionPlan : IServiceAsignacionPlan
    {
        public AsignacionPlan GetAsignacionById(int id)
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.GetAsignacionById(id);
        }

        public IEnumerable<AsignacionPlan> GetEstadoCuentas()
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.GetEstadoCuentas();
        }
    }
}