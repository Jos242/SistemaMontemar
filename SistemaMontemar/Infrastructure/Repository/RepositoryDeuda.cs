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
        public Deuda GetDeudaByAsignacionPlan(int id)
        {
            Deuda oDeuda = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oDeuda = ctx.Deuda.Include("AsignacionPlan").Where(u => u.IdAsignacion == id).FirstOrDefault();
                }
                return oDeuda;
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

        public Deuda GetDeudaById(int id)
        {
            Deuda oDeuda = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oDeuda = ctx.Deuda.Include("AsignacionPlan").Where(u => u.Id == id).FirstOrDefault();
                }
                return oDeuda;
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
                                    .Include("AsignacionPlan.Residencia")
                                    .Include("AsignacionPlan.PlanCobro")
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

                    lista = ctx.Deuda.Include("AsignacionPlan.Residencia").Include("AsignacionPlan.PlanCobro").ToList();
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
                oDeuda = GetDeudaById(deuda.Id);
                if (oDeuda == null)
                {
                    ctx.Deuda.Add(deuda);
                }
                else
                {
                    oDeuda = ctx.Deuda.Single(x => x.Id == deuda.Id);
                    oDeuda.Estado = deuda.Estado;
                }
                retorno = ctx.SaveChanges();

            }
            if (retorno >= 0)
                oDeuda = deuda;

            return oDeuda;
        }
    }
}
