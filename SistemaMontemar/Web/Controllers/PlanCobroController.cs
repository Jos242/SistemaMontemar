using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using Web.Utils;


namespace Web.Controllers
{
    
    public class PlanCobroController : Controller
    {
        // GET: PlanCobro
        public ActionResult Index()
        {
            IEnumerable<PlanCobro> lista = null;
            try
            {
                IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
                lista = _ServicePlanCobro.GetPlanCobros();
                return View(lista);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PlanCobro/Details/5
        public ActionResult Details(int? id)
        {
            ServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            PlanCobro planCobro = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                planCobro = _ServicePlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                if (planCobro == null)
                {
                    TempData["Message"] = "No Payment plan found";
                    TempData["Redirect"] = "PlanCobro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                return View(planCobro);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data error! " + ex.Message;
                return RedirectToAction("Default", "Error");
            }
        }

        public decimal AjaxResultado(List<string> list)
        {
            IServiceRubro _ServiceRubro = new ServiceRubro();

            decimal res = 0;

            foreach (string item in list)
            {
                Rubro rubro = _ServiceRubro.GetRubroById(Convert.ToInt32(item));
                res += rubro.Cobro;
            }

            PlanCobro plan = new PlanCobro();

            plan.Cobro = res;

            return res;
        }

        // GET: PlanCobro/Create
        public ActionResult Create()
        {
            ViewBag.idRubro = listaRubros();

            return View();
        }

        private MultiSelectList listaRubros(ICollection<Rubro> rubros = null)
        {
            IServiceRubro _ServiceRubro = new ServiceRubro();
            IEnumerable<Rubro> lista = _ServiceRubro.GetRubros();
            //Seleccionar rubros
            int[] listaRubrosSelect = null;
            if (rubros != null)
            {
                listaRubrosSelect = rubros.Select(c => c.Id).ToArray();
            }

            return new MultiSelectList(lista, "Id", "Descripcion", "Cobro", listaRubrosSelect);
        }

      

        // GET: PlanCobro/Edit/5
        public ActionResult Edit(int? id)
        {
            ServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            PlanCobro planCobro = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                planCobro = _ServicePlanCobro.GetPlanCobroById(Convert.ToInt32(id));
                if (planCobro == null)
                {
                    TempData["Message"] = "No payment plan found";
                    TempData["Redirect"] = "PlanCobro";
                    TempData["Redirect-Action"] = "Index";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Listados
                ViewBag.IdRubro = listaRubros(planCobro.Rubro);
                return View(planCobro);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // POST: PlanCobro/Edit/5
        [HttpPost]
        public ActionResult Save(PlanCobro planCobro, string[] selectedRubros)
        {
            //Gestión de archivos
            MemoryStream target = new MemoryStream();
            //Servicio PlanCobro
            IServicePlanCobro _ServicePlanCobro = new ServicePlanCobro();
            try
            {
                
                if (ModelState.IsValid)
                {
                    PlanCobro oPlanCobroI = _ServicePlanCobro.Save(planCobro, selectedRubros);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Utils.Util.ValidateErrors(this);
                    ViewBag.idRubro = listaRubros(planCobro.Rubro);
                    //Cargar la vista crear o actualizar
                    //Lógica para cargar vista correspondiente
                    if (planCobro.Id > 0)
                    {
                        return (ActionResult)View("Edit", planCobro);
                    }
                    else
                    {
                        return View("Create", planCobro);
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data processing Error! " + ex.Message;
                TempData["Redirect"] = "PlanCobro";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        // GET: PlanCobro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlanCobro/Delete/5
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
