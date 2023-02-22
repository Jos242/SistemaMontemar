using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceDeuda
    {
        public Deuda GetDeudaById(int id)
        {
            IRepositoryDeuda repository = new RepositoryDeuda();
            return repository.GetDeudaById(id);
        }

        public IEnumerable<Deuda> GetDeudas()
        {
            IRepositoryDeuda repository = new RepositoryDeuda();
            return repository.GetDeudas();
        }
    }
}
