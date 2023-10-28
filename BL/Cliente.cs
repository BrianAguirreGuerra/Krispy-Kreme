using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cliente
    {
        public static ML.Result Add(ML.Cliente cliente, List<ML.VentaDona> VentaDona)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KrispyKremeEntities context = new DL.KrispyKremeEntities())
                {

                    ObjectParameter IdCliente = new ObjectParameter("IdCliente", typeof(int));

                    var query = context.ClienteAdd(IdCliente,
                                                   cliente.Nombre,
                                                   cliente.ApellidoPaterno,
                                                   cliente.ApellidoMaterno);

                    context.SaveChanges();

                    if ((int)IdCliente.Value > 0)
                    {
                        cliente.IdCliente = (int)IdCliente.Value;

                        ML.Result resultDireccion = BL.Direccion.Add(cliente);
                        ML.Result resultVenta = BL.Venta.Add(cliente, VentaDona);

                        if (resultVenta.Correct)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = resultDireccion.ErrorMessage;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
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
