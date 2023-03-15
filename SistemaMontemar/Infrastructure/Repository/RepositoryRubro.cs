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
    public class RepositoryRubro : IRepositoryRubro
    {
        public Rubro GetRubroById(int id)
        {
            Rubro oRubro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    oRubro = ctx.Rubro.Where(u => u.Id == id).FirstOrDefault();
                }
                return oRubro;
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

        public IEnumerable<Rubro> GetRubros()
        {
            IEnumerable<Rubro> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Rubro.ToList();
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

        public Rubro Save(Rubro rubro)
        {
            int retorno = 0;
            Rubro oRubro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oRubro = GetRubroById((int)rubro.Id);
                IRepositoryRubro _RepositoryRubro = new RepositoryRubro();

                if (oRubro == null)
                {
                    //Insertar Rubro
                    ctx.Rubro.Add(rubro);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Rubro
                    ctx.Rubro.Add(rubro);
                    ctx.Entry(rubro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    
                }
            }

            if (retorno >= 0)
                oRubro = GetRubroById((int)rubro.Id);

            return oRubro;
        }

    }
}
