using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryPago
    {
        IEnumerable<Pago> GetPagos();
        IEnumerable<Pago> GetPagoByResidencia(int id);
    }
}
