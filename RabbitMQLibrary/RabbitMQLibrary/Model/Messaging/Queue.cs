using System;
using System.Collections.Generic;

namespace RabbitMQLibrary.Model.Messaging
{
    public class Queue
    {
        public string QueueName { get; set; }
        public bool Exclusive { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}
