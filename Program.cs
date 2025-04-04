using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using retotecnicocobol.model;
using retotecnicocobol.Service;
using retotecnicocobol.Service.Impl;

//se obtiene la ruta del archivo csv
string currentDirectory = Directory.GetCurrentDirectory();
string projectRoot = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
string filePath = Path.Combine(projectRoot, "data.csv");
//se crear la coleccion de services para su futura inyección de dependencias
var serviceCollection = new ServiceCollection();
serviceCollection.AddTransient<ITransaccionesBancarias, TransaccionesBancarias>();
var serviceProvider = serviceCollection.BuildServiceProvider();
//se crea el objeto y se inyecta la dependencia
var _tb = serviceProvider.GetService<ITransaccionesBancarias>();

try//se inicia la intención para captura de excepciones
{
    //se revisa si existe el archivo en la ruta especificada
    if (File.Exists(filePath))
    {
        //se inicia la configuración y si el campo no se encuentra
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",                        //para la delimitación por ;
            HeaderValidated = null,                 //desabilita la validacion de cabecera
            MissingFieldFound = null,               //Ignora los campos de error
            Encoding = System.Text.Encoding.UTF8    //se realiza la configuración con codificacion UTF-8
        };

        using (var reader = new StreamReader(filePath))//se instancia un objeto StreamReader del archivo
        using (var csv = new CsvReader(reader, config))//se instancia un objeto CsvReader del objeto StreamReader 
        {
            List<Cuenta> cuentas = csv.GetRecords<Cuenta>().ToList(); // obtengo del csv la lista de filas siguiendo la estructura de la entidad Cuenta
            Result resultado = _tb.ObtenerResultado(cuentas);//obtengo los resultados desde el servicio, enviando las cuentas
            //Reporto los resultados
            Console.WriteLine("Reporte de Transacciones");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Balance Final: {resultado.balanceFinal}");
            Console.WriteLine($"Transacción de Mayor Monto: ID {resultado.idMayor} - {resultado.transaccionMayormonto}");
            Console.WriteLine($"Conteo de Transacciones: Crédito: {resultado.conteoTransaccionesCredito} Débito: {resultado.conteoTransaccionesDebito}");
        }
    }
    else
    {
        Console.WriteLine("El archivo CSV no se ha encontrado. Verifica la ruta del archivo.");
    }
}
//Captura de Excepciones por tipo, más detalle en cada mensaje
catch (HeaderValidationException ex)//problema de cabeceras
{
    Console.WriteLine("Hubo un problema con las cabeceras del CSV. Por favor verifica si coinciden con los campos esperados:");
    Console.WriteLine(ex.Message);
}
catch (FileNotFoundException ex)//no encunetra el archivo
{
    Console.WriteLine("El archivo CSV no se ha encontrado. Verifica la ruta del archivo:");
    Console.WriteLine(ex.Message);
}
catch (IOException ex)//de manejo de archivo
{
    Console.WriteLine("Hubo un problema al leer el archivo. Verifica si el archivo está siendo usado por otra aplicación:");
    Console.WriteLine(ex.Message);
}
catch (Exception ex)//excepcion genérica
{
    Console.WriteLine("Ocurrió un error inesperado. Detalles:");
    Console.WriteLine(ex.Message);
}
