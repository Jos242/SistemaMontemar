using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        public ActionResult Index()
        {
            IEnumerable<Incidencia> lista = null;
            try
            {
                IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();
                lista = _ServiceIncidencia.GetIncidencias();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Incidencia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Incidencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Incidencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        public ActionResult AjaxCreate()
        {
            return PartialView("_PartialViewCreate");
        }

        [HttpPost]
        public ActionResult Save(Incidencia incidencia)
        {
            MemoryStream target = new MemoryStream();

            IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();

            Incidencia oIncidencia = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Session["User"] = new ServiceUsuario().GetUsuarioById(9);

                    oIncidencia = _ServiceIncidencia.GetIncidenciaById(incidencia.Id);

                    if (oIncidencia == null)
                    {
                        incidencia.IdUsuario = ((Usuario)Session["User"]).Id;
                        incidencia.Estado = 0;
                        incidencia.Fecha = DateTime.Now;
                    }
                    else
                    {
                        incidencia.Estado = incidencia.Estado == 0 ? 1 : 0;
                    }

                    Incidencia save = _ServiceIncidencia.Save(incidencia);
                }
                else
                {
                    return View("Create", incidencia);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "Residencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Incidencia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Incidencia/Delete/5
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
