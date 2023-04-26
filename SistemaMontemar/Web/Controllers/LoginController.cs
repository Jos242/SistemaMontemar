using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Index(Usuario usuario)
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                ModelState.Remove("IdTipoUsuario");
                ModelState.Remove("Nombre");
                ModelState.Remove("Apellido01");
                ModelState.Remove("Telefono");
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.GetUsuario(usuario.Email, usuario.Password);
                    if (oUsuario != null)
                    {
                        if (oUsuario.Estado == 1)
                        {
                            Session["User"] = oUsuario;
                            Log.Info($"Accede{oUsuario.Nombre} {oUsuario.Apellido01} " +
                                $"con el rol {oUsuario.IdTipoUsuario}");
                            TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login",
                                "User authorized", Util.SweetAlertMessageType.success);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            Log.Warn($"Intento de inicio de sesion{usuario.Email}");
                            ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                                "User Disabled", Util.SweetAlertMessageType.warning);
                        }
                    }
                    else
                    {
                        Log.Warn($"Intento de inicio de sesion{usuario.Email}");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login",
                            "User not found", Util.SweetAlertMessageType.warning);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
            return View("Index");
        }
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "No autorizado";
            if (Session["User"] != null)
            {
                Usuario usuario = Session["User"] as Usuario;
                Log.Warn($"No autorizado {usuario.Email}");
            }
            return View();
        }
        public ActionResult Logout()
        {
            try
            {
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
    }
}