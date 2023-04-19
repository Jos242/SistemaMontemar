using ApplicationCore.Services;
using Infrastructure.Models;
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
    public class ReservacionController : Controller
    {
        // GET: Reservacion
        public ActionResult Index()
        {
            IEnumerable<Reservacion> lista = null;
            try
            {
                IServiceReservacion _ServiceReservacion = new ServiceReservacion();
                lista = _ServiceReservacion.GetReservacions();
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

        public ActionResult AjaxCambiar(int idReservacion, int AccORjc)
        {
            IServiceReservacion _ServiceReservacion = new ServiceReservacion();
            IEnumerable<Reservacion> lista = null;

            if (idReservacion > 0 && AccORjc == 1)
            {
                Reservacion oReservacion = _ServiceReservacion.GetReservacionById(idReservacion);

                oReservacion.Estado = 1;

                Reservacion save = _ServiceReservacion.Save(oReservacion);

                lista = _ServiceReservacion.GetReservacions();

                ViewBag.status = -69;
            } 
            else if (idReservacion > 0 && AccORjc == 2) 
            {
                Reservacion oReservacion = _ServiceReservacion.GetReservacionById(idReservacion);

                oReservacion.Estado = 2;

                Reservacion save = _ServiceReservacion.Save(oReservacion);

                lista = _ServiceReservacion.GetReservacions();

                ViewBag.status = -69;

            }
            else
            {
                lista = _ServiceReservacion.GetReservacions();
                IEnumerable<Reservacion> listaRjc = lista.Where(x => x.Estado == 2);
                IEnumerable<Reservacion> listaApr = lista.Where(x => x.Estado == 1);
                IEnumerable<Reservacion> listaPnd = lista.Where(x => x.Estado == 0);

                if (idReservacion == 0)
                {
                    lista = listaApr.Concat(listaPnd).Concat(listaRjc);
                    ViewBag.status = -1;
                }
                else if (idReservacion == -1)
                {
                    lista = listaPnd.Concat(listaApr).Concat(listaRjc);
                    ViewBag.status = -2;
                }
                else
                {
                    lista = listaRjc.Concat(listaApr).Concat(listaPnd);
                    ViewBag.status = 0;
                }
            }


            return PartialView("_PartialViewEstado", lista);
        }

        

        // GET: Reservacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservacion/Create
        public ActionResult Create()
        {
            ViewBag.idArea = listaAreas();

            return View();
        }

        private SelectList listaAreas(int idArea = 0)
        {
            IServiceArea _ServiceArea = new ServiceArea();
            IEnumerable<Area> lista = _ServiceArea.GetAreas();
            return new SelectList(lista, "Id", "Descripcion", idArea);
        }



        // POST: Reservacion/Create
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

        // GET: Reservacion/Edit/5
        public ActionResult Save(Reservacion reservacion)
        {
            MemoryStream target = new MemoryStream();

            IServiceReservacion _ServiceReservacion = new ServiceReservacion();

            Reservacion oReservacion = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Session["User"] = new ServiceUsuario().GetUsuarioById(1);

                    bool hayReservaciones = _ServiceReservacion.RevisarFechas(reservacion.FechaInicio, reservacion.FechaFinal, reservacion.IdArea);
                    if (hayReservaciones)
                    {
                        ModelState.AddModelError(string.Empty, "The selected dates overlap with an existing reservation.");
                        ViewBag.idArea = listaAreas();
                        return View("Create", reservacion);
                    }


                    oReservacion = _ServiceReservacion.GetReservacionById(reservacion.Id);

                    reservacion.IdUsuario = ((Usuario)Session["User"]).Id;
                    reservacion.Estado = 0;

                    Reservacion save = _ServiceReservacion.Save(reservacion);
                }
                else
                {
                 
                    return View("Create", reservacion);
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

        // POST: Reservacion/Edit/5
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

        // GET: Reservacion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservacion/Delete/5
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
