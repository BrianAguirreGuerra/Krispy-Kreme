using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult Form()
        {
            ML.Cliente cliente = new ML.Cliente();
            ML.Result resultDona = BL.Dona.GetAll();
            cliente.Dona = new ML.Dona();
            cliente.Dona.Donas = resultDona.Objects.Cast<ML.Dona>().ToList();
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Form(ML.Cliente cliente, ML.Direccion direccion, List<ML.VentaDona> VentaDona, decimal total)
        {
            cliente.Venta = new ML.Venta();
            cliente.Venta.Total = total;
            cliente.Direccion = direccion;
            ML.Result resultCliente = BL.Cliente.Add(cliente, VentaDona);

            if (resultCliente.Correct)
            {
                return Json(new { Correct = true, Message = "Se ha realizado correctamente la compra" });
            }
            else
            {
                return Json(new { Correct = false, Message = "No se ha podido realizar la compra. Error: " + resultCliente.ErrorMessage });
            }
        }

    }
}