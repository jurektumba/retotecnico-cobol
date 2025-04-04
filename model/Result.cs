using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retotecnicocobol.model
{
    //entidad Resultante que recogerá las operaciones que requiere
    public class Result
    {
        public Double balanceFinal { get; set; }
        public Double transaccionMayormonto { get; set; }
        public Double conteoTransaccionesDebito { get; set; }
        public Double conteoTransaccionesCredito { get; set; }
        public int idMayor { get; set; }
    }
}
