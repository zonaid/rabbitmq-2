using RabbitMQLibrary.Model.Scheduling;

namespace RabbitMQLibrary.Interfaces
{
    public interface IMessageRunLogger
    {
        void LogMessageRun(MessageRun messageRun);
    }
}
