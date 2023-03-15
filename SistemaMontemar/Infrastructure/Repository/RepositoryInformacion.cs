using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryInformacion : IRepositoryInformacion
    {
        public Informacion GetInformacionById(int id)
        {
            Informacion oInformacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oInformacion = ctx.Informacion.Where(i => i.Id == id).FirstOrDefault();
                }
                return oInformacion;
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

        public IEnumerable<Informacion> GetInformacions()
        {
            IEnumerable<Informacion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Informacion.ToList();
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

        public Informacion Save(Informacion informacion)
        {
            int retorno = 0;
            Informacion oInformacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oInformacion = GetInformacionById(informacion.Id);

                if (oInformacion == null)
                {
                    ctx.Informacion.Add(informacion);

                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Informacion.Add(informacion);
                    ctx.Entry(informacion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                oInformacion = GetInformacionById(informacion.Id);

            return oInformacion;
        }
    }
}
