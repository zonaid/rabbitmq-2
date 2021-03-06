﻿using System;
using RabbitMQ.Client;

namespace RabbitMQLibrary
{
    public static class ConfigureTestServer
    {
        //NOTE you can just do this in the web client if you like.
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("master_exchange", "topic");

                channel.QueueDeclare("email", true, false, false, null);

                channel.QueueBind("email", "master_exchange", "email");

                channel.QueueDeclare("log", true, false, false, null);

                channel.QueueBind("log", "master_exchange", "log");

                Console.WriteLine("master_exchange built, log and email queues declared and bound. Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
