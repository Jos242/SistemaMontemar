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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult AjaxCambiar(int idUsuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> lista = null;

            Usuario oUsuario = _ServiceUsuario.GetUsuarioById(idUsuario);

            oUsuario.Estado = oUsuario.Estado == 1 ? 0 : 1;

            Usuario save = _ServiceUsuario.Save(oUsuario);

            lista = _ServiceUsuario.GetUsuarios();

            return PartialView("_PartialViewEstado", lista);
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Save(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();

            try
            {
                if (ModelState.IsValid)
                {
                    usuario.DiaPagar = DateTime.Now.Day;
                    usuario.Estado = 1;

                    Usuario save = _ServiceUsuario.Save(usuario);
                }
                else
                {

                    return View("Create", usuario);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "Reservacion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
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
