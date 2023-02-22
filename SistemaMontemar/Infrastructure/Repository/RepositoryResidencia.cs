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
    public class RepositoryResidencia : IRepositoryResidencia
    {
        public Residencia GetResidenciaById(int id)
        {
            Residencia oResidencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oResidencia = ctx.Residencia.Where(r => r.Id == id).Include("Usuario").FirstOrDefault();
                }
                return oResidencia;
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

        public IEnumerable<Residencia> GetResidencias()
        {
            IEnumerable<Residencia> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Residencia.Include("Usuario").ToList();
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

        public Residencia Save(Residencia residencia)
        {
            int retorno = 0;
            Residencia oResidencia = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oResidencia = GetResidenciaById(residencia.Id);

                if (oResidencia == null)
                {
                    ctx.Residencia.Add(residencia);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Residencia.Add(residencia);
                    ctx.Entry(residencia).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                oResidencia = GetResidenciaById(residencia.Id);

            return oResidencia;
        }
    }
}
