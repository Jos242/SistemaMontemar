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
            return new SelectList(lista, "Id", "FullName", idUsuario);
        }

        // GET: Residencia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Residencia/Edit/5
        [HttpPost]
        public ActionResult Save(Residencia residencia, HttpPostedFileBase ImageFile)
        {
            MemoryStream target = new MemoryStream();

            IServiceResidencia _ServiceResidencia = new ServiceResidencia();

            try
            {
                if (residencia.Imagen == null)
                {
                    if (ImageFile != null)
                    {
                        ImageFile.InputStream.CopyTo(target);
                        residencia.Imagen = target.ToArray();
                        ModelState.Remove("Imagen");
                    }
                }
                if (ModelState.IsValid)
                {
                    Residencia oResidencia = _ServiceResidencia.Save(residencia);
                }
                else
                {
                    //Cargar la vista crear o actualizar

                    ViewBag.idAutor = listUsuarios(residencia.IdUsuario);
                    //Lógica para cagar vista correpondiente
                    return View("Create", residencia);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Libro";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
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
