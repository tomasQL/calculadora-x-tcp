using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Calculadora;

public class Servidor
{
    // Clase Calculadora encargada solo de manejar las
    // operaciones aritméticas
    public class Calculadora
    {
        // Usamos expresiones lambda ya que las operaciones
        // aritméticas son simples
        
        public double Sumar(double numero1, double numero2) => numero1 + numero2;
        public double Restar(double numero1, double numero2) => numero1 - numero2;
        public double Multiplicar(double numero1, double numero2) => numero1 * numero2;
        public double Division(double numero1, double numero2)
        {
            if (numero2 == 0)
                throw new DivideByZeroException("No se puede dividir por cero");
            return numero1 / numero2;
        }
        
        // Solo para la potencia usamos la libreria Math debido al manejo de excepciones
        public double Potencia(double numero1, double numero2) => Math.Pow(numero1, numero2);
    }

    public class Operaciones
    {
        public double Operable1 { get; set; }
        public double Operable2 { get; set; }
        public string Operador { get; set; }
    }

    public class CalculadoraServer
    {
        private Calculadora calculadora;
        private List<Operaciones> historialOperaciones;

        public CalculadoraServer()
        {
            calculadora = new Calculadora();
            historialOperaciones = new List<Operaciones>();
        }

        public void IniciarServidor()
        {
            // Se declara server null ya que aún no se inicializa
            // con objetos válidos
            TcpListener server = null;
            try
            {
                Int32 puerto = 8080;
                IPAddress direccionLocal = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(direccionLocal, puerto);
                server.Start();

                while (true)
                {
                    Console.WriteLine("Iniciando servidor...");
                    Console.WriteLine("Esperando una conexión");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Conectado exitosamente!");

                    // Una vez conectados creamos un stream para enviar
                    // y recibir datos entre cliente servidor.
                    NetworkStream stream = client.GetStream();

                    // Array de datos tipo byte para enviar por el stream
                    byte[] bytes = new byte[256];
                    string datos = null;
                    int indice = 0;

                    while ((indice = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // codificamos los datos en ASCII ya que solo usaremos numeros 
                        // y operaciones sin complejidad de caracteres
                        datos = Encoding.ASCII.GetString(bytes, 0, indice);
                        Console.WriteLine("Datos recibidos: {0}", datos);

                        // lo dividimos en partes ya que el uso del programa
                        // se limita a 3 variables que hay que entregar, Operables y 
                        // la operación
                        string[] partes = datos.Split(' ');
                        if (partes.Length == 3)
                        {
                            double numero1 = double.Parse(partes[0]);
                            double numero2 = double.Parse(partes[2]);
                            string operador = partes[1];

                            double resultado = 0;
                            switch (operador)
                            {
                                case "+":
                                    resultado = calculadora.Sumar(numero1, numero2);
                                    break;
                                case "-":
                                    resultado = calculadora.Restar(numero1, numero2);
                                    break;
                                case "*":
                                    resultado = calculadora.Multiplicar(numero1, numero2);
                                    break;
                                case "/":
                                    resultado = calculadora.Division(numero1, numero2);
                                    break;
                                case "**":
                                    resultado = calculadora.Potencia(numero1, numero2);
                                    break;
                            }

                            historialOperaciones.Add(new Operaciones
                                { Operable1 = numero1, Operable2 = numero2, Operador = operador });
                            byte[] mensaje = Encoding.ASCII.GetBytes(resultado.ToString());
                            stream.Write(mensaje, 0, mensaje.Length);
                            Console.WriteLine("El resultado es {0}", resultado);
                        }
                        else
                        {
                            string mensajeError = "El formato es incorrecto, ingrese los 3 elementos";
                            byte[] mensajeErrorBytes = Encoding.ASCII.GetBytes(mensajeError);
                            stream.Write(mensajeErrorBytes, 0, mensajeErrorBytes.Length);
                        }
                    }

                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server?.Stop();
            }
        }
    }
}
