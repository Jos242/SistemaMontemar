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
    public class RepositoryAsignacionPlan : IRepositoryAsignacionPlan
    {
        public AsignacionPlan GetAsignacionById(int id)
        {
            AsignacionPlan oAsignacionPlan = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oAsignacionPlan = ctx.AsignacionPlan.Where(a => a.Id == id).Include("Residencia.Usuario").Include("PlanCobro.Rubro").FirstOrDefault();
                }
                return oAsignacionPlan;
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

        public IEnumerable<AsignacionPlan> GetAsignacionByResidencia(int idResidencia)
        {
            IEnumerable<AsignacionPlan> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.AsignacionPlan.Where(a => a.IdResidencia == idResidencia).Include("Residencia.Usuario").Include("PlanCobro.Rubro").Include("Pago").Include("Deuda").ToList();
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

        public IEnumerable<AsignacionPlan> GetAsignaciones()
        {
            IEnumerable<AsignacionPlan> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.AsignacionPlan.Include("PlanCobro").Include("Residencia.Usuario").ToList();
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

        public IEnumerable<AsignacionPlan> GetEstadoCuentas()
        {
            IEnumerable<AsignacionPlan> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.AsignacionPlan.GroupBy(h => h.IdResidencia).Select(g => g.FirstOrDefault()).Include(h => h.Residencia.Usuario).Include("PlanCobro").ToList();
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

        public AsignacionPlan Save(AsignacionPlan asignacionPlan)
        {
            int retorno = 0;
            AsignacionPlan oAsignacionPlan = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oAsignacionPlan = GetAsignacionById(asignacionPlan.Id);

                if (oAsignacionPlan == null)
                {
                    ctx.AsignacionPlan.Add(asignacionPlan);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    oAsignacionPlan = ctx.AsignacionPlan.Single(x => x.Id == asignacionPlan.Id);
                    oAsignacionPlan.Estado = asignacionPlan.Estado;
                }
                retorno = ctx.SaveChanges();

            }
            if (retorno >= 0)
                oAsignacionPlan = GetAsignacionById(asignacionPlan.Id);

            return oAsignacionPlan;
        }
    }
}