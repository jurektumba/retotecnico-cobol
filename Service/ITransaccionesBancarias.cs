using retotecnicocobol.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retotecnicocobol.Service
{
    interface ITransaccionesBancarias
    {
        Result ObtenerResultado(List<Cuenta> lista);
    }
}
