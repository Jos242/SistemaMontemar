using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
            IEnumerable<AsignacionPlan> listAsignacionPlan = _ServiceAsignacionPlan.GetAsignacionByResidencia(1);
            listAsignacionPlan = listAsignacionPlan.OrderBy(x => x.Anio);

            ViewBag.Pagos = listPagos(1);
            ViewBag.Deudas = listDeudas(1);

            return View(listAsignacionPlan);
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

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
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

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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
