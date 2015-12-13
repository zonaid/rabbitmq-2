using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQLibrary.Model.Messaging;

namespace RabbitMQLibrary
{
    public class MessagePublisher
    {
        private string _host;

        private Exchange _exchange;

        public MessagePublisher(string host, Exchange exchange)
        {
            _host = host;
            _exchange = exchange;
        }

        public void Publish(string body, string routingKey, IDictionary<string, object> headers = null)
        {
            var factory = new ConnectionFactory() { HostName = _host };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(_exchange.ExchangeName, _exchange.ExchangeType, _exchange.Durable);

                var bodyBytes = Encoding.UTF8.GetBytes(body);

                var properties = channel.CreateBasicProperties();

                properties.Persistent = true;

                if (headers != null)
                {
                    properties.Headers = headers;
                }

                channel.BasicPublish(_exchange.ExchangeName, routingKey, properties, bodyBytes);
            };
        }
    }
}
