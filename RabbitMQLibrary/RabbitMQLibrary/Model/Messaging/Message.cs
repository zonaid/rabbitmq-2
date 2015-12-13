using System.Collections.Generic;

namespace RabbitMQLibrary.Model.Messaging
{
    public class Message
    {
        public string RoutingKey { get; set; }
        public string Body { get; set; }
        public IDictionary<string, object> Headers { get; set; }
        public Exchange Exchange { get; set; }
    }
}
