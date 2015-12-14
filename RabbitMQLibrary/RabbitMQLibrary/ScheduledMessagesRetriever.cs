using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Model.Messaging;
using RabbitMQLibrary.Queries;
using RabbitMQLibrary.Model.Scheduling;


namespace RabbitMQLibrary
{
    public class ScheduledMessagesRetriever : IScheduledMessagesRetriever
    {
        public string _connectionString { get; set; }

        public ScheduledMessagesRetriever(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<ScheduledMessage> GetScheduledMessages(bool? active = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var allScheduledMessages = conn.Query<ScheduledMessage, Message, Exchange, ScheduledMessage>
                (SchedulingQueries.GetScheduledMessages,
                (scheduledMessage, message, exchange) =>
                {
                    scheduledMessage.Message = message;
                    scheduledMessage.Message.Exchange = exchange;
                    return scheduledMessage;

                },
                splitOn: SchedulingQueries.GetScheduledMessagesSplitOn
                )
                .ToList();

                List<ScheduledMessage> scheduledMessages;

                if(active != null)
                {
                    scheduledMessages = allScheduledMessages.Where(m => m.Active == active).ToList();
                }
                else
                {
                    scheduledMessages = allScheduledMessages;
                }

                foreach(var scheduledMessage in scheduledMessages)
                {
                    scheduledMessage.MessageRuns = conn.Query<MessageRun>
                        (SchedulingQueries.GetMessageRuns, new { ScheduledMessageGuid = scheduledMessage.ScheduledMessageGuid }).ToList();
                }

                return scheduledMessages;
            }
        }
    }
}
