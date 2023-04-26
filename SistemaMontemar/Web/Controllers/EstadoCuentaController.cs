using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class EstadoCuentaController : Controller
    {
        // GET: EstadoCuenta
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<AsignacionPlan> lista = null;
            try
            {
                IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
                lista = _ServiceAsignacionPlan.GetEstadoCuentas();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult IndexUser()
        {
            IEnumerable<AsignacionPlan> lista = null;
            try
            {
                IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
                lista = _ServiceAsignacionPlan.GetAsignaciones().Where(x => x.Residencia.IdUsuario == ((Usuario)Session["User"]).Id && x.Estado == 0);
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: EstadoCuenta/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Details(int? id)
        {
            IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
            AsignacionPlan asignacion = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                asignacion = _ServiceAsignacionPlan.GetAsignacionById(Convert.ToInt32(id));
                if (asignacion == null)
                {
                    TempData["Message"] = "No Account Status found";
                    TempData["Redirect"] = "EstadoCuenta";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }

                ViewBag.Pagos = listPagos(asignacion.IdResidencia);
                ViewBag.Deudas = listDeudas(asignacion.IdResidencia);
                return View(asignacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "EstadoCuenta";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private IEnumerable<Pago> listPagos(int idResidencia)
        {
            IServicePago _ServicePago = new ServicePago();
            IEnumerable<Pago> lista = _ServicePago.GetPagoByResidencia(idResidencia);
            return lista;
        }

        private IEnumerable<Deuda> listDeudas(int idResidencia)
        {
            IServiceDeuda _ServiceDeuda = new ServiceDeuda();
            IEnumerable<Deuda> lista = _ServiceDeuda.GetDeudaByResidencia(idResidencia);
            return lista;
        }

        // GET: EstadoCuenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoCuenta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoCuenta/Edit/5
        public ActionResult Save(int idAsignacionPlan)
        {
            IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
            IServiceDeuda _ServiceDeuda = new ServiceDeuda();
            IServicePago _ServicePago = new ServicePago();
            try
            {
                AsignacionPlan asignacionPlan = _ServiceAsignacionPlan.GetAsignacionById(idAsignacionPlan);
                Deuda deuda = _ServiceDeuda.GetDeudaByAsignacionPlan(idAsignacionPlan);

                if (ModelState.IsValid)
                {
                    asignacionPlan.Estado = 1;
                    deuda.Estado = 1;

                    AsignacionPlan saveA = _ServiceAsignacionPlan.Save(asignacionPlan);
                    Deuda saveD = _ServiceDeuda.Save(deuda);

                    Pago pago = new Pago
                    {
                        Id = 0,
                        IdAsignacion = idAsignacionPlan,
                        FechaPago = DateTime.Now,
                        Total = asignacionPlan.PlanCobro.Cobro
                    };

                    Pago saveP = _ServicePago.Save(pago);
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "EstadoCuenta";
                TempData["Redirect-Action"] = "IndexUser";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: EstadoCuenta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoCuenta/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadoCuenta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
