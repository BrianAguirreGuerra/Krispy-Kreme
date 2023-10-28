using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class VentaDona
    {
        public static ML.Result Add(ML.Cliente cliente, List<ML.VentaDona> VentaDona)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KrispyKremeEntities context = new DL.KrispyKremeEntities())
                {
                    foreach (var item in VentaDona)
                    {
                        DL.VentaDona ventaDona = new DL.VentaDona();

                        ventaDona.IdVenta = cliente.Venta.IdVenta;
                        ventaDona.IdDona = item.IdDona; 
                        ventaDona.Cantidad = item.Cantidad; 

                        context.VentaDonas.Add(ventaDona);
                        int rowsAffected = context.SaveChanges();

                        if (rowsAffected <= 0)
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La venta no fue registrada";
                            break;
                        }
                        else
                        {
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}
