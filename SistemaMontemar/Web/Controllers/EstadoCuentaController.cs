using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class EstadoCuentaController : Controller
    {
        // GET: EstadoCuenta
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

        // GET: EstadoCuenta/Details/5
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
        public ActionResult Edit(int id)
        {
            return View();
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
