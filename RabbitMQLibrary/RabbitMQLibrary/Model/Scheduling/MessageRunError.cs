using System;

namespace RabbitMQLibrary.Model.Scheduling
{
    public class MessageRunError
    {
        public Guid MessageRunGuid { get; set; }
        public string Error { get; set; }
    }
}
