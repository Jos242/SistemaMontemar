using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(Exception exception)
        {
            //Controlador
            String redirect = "Home";
            //Acción de controlador
            String redirectAction = "Index";

            HttpException httpException = exception as HttpException;
            //Obtener el código de estado HTTP
            int error = httpException != null ? httpException.GetHttpCode() : 0;
            switch (error)
            {
                case 400:
                    ViewBag.Title = "Wrong request";
                    ViewBag.Description = "The server couldn't understand invalid syntaxis.";
                    break;

                case 401:
                    ViewBag.Title = "Not authorized";
                    ViewBag.Description = "User must be authenticated to reach the other end.";
                    break;
                case 403:
                    ViewBag.Title = "Restricted access";
                    ViewBag.Description = "User doesn't have the necessary privileges to access content";
                    break;
                default:
                    ViewBag.Title = error + " Error";
                    ViewBag.Description = exception.Message;
                    break;
            }
            ViewBag.redirect = redirect;
            ViewBag.redirectAction = redirectAction;

            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Default()
        {
            //Controlador
            String redirect = "Home";
            //Acción de controlador
            String redirectAction = "Index";
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Title = "Error";
                ViewBag.Description = TempData["Message"];
                if (TempData.ContainsKey("Redirect"))
                {
                    redirect = (String)TempData["Redirect"];
                }
                if (TempData.ContainsKey("Redirect-Action"))
                {
                    redirectAction = (String)TempData["Redirect-Action"];
                }
                ViewBag.redirect = redirect;
                ViewBag.redirectAction = redirectAction;
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}