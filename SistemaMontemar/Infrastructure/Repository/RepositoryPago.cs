using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
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

        public void GetPagoDate(out string etiquetas, out string valores)
        {
            String varEtiquetas = "";
            String varValores = "";
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Obtener la cantidad de ordenes por fecha
                    var currentYear = DateTime.Now.Year;
                    var pagos = ctx.Pago.Where(p => p.FechaPago.Year == currentYear).ToList();

                    var resultado = pagos.GroupBy(x => new { x.FechaPago.Year, x.FechaPago.Month }).
                        Select(
                        o => new
                        {
                            Count = o.Count(),
                            Total = o.Sum(p => p.Total),
                            FechaPago = new DateTime(o.Key.Year, o.Key.Month, 1)

                        });
                    //Crear etiquetas y valores
                    var englishCulture = new CultureInfo("en-US");
                    foreach (var item in resultado)
                    {

                        varEtiquetas += item.FechaPago.ToString("MMMM yyyy", englishCulture) + ": ₡" + item.Total.ToString("#,0.##", CultureInfo.CurrentCulture) + ",";
                        varValores += item.Count + ",";
                    }

                }
                //Ultima coma
                varEtiquetas = varEtiquetas.Substring(0, varEtiquetas.Length - 1); // ultima coma
                varValores = varValores.Substring(0, varValores.Length - 1);
                //Asignar valores de salida
                etiquetas = varEtiquetas;
                valores = varValores;
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
                throw new Exception(mensaje);
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
