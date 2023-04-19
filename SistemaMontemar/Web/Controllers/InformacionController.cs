using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class InformacionController : Controller
    {
        // GET: Informacion
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Informacion> lista = null;
            try
            {
                IServiceInformacion _ServiceInformacion = new ServiceInformacion();
                lista = _ServiceInformacion.GetInformacions();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Informacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Informacion/Create
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Create()
        {
            ViewBag.Tipos = tipoLista();

            return View();
        }

        public SelectList tipoLista(int id = 0)
        {
            Informacion informacion = new Informacion();
            List<SelectListItem> tipos = new List<SelectListItem>();

            foreach (KeyValuePair<int, string> item in informacion.Tipos)
            {
                tipos.Add(new SelectListItem { Value = item.Key.ToString(), Text = item.Value });
            }
            SelectList list = new SelectList(tipos, "Value", "Text", id);

            return list;
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Edit(int? id)
        {
            IServiceInformacion _ServiceInformacion = new ServiceInformacion();
            Informacion informacion = null;

            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                informacion = _ServiceInformacion.GetInformacionById(Convert.ToInt32(id));
                if (informacion == null)
                {
                    TempData["Message"] = "No Announcement found";
                    TempData["Redirect"] = "Informacion";
                    TempData["Redirect-Action"] = "Index";
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.Tipos = tipoLista(informacion.Id);

                return View(informacion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "Informacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Informacion/Create
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

        // GET: Informacion/Edit/5
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Informacion informacion)
        {
            MemoryStream target = new MemoryStream();

            IServiceInformacion _ServiceInformacion = new ServiceInformacion();

            try
            {
                if (ModelState.IsValid)
                {
                    Informacion oInformacion = _ServiceInformacion.Save(informacion);
                }
                else
                {
                    return View("Create", informacion);
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

        // GET: Informacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Informacion/Delete/5
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
