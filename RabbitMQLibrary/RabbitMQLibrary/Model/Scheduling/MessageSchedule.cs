using System;

namespace RabbitMQLibrary.Model.Scheduling
{
    public class MessageSchedule
    {
        public string CronExpression { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }
    }
}
