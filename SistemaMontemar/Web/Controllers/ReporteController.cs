using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Web.Security;
using Web.Utils;

namespace Web.Controllers
{
    public class ReporteController : Controller
    {
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult Index()
        {
            return View("ReporteDeudas");
        }
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult ReporteDeudas()
        {
            IEnumerable<Deuda> lista = null;
            try
            {
                IServiceDeuda _ServiceDeuda = new ServiceDeuda();
                lista = _ServiceDeuda.GetDeudas().Where(x => x.Estado == 0);

                Session["lista"] = lista;
                Session["listaExp"] = lista;

                IEnumerable<Residencia> residencias = lista.Select(x => x.AsignacionPlan.Residencia).Distinct();
                IEnumerable<int> meses = lista.Select(x => x.AsignacionPlan.Mes).Distinct();
                IEnumerable<int> anios = lista.Select(x => x.AsignacionPlan.Anio).Distinct();

                ViewBag.idResidencia = new SelectList(residencias, "id", "id");
                ViewBag.mes = new SelectList(meses);
                ViewBag.anio = new SelectList(anios);

                Session["ajaxResidencia"] = 0;
                Session["ajaxMes"] = 0;
                Session["ajaxAnio"] = 0;

                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult AjaxFiltrar(int id, int value)
        {
            IEnumerable<Deuda> lista = (IEnumerable<Deuda>)Session["lista"];

            switch (id)
            {
                case 1:
                    Session["ajaxResidencia"] = value;
                    break;

                case 2:
                    Session["ajaxMes"] = value;
                    break;

                case 3:
                    Session["ajaxAnio"] = value;
                    break;
            }

            int valueR = Convert.ToInt32(Session["ajaxResidencia"]);
            int valueM = Convert.ToInt32(Session["ajaxMes"]);
            int valueA = Convert.ToInt32(Session["ajaxAnio"]);

            if (valueR != 0)
            {
                lista = lista.Where(x => x.AsignacionPlan.IdResidencia == valueR);
            }

            if (valueM != 0)
            {
                lista = lista.Where(x => x.AsignacionPlan.Mes == valueM);
            }

            if (valueA != 0)
            {
                lista = lista.Where(x => x.AsignacionPlan.Anio == valueA);
            }

            IEnumerable<Residencia> residencias = ((IEnumerable<Deuda>)Session["lista"]).Select(x => x.AsignacionPlan.Residencia).Distinct();
            IEnumerable<int> meses = ((IEnumerable<Deuda>)Session["lista"]).Select(x => x.AsignacionPlan.Mes).Distinct();
            IEnumerable<int> anios = ((IEnumerable<Deuda>)Session["lista"]).Select(x => x.AsignacionPlan.Anio).Distinct();

            ViewBag.idResidencia = new SelectList(residencias, "id", "id", Convert.ToInt32(Session["ajaxResidencia"]));
            ViewBag.mes = new SelectList(meses, Convert.ToInt32(Session["ajaxMes"]));
            ViewBag.anio = new SelectList(anios, Convert.ToInt32(Session["ajaxAnio"]));

            Session["listaExp"] = lista;

            return PartialView("_PartialViewFiltrar", lista);
        }

        /// <summary>
        /// https://riptutorial.com/itext
        /// Nugget iText7
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize((int)Roles.Administrador)]
        public ActionResult CreatePdfReporteDeudas()
        {
            //Ejemplos IText7 https://kb.itextpdf.com/home/it7kb/examples
            IEnumerable<Deuda> lista = (IEnumerable<Deuda>)Session["listaExp"];
            try
            {
                // Extraer informacion
                IServiceDeuda _ServiceDeuda = new ServiceDeuda();

                // Crear stream para almacenar en memoria el reporte 
                MemoryStream ms = new MemoryStream();
                //Inicializar writer
                PdfWriter writer = new PdfWriter(ms);

                //Inicializar document
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document doc = new Document(pdfDoc);

                //Título
                Paragraph header = new Paragraph("Debt Report")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(14)
                    .SetFontColor(ColorConstants.BLUE);
                doc.Add(header);

                // Crear tabla con 4 columnas 
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(4, true);

                //Encabezados de la tabla
                table.AddHeaderCell("House N°");
                table.AddHeaderCell("Payment Plan"); 
                table.AddHeaderCell("Date");
                table.AddHeaderCell("Total Amount");

                foreach (var item in lista)
                {
                    // Agregar datos a las celdas
                    table.AddCell(new Paragraph(item.AsignacionPlan.IdResidencia.ToString()));
                    table.AddCell(new Paragraph(item.AsignacionPlan.PlanCobro.Descripcion));
                    table.AddCell(new Paragraph(item.AsignacionPlan.Anio + " - " + item.AsignacionPlan.Mes));
                    table.AddCell(new Paragraph(item.AsignacionPlan.PlanCobro.Cobro.ToString()));

                }
                doc.Add(table);

                // Colocar número de páginas
                int numberofPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberofPages; i++)
                {
                    //Texto alineado a un punto específico
                    doc.ShowTextAligned(
                        new Paragraph(
                            String.Format("página {0} de {1}", i, numberofPages)),
                        559, 826, i, TextAlignment.RIGHT, VerticalAlignment.TOP, 0);
                }


                //Terminar document
                doc.Close();
                // Retorna un File
                return File(ms.ToArray(), "application/pdf", "reporte.pdf");

            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Data Error! " + ex.Message;
                TempData.Keep();
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }

        }
    }
}
