using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections;
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
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Residencia/Details/5
        public ActionResult Details(int? id)
        {
            ServiceResidencia _ServiceResidencia = new ServiceResidencia();
            Residencia residencia = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                residencia = _ServiceResidencia.GetResidenciaById(Convert.ToInt32(id));
                if (residencia == null)
                {
                    TempData["Message"] = "No existe la residencia solicitada";
                    TempData["Redirect"] = "Residencia";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(residencia);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos!" + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Residencia/Create
        public ActionResult Create()
        {
            ViewBag.idUsuario = listUsuarios();

            return View();
        }

        private SelectList listUsuarios(int idUsuario = 0)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> lista = _ServiceUsuario.GetUsuarios();
            return new SelectList(lista, "Id", "Nombre " + "Apellido01", idUsuario);
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
