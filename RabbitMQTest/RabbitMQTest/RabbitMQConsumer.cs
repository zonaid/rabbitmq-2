using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace RabbitMQTest
{
    public abstract class RabbitMQConsumer
    {
        public string Host { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string BindingKey { get; set; }
        public string QueueName { get; set; }
        
        public RabbitMQConsumer(string host,
                                string exchangeName,
                                string exchangeType,
                                string bindingKey,
                                string queueName = null)
        {
            Host = host;
            ExchangeName = exchangeName;
            ExchangeType = exchangeType;
            BindingKey = bindingKey;
            QueueName = queueName;
        }

        public void Consume()
        {
            var factory = new ConnectionFactory() { HostName = Host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType);

                //if queue name not supplied use temp queue
                if (String.IsNullOrEmpty(QueueName))
                {
                    var tempQueueName = channel.QueueDeclare();
                    channel.QueueBind(tempQueueName, ExchangeName, BindingKey);
                }
                else
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    channel.QueueBind(QueueName, ExchangeName, BindingKey);
                }

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);

                    Work(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(QueueName, false, consumer);

                Console.WriteLine("Listening...");
                Console.ReadLine();
            }
        }

        protected abstract void Work(string message);
    }
}
