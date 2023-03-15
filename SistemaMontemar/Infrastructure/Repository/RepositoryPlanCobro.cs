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
    public class RepositoryPlanCobro : IRepositoryPlanCobro
    {
        public PlanCobro GetPlanCobroById(int id)
        {
            PlanCobro oPlanCobro = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener libro por ID incluyendo el autor y todas sus categorías
                    oPlanCobro = ctx.PlanCobro.
                        Where(l => l.Id == id).
                        Include("Rubro").
                        FirstOrDefault();
                }
                return oPlanCobro;
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

        public IEnumerable<PlanCobro> GetPlanCobros()
        {
            IEnumerable<PlanCobro> lista = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.PlanCobro.ToList();
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

        public PlanCobro Save(PlanCobro planCobro, string[] selectedRubros)
        {
            int retorno = 0;
            PlanCobro oPlanCobro = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oPlanCobro = GetPlanCobroById((int)planCobro.Id);
                IRepositoryRubro _RepositoryRubro = new RepositoryRubro();

                if (oPlanCobro == null)
                {

                    //Insertar
                    //Logica para agregar las rubros al planCobro
                    if (selectedRubros != null)
                    {

                        planCobro.Rubro = new List<Rubro>();
                        foreach (var rubro in selectedRubros)
                        {
                            var rubroToAdd = _RepositoryRubro.GetRubroById(int.Parse(rubro));
                            ctx.Rubro.Attach(rubroToAdd); //sin esto, EF intentará crear una categoría
                            planCobro.Rubro.Add(rubroToAdd);// asociar a la categoría existente con el planCobro


                        }
                    }
                    //Insertar PlanCobro
                    ctx.PlanCobro.Add(planCobro);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar PlanCobro
                    ctx.PlanCobro.Add(planCobro);
                    ctx.Entry(planCobro).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    //Logica para actualizar Rubros
                    var selectedRubrosID = new HashSet<string>(selectedRubros);
                    if (selectedRubros != null)
                    {
                        ctx.Entry(planCobro).Collection(p => p.Rubro).Load();
                        var newRubroForPlanCobro = ctx.Rubro
                         .Where(x => selectedRubrosID.Contains(x.Id.ToString())).ToList();
                        planCobro.Rubro = newRubroForPlanCobro;

                        ctx.Entry(planCobro).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oPlanCobro = GetPlanCobroById((int)planCobro.Id);

            return oPlanCobro;
        }

    }
}
