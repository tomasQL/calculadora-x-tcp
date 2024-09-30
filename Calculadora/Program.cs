// 1. Menú con selección de opciones como: SUMAR, RESTAR, MULTIPLICAR, DIVIDIR,POTENCIA, SALIR.
// 2. El menú debe permanecer en un ciclo WHILE y debe terminar sólo si el usuario selecciona la
// opción salir.
// 3. Si el usuario selecciona alguna opción que no se encuentre en el menú, el programa debe
// mostrar un mensaje indicando que la opción no es válida.
// 4. Todas las opciones deben ser métodos de una clase llamada calculadora y las variables
// deben ser sus atributos.
// 5. Debe validar en el caso de la división que el divisor no sea cero y volver a solicitar al usuario
// el ingreso de este valor.
// 6. El usuario debe ser capaz de seleccionar cuántos números desea utilizar en las opciones
// SUMAR, RESTAR y MULTIPLICAR.
// 7. Cree el diagrama de clases correspondiente a la aplicación desarrollada.
// 8. Cargue el desarrollo de la aplicación y el diagrama en respuesta a esta actividad.

using System.Diagnostics;
using Calculadora;

class Program
{
    static void Main(string[] args)
    {
        // Crear un hilo para el servidor
        Thread servidorThread = new Thread(() =>
        {
            Console.WriteLine("Consola del Servidor:");
            Servidor.CalculadoraServer server = new Servidor.CalculadoraServer();
            
        });

        // Crear un hilo para el cliente
        Thread clienteThread = new Thread(() =>
        {
            Console.WriteLine("Consola del Cliente:");
            Cliente.ClienteTCP cliente = new Cliente.ClienteTCP();
        });

        // Iniciar ambos hilos
        servidorThread.Start();
        clienteThread.Start();

        // Esperar a que ambos hilos terminen
        servidorThread.Join();
        clienteThread.Join();
    }
}





// Servidor.CalculadoraServer server = new Servidor.CalculadoraServer();
// // Iniciamos el servidor en un Hilo separado
// Thread serverThread = new Thread(server.IniciarServidor);
// serverThread.Start();

// // Espera conexiones durante 60 segundos
// Thread.Sleep(60000);



// Cliente.ClienteTCP cliente = new Cliente.ClienteTCP();
// // Iniciamos el cliente en un Hilo separado
// Thread clienteThread = new Thread(cliente.IniciarCliente);
// clienteThread.Start();

// // El cliente es iniciado con 60 segundos para comenzar a operar con el
// Thread.Sleep(60000);
