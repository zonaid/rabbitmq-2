using System.Text;
using RabbitMQ.Client;

namespace RabbitMQTest
{
    public class RabbitMQPublisher
    {
        public string Host { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }

        public RabbitMQPublisher(string host, string exchangeName, string exchangeType)
        {
            Host = host;
            ExchangeName = exchangeName;
            ExchangeType = exchangeType;
        }

        public void Publish(string message, string routingKey)
        {
            var factory = new ConnectionFactory() { HostName = Host };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType);

                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(ExchangeName, routingKey, properties, body);
            };
        }
    }
}
