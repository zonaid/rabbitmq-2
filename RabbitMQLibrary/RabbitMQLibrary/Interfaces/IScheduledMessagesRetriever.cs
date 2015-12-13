using System.Collections.Generic;
using RabbitMQLibrary.Model.Scheduling;

namespace RabbitMQLibrary.Interfaces
{
    public interface IScheduledMessagesRetriever
    {
        List<ScheduledMessage> GetScheduledMessages(bool? active);
    }
}
