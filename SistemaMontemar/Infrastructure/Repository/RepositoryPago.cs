using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryPago : IRepositoryPago
    {
        public IEnumerable<Pago> GetPagoByResidencia(int id)
        {
            IEnumerable<Pago> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Pago.Join(ctx.AsignacionPlan,
                                          p => p.IdAsignacion,
                                          a => a.Id,
                                          (p, a) => new { Pago = p, AsignacionPlan = a })
                                    .Join(ctx.Residencia,
                                          a => a.AsignacionPlan.IdResidencia,
                                          r => r.Id,
                                          (a, r) => new { Pago = a.Pago, Residencia = r })
                                    .Where(e => e.Residencia.Id == id)
                                    .Select(p => p.Pago)
                                    .ToList();
                }

                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public IEnumerable<Pago> GetPagos()
        {
            IEnumerable<Pago> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Pago.ToList();
                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
