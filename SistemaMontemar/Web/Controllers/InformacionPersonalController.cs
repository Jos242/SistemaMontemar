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
    public class InformacionPersonalController : Controller
    {
        // GET: InformacionPersonal
        public ActionResult Index()
        {
            try { 
                IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();

                Residencia residencia = _ServiceResidencia.GetResidenciaByIdUsuario(((Usuario)Session["User"]).Id);

                IEnumerable<AsignacionPlan> listAsignacionPlan = null;
                if (residencia != null )
                {
                    listAsignacionPlan = _ServiceAsignacionPlan.GetAsignacionByResidencia(residencia.Id);
                    listAsignacionPlan = listAsignacionPlan.OrderBy(x => x.Anio);

                    ViewBag.Pagos = listPagos(residencia.Id);
                    ViewBag.Deudas = listDeudas(residencia.Id);
                }

                return View(listAsignacionPlan);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
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

        // GET: InformacionPersonal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InformacionPersonal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InformacionPersonal/Create
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

        // GET: InformacionPersonal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InformacionPersonal/Edit/5
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

        // GET: InformacionPersonal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InformacionPersonal/Delete/5
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
