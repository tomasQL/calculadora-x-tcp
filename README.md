# Calculadora x TCP/IP

Aplicación desarrollada para la asignatura **Programación Avanzada de Aplicaciones**.  
Evaluación 3/4, Nota: 6.7  

## Contexto
Durante la asignatura se nos encargó la tarea de comprender los aspectos básicos de la programación  
en C#, junto con el desarrollo de una aplicación sencilla tipo calculadora de operaciones  
aritméticas básicas entre 2 variables, esto es SUMA, RESTA, MULTIPLICACIÓN, DIVISIÓN Y POTENCIACIÓN.  
Los detalles de implementación quedaban a decisión del estudiante.  

## Over-engineering
Parecía una tarea sencilla, por lo que arbitrariamente decidí añadir una capa extra al  
desafío, crear una aplicación tipo cliente/servidor que enviara y recibiera las operaciones  
para finalmente devolver los resultados respectivamente.  

**Modificaciones pendientes.**  
Si bien originalmente quería matar un insecto con un helicóptero Apache AH64  
(Problema - Solución), estoy en vías de reducir el uso de recursos de esta pequeña  
aplicación y mejorar el uso de conceptos de la orientación a objetos.  
Sí, a Grady Booch le daría un infarto...  

### Detalles de implementación

## Servidor:

Clase Calculadora: contiene la definición de los métodos para cada operación  
aritmética específica, recibiendo los 2 números y operando sobre ellos  
devolviendo su resultado.  
 
Clase Operaciones: permite llevar un registro de objetos de la calculadora.  

CalculadoraServer():  
1. Crea un objeto calculadora.  
2. Crea una lista de operaciones.  
IniciarServidor():  
Define las propiedades necesarias para crear un objeto TCPListener donde  
encapsularemos la funcionalidad del "servicio", mediante un stream de datos  
en formato de bytes mediante un array de 256 bytes.

La codificación elegida es ASCII por la utilización de símbolos y números.  
Cuando iniciamos la "escucha" del Stream de datos, entramos en varios ciclos  
de evaluación (while's e if's) con captura de errores mediante try-catch.  
1. Recibimos y codificamos el stream en una variable llamada "datos".  
2. Datos es dividida en partes según el uso del símbolo espacio para identificar  
   Operador1 Operando Operador2.  
3. Traducimos el string a los tipos correspondientes (Double-Numeric) 
4. Utilizamos un Switch para manejar el caso específico de cada operador.  
5. Invocamos en cada case statement el método correspondiente al operador  
   devolviendo el resultado y saliendo del switch mediante un break.  
6. Registramos la "operación" completa mediante la lista definida de Operaciones.  
7. El resultado es codificado y como stream de datos por el servidor.  
8. Se cierra la conexión.  

## Cliente:  

*Nunca tiene la razón*
