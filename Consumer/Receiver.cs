﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    internal class Receiver
    {
        static void Main(string[] args)
        {
            // Crea la conexion hacia el HostName
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Message Received: {0}", message.ToString());
                };

                channel.BasicConsume("BasicTest", true, consumer);

                Console.WriteLine("Press [enter] to exit the consumer");
            }

            Console.ReadLine();
        }
    }
}
