using System;
using System.Collections.Generic;

namespace RabbitMQLibrary.Model.Messaging
{
    public class Exchange
    {
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}
