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

        public void GetPagoDate(out string etiquetas1, out string valores1)
        {
            IRepositoryPago repository = new RepositoryPago();

            repository.GetPagoDate(out string etiquetas, out string valores);
            etiquetas1 = etiquetas;
            valores1 = valores;
        }

        public Pago GetPagoById(int id)
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagoById(id);
        }

        public Pago Save(Pago pago)
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.Save(pago);
        }

        public Pago GetPagoByAsignacionPlan(int id)
        {
            IRepositoryPago repository = new RepositoryPago();
            return repository.GetPagoByAsignacionPlan(id);
        }
    }
}
