using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQLibrary.Model.Messaging;
using System;

namespace RabbitMQLibrary
{
    public abstract class MessageConsumer
    {
        public string Host { get; set; }
        public Exchange Exchange { get; set; }
        public string BindingKey { get; set; }
        public Queue Queue { get; set; }
        
        public MessageConsumer(string host,
                               Exchange exchange,
                               string bindingKey,
                               Queue queue = null)
        {
            Host = host;
            Exchange = exchange;
            BindingKey = bindingKey;
            Queue = queue;
        }

        public void Consume()
        {
            var factory = new ConnectionFactory() { HostName = Host };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(Exchange.ExchangeName, Exchange.ExchangeType);

                //if queue not supplied use temp queue - for use in pub/sub situations.
                if (Queue == null)
                {
                    var tempQueueName = channel.QueueDeclare();
                    channel.QueueBind(tempQueueName, Exchange.ExchangeName, BindingKey);
                }
                else
                {
                    channel.QueueDeclare(Queue.QueueName,
                                         Queue.Durable,
                                         Queue.Exclusive,
                                         Queue.AutoDelete,
                                         Queue.Arguments);

                    channel.QueueBind(Queue.QueueName, Exchange.ExchangeName, BindingKey);
                }

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);

                    Work(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(Queue.QueueName, false, consumer);

                Console.WriteLine("Listening...");
                Console.ReadLine();
            }
        }

        protected abstract void Work(string message);
    }
}
