using System.Configuration;

namespace RabbitMQLibrary
{
    public static class RabbitRun
    {
        public static void Main()
        {
            while(true)
            {
                var scheduledMessagesWorker = new ScheduledMessagesWorker("localhost",
                                                                          new MessageRunLogger(ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString),
                                                                          new ScheduledMessagesRetriever(ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString));

                scheduledMessagesWorker.ScheduleRabbitMQMessages();

                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
