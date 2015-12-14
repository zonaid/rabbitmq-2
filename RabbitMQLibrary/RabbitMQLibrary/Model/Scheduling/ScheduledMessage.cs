using System;
using System.Collections.Generic;
using RabbitMQLibrary.Model.Messaging;

namespace RabbitMQLibrary.Model.Scheduling
{
    public class ScheduledMessage
    {
        public Guid ScheduledMessageGuid { get; set; }
        public bool Active { get; set; }
        public Message Message { get; set; }
        public string CronExpression { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
        public IList<MessageRun> MessageRuns { get; set; }
    }
}
