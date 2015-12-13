using System.Data.SqlClient;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Model.Scheduling;
using RabbitMQLibrary.Queries;
using Dapper;

namespace RabbitMQLibrary
{
    public class MessageRunLogger : IMessageRunLogger
    {
        private string _connectionString;

        public MessageRunLogger(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void LogMessageRun(MessageRun messageRun)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var parameters = new
                {
                    MessageRunGuid = messageRun.MessageRunGuid,
                    ScheduledMessageGuid = messageRun.ScheduledMessageGuid,
                    SendAttemptTime = messageRun.SendAttemptTime,
                    SendSuccessfully = messageRun.SentSuccessfully
                };

                conn.Execute(SchedulingQueries.InsertMessageRun, parameters);

                if (messageRun.MessageRunError != null)
                {
                    var errorParameters = new
                    {
                        MessageRunGuid = messageRun.MessageRunGuid,
                        Error = messageRun.MessageRunError.Error
                    };

                    conn.Execute(SchedulingQueries.InsertMessageRunError, errorParameters);
                }
            }
        }
    }
}
