using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retotecnicocobol.model
{
    //entidad cuenta que nos servirá para mapear los valores del csv y asignará a los atributos de c#
    public class Cuenta
    {
        public int id { get; set; }
        public string tipo { get; set; }
        public Double monto { get; set; }
    }
}
