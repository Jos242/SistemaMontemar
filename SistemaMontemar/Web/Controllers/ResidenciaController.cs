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
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        public ActionResult Index()
        {
            IEnumerable<Residencia> lista = null;
            try
            {
                IServiceResidencia _ServiceResidencia = new ServiceResidencia();
                lista = _ServiceResidencia.GetResidencias();
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }

        // GET: Residencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Residencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Residencia/Create
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

        // GET: Residencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Residencia/Edit/5
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

        // GET: Residencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Residencia/Delete/5
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
