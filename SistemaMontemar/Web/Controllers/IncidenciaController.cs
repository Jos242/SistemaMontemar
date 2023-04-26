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
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class IncidenciaController : Controller
    {
        // GET: Incidencia
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Incidencia> lista = null;
            try
            {
                IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();
                lista = _ServiceIncidencia.GetIncidencias();
                ViewBag.status = -69;
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
            IEnumerable<Incidencia> lista = null;
            try
            {
                IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();
                lista = _ServiceIncidencia.GetIncidencias().Where(i => i.IdUsuario == ((Usuario)Session["User"]).Id);
                ViewBag.status = -69;
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
        public ActionResult AjaxCambiar(int idIncidencia)
        {
            IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();
            IEnumerable<Incidencia> lista = null;

            if (idIncidencia > 0)
            {
                Incidencia oIncidencia = _ServiceIncidencia.GetIncidenciaById(idIncidencia);

                oIncidencia.Estado = 1;

                Incidencia save = _ServiceIncidencia.Save(oIncidencia);

                lista = _ServiceIncidencia.GetIncidencias();

                ViewBag.status = -69;
            }
            else
            {
                lista = _ServiceIncidencia.GetIncidencias();
                IEnumerable<Incidencia> listaC = lista.Where(x => x.Estado == 1);
                IEnumerable<Incidencia> listaN = lista.Where(x => x.Estado == 0);

                if(idIncidencia == 0)
                {
                    lista = listaC.Concat(listaN);
                    ViewBag.status = -1;
                } 
                else
                {
                    lista = listaN.Concat(listaC);
                    ViewBag.status = 0;
                }
            }

            if (((Usuario)Session["User"]).IdTipoUsuario == 1)
            {
                return PartialView("_PartialViewEstado", lista);
            }
            else
            {
                lista = lista.Where(i => i.IdUsuario == ((Usuario)Session["User"]).Id);
                return PartialView("_PartialViewEstadoUser", lista);
            }
        }

        [HttpPost]
        public ActionResult Save(Incidencia incidencia)
        {
            IServiceIncidencia _ServiceIncidencia = new ServiceIncidencia();

            Incidencia oIncidencia = null;

            try
            {
                if (ModelState.IsValid)
                {
                    oIncidencia = _ServiceIncidencia.GetIncidenciaById(incidencia.Id);

                    incidencia.IdUsuario = ((Usuario)Session["User"]).Id;
                    incidencia.Estado = 0;
                    incidencia.Fecha = DateTime.Now;

                    Incidencia save = _ServiceIncidencia.Save(incidencia);
                }
                else
                {
                    return PartialView("_PartialViewCreate", incidencia);
                }

                if (((Usuario)Session["User"]).IdTipoUsuario == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("IndexUser");
                }
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "Incidencia";
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
