using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dona
    {
        public int IdDona { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Detalles { get; set; }
        public List<Dona> Donas { get; set; }
        public int SelectedDonaId { get; set; }
        public List<ML.Venta> Ventas { get; set; }
    }
}
