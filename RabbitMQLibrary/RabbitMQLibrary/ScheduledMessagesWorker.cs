using System;
using System.Linq;
using RabbitMQLibrary.Interfaces;
using RabbitMQLibrary.Model.Scheduling;

namespace RabbitMQLibrary
{
    public class ScheduledMessagesWorker
    {
        private string _host;

        private IMessageRunLogger _messageRunLogger;

        private IScheduledMessagesRetriever _scheduledMessagesRetriever;

        public ScheduledMessagesWorker(string rabbitMQHost,
                                       IMessageRunLogger messageRunLogger,
                                       IScheduledMessagesRetriever scheduledMessagesRetriever)
        {
            _host = rabbitMQHost;
            _messageRunLogger = messageRunLogger;
            _scheduledMessagesRetriever = scheduledMessagesRetriever;
        }

        public void ScheduleRabbitMQMessages()
        {
            foreach(var scheduledMessage in _scheduledMessagesRetriever.GetScheduledMessages(true))
            {
                if(!scheduledMessage.MessageRuns.Any(mr => !mr.SendAttemptTime.HasValue))
                {
                    DateTimeOffset lastRunTime;
                    
                    if (scheduledMessage.MessageRuns.Any())
                    {
                        lastRunTime = scheduledMessage.MessageRuns.Max(mr => mr.SendAttemptTime.Value);
                    }
                    else
                    {
                        lastRunTime = scheduledMessage.MessageSchedule.EffectiveDate;
                    }

                    var cronExpression = new Quartz.CronExpression(scheduledMessage.MessageSchedule.CronExpression);
                    cronExpression.TimeZone = TimeZoneInfo.Utc;
                    var nextRunTime = cronExpression.GetTimeAfter(lastRunTime);

                    if (nextRunTime <= DateTimeOffset.UtcNow)
                    {
                        var messageRun = SendMessage(scheduledMessage);

                        _messageRunLogger.LogMessageRun(messageRun);
                    }
                }
            }
        }

        private MessageRun SendMessage(ScheduledMessage scheduledMessage)
        {
            var messageRun = new MessageRun()
            {
                MessageRunGuid = Guid.NewGuid(),
                SentSuccessfully = true,
                SendAttemptTime = DateTime.UtcNow,
                ScheduledMessageGuid = scheduledMessage.ScheduledMessageGuid
            };
                    
            try
            {
                var publisher = new MessagePublisher(_host, scheduledMessage.Message.Exchange);

                publisher.Publish(scheduledMessage.Message.Body,
                                  scheduledMessage.Message.RoutingKey,
                                  scheduledMessage.Message.Headers);
            }
            catch(Exception ex)
            {
                messageRun.SentSuccessfully = false;
                messageRun.MessageRunError = new MessageRunError()
                {
                    Error = ex.ToString(),
                    MessageRunGuid = messageRun.MessageRunGuid
                };
            }

            return messageRun;
        }

    }
}
