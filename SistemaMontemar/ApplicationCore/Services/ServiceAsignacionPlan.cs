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

        public IEnumerable<AsignacionPlan> GetAsignacionByResidencia(int idResidencia)
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.GetAsignacionByResidencia(idResidencia);
        }

        public IEnumerable<AsignacionPlan> GetAsignaciones()
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.GetAsignaciones();
        }

        public IEnumerable<AsignacionPlan> GetEstadoCuentas()
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.GetEstadoCuentas();
        }

        public AsignacionPlan Save(AsignacionPlan asignacionPlan)
        {
            IRepositoryAsignacionPlan repository = new RepositoryAsignacionPlan();
            return repository.Save(asignacionPlan);
        }
    }
}