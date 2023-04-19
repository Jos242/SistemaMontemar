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
    public class RepositoryReservacion:IRepositoryReservacion
    {
        public Reservacion GetReservacionById(int id)
        {
            Reservacion oReservacion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oReservacion = ctx.Reservacion.Where(i => i.Id == id).Include("Area").Include("Usuario").FirstOrDefault();
                }
                return oReservacion;
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

        public IEnumerable<Reservacion> GetReservacions()
        {
            IEnumerable<Reservacion> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Reservacion.Include("Area").Include("Usuario").ToList();
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

        public Reservacion Save(Reservacion reservacion)
        {
            int retorno = 0;
            Reservacion oReservacion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oReservacion = GetReservacionById(reservacion.Id);

                if (oReservacion == null)
                {
                    ctx.Reservacion.Add(reservacion);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    oReservacion = ctx.Reservacion.Single(x => x.Id == reservacion.Id);
                    oReservacion.Estado = reservacion.Estado;
                    retorno = ctx.SaveChanges();
                }

            }
            if (retorno >= 0)
                oReservacion = GetReservacionById(reservacion.Id);

            return oReservacion;
        }

        public bool RevisarFechas(DateTime start, DateTime end, int area) 
        {
            bool hay = true;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    // if true entonces si hay reservacion en esas horas
                    hay = ctx.Reservacion.Any(r => r.Estado == 1 &&
                         ((r.FechaInicio <= start && r.FechaFinal >= start && r.IdArea == area) ||
                          (r.FechaInicio <= end && r.FechaFinal >= end && r.IdArea == area) ||
                          (r.FechaInicio >= start && r.FechaFinal <= end && r.IdArea == area)));
                }
                return hay;
                
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
