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
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ResidenciaController : Controller
    {
        // GET: Residencia
        [CustomAuthorize((int)Roles.Administrador)]
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
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxAsignarPlan(int idResidencia)
        {
            IServiceResidencia _ServiceResidencia = new ServiceResidencia();
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();
            IServicePago _ServicePago = new ServicePago();

            AsignacionPlan asignacionPlan = null;
            Residencia residencia = _ServiceResidencia.GetResidenciaById(idResidencia);
            AsignacionPlan asignacionPlanActual = _ServiceAsignacionPlan.GetAsignacionByResidencia(idResidencia).Where(a => a.Mes == DateTime.Now.Month && a.Anio == DateTime.Now.Year).FirstOrDefault();
            if (asignacionPlanActual != null)
            {
                if (asignacionPlanActual.Estado == 1)
                {
                    AsignacionPlan asignacionPlanFuturo = _ServiceAsignacionPlan.GetAsignacionByResidencia(idResidencia).Where(a => a.Mes == (DateTime.Now.Month) + 1 && a.Anio == DateTime.Now.Year).FirstOrDefault();

                    if (asignacionPlanFuturo == null)
                    {
                        asignacionPlan = new AsignacionPlan
                        {
                            Residencia = _ServiceResidencia.GetResidenciaById(idResidencia),
                            PlanCobro = _ServicePlanCobro.GetPlanCobroById(1),
                            Mes = (DateTime.Now.Month) + 1,
                            Anio = DateTime.Now.Year,
                            Estado = 0
                        };
                    }
                    else
                    {
                        asignacionPlan = asignacionPlanFuturo;
                    }
                }
                else
                {
                    asignacionPlan = asignacionPlanActual;
                }
            }
            else
            {
                asignacionPlan = new AsignacionPlan
                {
                    Residencia = _ServiceResidencia.GetResidenciaById(idResidencia),
                    PlanCobro = _ServicePlanCobro.GetPlanCobroById(1),
                    Mes = (DateTime.Now.Month),
                    Anio = DateTime.Now.Year,
                    Estado = 0
                };
            }

            ViewBag.idPlanCobro = listPlanCobros(asignacionPlan.IdPlan);

            return PartialView("_PartialViewAsignPaymentPlan", asignacionPlan);
        }

        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult AjaxGetPlanCobro(int idPlanCobro)
        {
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            PlanCobro planCobro = _ServicePlanCobro.GetPlanCobroById(idPlanCobro);

            return PartialView("_PartialViewGetPlanCobro", planCobro);
        }

        private SelectList listPlanCobros(int idPlanCobro = 0)
        {
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            IEnumerable<PlanCobro> lista = _ServicePlanCobro.GetPlanCobros();
            return new SelectList(lista, "Id", "Descripcion", idPlanCobro);
        }

        // GET: Residencia/Details/5
        [CustomAuthorize((int)Roles.Administrador)]
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
                    TempData["Message"] = "No Residence found";
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
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: Residencia/Create
        [CustomAuthorize((int)Roles.Administrador)]
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
        [CustomAuthorize((int)Roles.Administrador)]
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
                TempData["Message"] = "Data error! " + ex.Message;
                TempData["Redirect"] = "Residencia";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: Residencia/Edit/5
        [HttpPost]
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult SaveAsignacionPlan(AsignacionPlan asignacionPlan)
        {
            IServiceAsignacionPlan _ServiceAsignacionPlan = new ServiceAsignacionPlan();

            try
            {
                AsignacionPlan oAsignacionPlan = _ServiceAsignacionPlan.Save(asignacionPlan);

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
