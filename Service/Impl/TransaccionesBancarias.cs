using retotecnicocobol.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace retotecnicocobol.Service.Impl
{
    public class TransaccionesBancarias : ITransaccionesBancarias
    {
        Result ITransaccionesBancarias.ObtenerResultado(List<Cuenta> lista)
        {
            //instancio el objeto resultado que voy a retornar y asigno valor 0 a todas atributos
            Result objResult = new Result();
            objResult.balanceFinal = 0;
            objResult.transaccionMayormonto = 0;
            objResult.conteoTransaccionesDebito = 0;
            objResult.conteoTransaccionesCredito = 0;
            //realizo el for que recorrerá todos las filas y hará las validaciones y sumas correspondientes
            foreach (Cuenta item in lista)
            {
                //busca el mayor monto y si encuentra uno mayor al actual, lo asigna
                if (item.monto > objResult.transaccionMayormonto)
                {
                    objResult.transaccionMayormonto = item.monto;
                    objResult.idMayor = item.id;
                }
                //si es tipo Crédito suma en cuenta de Créditos
                if (item.tipo.Equals("Crédito"))
                {
                    objResult.conteoTransaccionesCredito++;
                }
                //si es tipo Débito suma en cuenta de Débitos
                if (item.tipo.Equals("Débito"))
                {
                    objResult.conteoTransaccionesDebito++;
                }
                //Suma todos los montos al balance final
                objResult.balanceFinal += item.monto;
            }
            //se retorna el objeto Resultado
            return objResult;
        }
    }
}
