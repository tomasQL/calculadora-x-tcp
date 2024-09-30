using System.Net.Sockets;
using System.Text;
namespace Calculadora;

public class Cliente
{
    public class ClienteTCP
    {
        private Cliente cliente;
         public void IniciarCliente(){
            try
            {
                string IPServidor = "127.0.0.1";
                int puerto = 13000;
                while (true)
                {     
                    Console.WriteLine("Cliente Iniciado...");
                    Console.WriteLine("Ingrese una operación aritmética (SUMA, RESTA, DIVISION, MULTIPLICACIÓN O POTENCIA)");
                    Console.WriteLine("el formato debe ser <numero> <operador> <numero>");
                    Console.WriteLine("Para la potencia se utiliza ** ");
                
                    string input = Console.ReadLine();

                    if (input.ToLower() == "salir")
                        break;
                
                    // Creamos un cliente TCP
                    using (TcpClient cliente = new TcpClient(IPServidor, puerto))
                    {
                        using (NetworkStream stream = cliente.GetStream())
                        {
                            // Creamos un array de datos en el que codificamos
                            // los datos ingresados por el usuario y los pasamos
                            // al encoder
                            byte[] datos = Encoding.ASCII.GetBytes(input);
                            
                            // Enviamos los datos al servidor
                            stream.Write(datos, 0, datos.Length);
                            Console.WriteLine("Operación enviada: {0}", input);
                            
                            // Buffer para almacenar los datos recibidos
                            datos = new byte[256];
                            
                            // Lee la respuesta del servidor
                            int bytes = stream.Read(datos, 0, datos.Length);
                            string datosRespuesta = Encoding.ASCII.GetString(datos, 0, bytes);
                            Console.WriteLine("Resultado:  {0}", datosRespuesta);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Console.WriteLine("Presiona cualquier tecla para salir ...");
            Console.ReadKey();
        }
    }
}

   
