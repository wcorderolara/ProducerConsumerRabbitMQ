using RabbitMQ.Client;
using System;
using System.Text;
 
namespace Producer
{
    internal class Sender
    {
        public static void Main(string[] args)
        {
            // Crea la conexion hacia el HostName
            var factory = new ConnectionFactory() { HostName = "localhost" };

            for(int i = 0; i < 5; i++)
            {
                // abre el canal para la comunicacion con RabbitMQ
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    // Creacion del Exchange
                    channel.ExchangeDeclare("BasicExchange", ExchangeType.Direct);
                    // Creacion de la Queue
                    channel.QueueDeclare("BasicTest", false, false, false, null);
                    // Creacion del Binding y routing Key
                    channel.QueueBind("BasicTest", "BasicExchange", "BasicRoutingKey", null);

                    // Creacion del message
                    string message = "Getting started with .Net Core and RabbitMQ";
                    var body = Encoding.UTF8.GetBytes(message);

                    // Publicacion del message
                    channel.BasicPublish("BasicExchange", "BasicRoutingKey", null, body);
                    Console.WriteLine("Sent message {0}...", message);

                }
            }
            

            Console.WriteLine("Press enter to exit the Sender App...");
            Console.ReadLine();
        }
    }
}
