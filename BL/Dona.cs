using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.KrispyKremeEntities context = new DL.KrispyKremeEntities())
                {
                    var donasLista = context.Donas.ToList();

                    if (donasLista != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in donasLista)
                        {
                            ML.Dona dona = new ML.Dona();
                            dona.IdDona = obj.IdDona;
                            dona.Nombre = obj.Nombre;
                            dona.Precio = obj.Precio.Value;
                            dona.Detalles = obj.Detalles;
                            result.Objects.Add(dona);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
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
