using System;
using System.Collections.Generic;

namespace RabbitMQLibrary.Model.Scheduling
{
    public class MessageRun
    {
        public Guid MessageRunGuid { get; set; }
        public Guid ScheduledMessageGuid { get; set; }
        public DateTimeOffset? SendAttemptTime { get; set; }
        public bool SentSuccessfully { get; set; }
        public MessageRunError MessageRunError { get; set; }
    }
}
