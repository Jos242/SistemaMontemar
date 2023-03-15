using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryIncidencia : IRepositoryIncidencia
    {
        public Incidencia GetIncidenciaById(int id)
        {
            Incidencia oIncidencia = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oIncidencia = ctx.Incidencia.Where(i => i.Id == id).Include("Usuario").FirstOrDefault();
                }
                return oIncidencia;
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

        public IEnumerable<Incidencia> GetIncidencias()
        {
            IEnumerable<Incidencia> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Incidencia.Include("Usuario").ToList();
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

        public Incidencia Save(Incidencia incidencia)
        {
            int retorno = 0;
            Incidencia oIncidencia = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oIncidencia = GetIncidenciaById(incidencia.Id);

                if (oIncidencia == null)
                {
                    ctx.Incidencia.Add(incidencia);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    oIncidencia = ctx.Incidencia.Single(x => x.Id == incidencia.Id);
                    oIncidencia.Estado = incidencia.Estado;
                    retorno = ctx.SaveChanges();
                }

            }
            if (retorno >= 0)
                oIncidencia = GetIncidenciaById(incidencia.Id);

            return oIncidencia;
        }
    }
}
