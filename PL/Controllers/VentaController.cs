using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Ventas()
        {
            ML.Dona dona = new ML.Dona();
            ML.Result resultDona = BL.Dona.GetAll();
            dona.Donas = resultDona.Objects.Cast<ML.Dona>().ToList();
            return View(dona);
        }

        [HttpPost]
        public ActionResult Ventas(ML.Dona dona)
        {
            ML.Result resultDona = BL.Dona.GetAll();
            dona.Donas = resultDona.Objects.Cast<ML.Dona>().ToList();
            int selectedDonaId = dona.IdDona;

            BL.Venta venta = new BL.Venta();
            List<ML.Venta> ventas = venta.GetAll(selectedDonaId);
            dona.Ventas = ventas;
            return View(dona);
        }

    }
}