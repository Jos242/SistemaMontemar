using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDeuda : IServiceDeuda
    {
        public IEnumerable<Deuda> GetDeudaByResidencia(int id)
        {
            IRepositoryDeuda repository = new RepositoryDeuda();
            return repository.GetDeudaByResidencia(id);
        }

        public IEnumerable<Deuda> GetDeudas()
        {
            IRepositoryDeuda repository = new RepositoryDeuda();
            return repository.GetDeudas();
        }

        public Deuda Save(Deuda deuda)
        {
            IRepositoryDeuda repository = new RepositoryDeuda();
            return repository.Save(deuda);
        }
    }
}
