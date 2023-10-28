using DL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static ML.Result Add(ML.Cliente cliente, List<ML.VentaDona> VentaDona)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KrispyKremeEntities context = new DL.KrispyKremeEntities())
                {
                    ObjectParameter IdVenta = new ObjectParameter("IdVenta", typeof(int));

                    cliente.Venta.Fecha = DateTime.Now;

                    var query = context.VentaAdd(IdVenta,
                                                   cliente.IdCliente,
                                                   cliente.Venta.Total,
                                                   cliente.Venta.Fecha);

                    context.SaveChanges();

                    if ((int)IdVenta.Value > 0)
                    {
                        cliente.Venta.IdVenta = (int)IdVenta.Value;

                        ML.Result resultVentaDona = BL.VentaDona.Add(cliente, VentaDona);

                        if (resultVentaDona.Correct = true)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = resultVentaDona.ErrorMessage;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public List<ML.Venta> GetAll(int idDona)
        {
            using (var context = new KrispyKremeEntities())
            {
                var ventas = from venta in context.Ventas
                             where venta.VentaDonas.Any(vd => vd.IdDona == idDona && vd.Cantidad > 10)
                             select new ML.Venta
                             {
                                 IdVenta = venta.IdVenta,
                                 Fecha = venta.Fecha.Value,
                                 Total = venta.Total.Value,
                                 Clientes = new ML.Cliente
                                 {
                                     Nombre = venta.Cliente.Nombre,
                                     ApellidoPaterno = venta.Cliente.ApellidoPaterno,
                                     ApellidoMaterno = venta.Cliente.ApellidoMaterno
                                 }
                             };

                return ventas.ToList();
            }
        }
    }
}
