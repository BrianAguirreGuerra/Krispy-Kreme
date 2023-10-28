using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result Add(ML.Cliente cliente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.KrispyKremeEntities context = new DL.KrispyKremeEntities())
                {

                    DL.Direccion direccion = new DL.Direccion();

                    direccion.Calle = cliente.Direccion.Calle;
                    direccion.NumInt = cliente.Direccion.NumInt;
                    direccion.NumExt = cliente.Direccion.NumExt;
                    direccion.Colonia = cliente.Direccion.Colonia;
                    direccion.Municipio = cliente.Direccion.Municipio;
                    direccion.Estado = cliente.Direccion.Estado;
                    direccion.IdCliente = cliente.IdCliente;
                    context.Direccions.Add(direccion);
                    int rowsAffected = context.SaveChanges();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "La direccion no fue registrada";
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
