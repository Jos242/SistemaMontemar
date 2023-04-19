using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryDeuda : IRepositoryDeuda
    {
        public IEnumerable<Deuda> GetDeudaByResidencia(int id)
        {
            IEnumerable<Deuda> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Deuda.Join(ctx.AsignacionPlan,
                                          d => d.IdAsignacion,
                                          a => a.Id,
                                          (d, a) => new { Deuda = d, AsignacionPlan = a })
                                    .Join(ctx.Residencia,
                                          a => a.AsignacionPlan.IdResidencia,
                                          r => r.Id,
                                          (a, r) => new { Deuda = a.Deuda, Residencia = r })
                                    .Where(e => e.Residencia.Id == id)
                                    .Select(d => d.Deuda)
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

        public IEnumerable<Deuda> GetDeudas()
        {
            IEnumerable<Deuda> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Deuda.ToList();
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

        public Deuda Save(Deuda deuda)
        {
            int retorno = 0;
            Deuda oDeuda = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                ctx.Deuda.Add(deuda);
                retorno = ctx.SaveChanges();

            }
            if (retorno >= 0)
                oDeuda = deuda;

            return oDeuda;
        }
    }
}
