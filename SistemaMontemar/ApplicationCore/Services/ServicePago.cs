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
        public IEnumerable<Pago> GetPagoByResidencia(int id)
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagoByResidencia(id);
        }

        public IEnumerable<Pago> GetPagos()
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagos();
        }
    }
}
