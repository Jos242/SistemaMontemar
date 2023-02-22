using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServicePago : IServicePago
    {
        public Pago GetPagoById(int id)
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagoById(id);
        }

        public IEnumerable<Pago> GetPagos()
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagos();
        }
    }
}
