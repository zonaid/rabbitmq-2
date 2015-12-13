USE [RabbitMQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScheduledMessages](
	[ScheduledMessageGuid] [uniqueidentifier] NOT NULL,
	[MessageGuid] [uniqueidentifier] NOT NULL,
	[MessageScheduleGuid] [uniqueidentifier] NOT NULL,
	[ExchangeGuid] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ScheduledMessages] PRIMARY KEY CLUSTERED 
(
	[ScheduledMessageGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ScheduledMessages]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledMessages_Exchanges] FOREIGN KEY([ExchangeGuid])
REFERENCES [dbo].[Exchanges] ([ExchangeGuid])
GO

ALTER TABLE [dbo].[ScheduledMessages] CHECK CONSTRAINT [FK_ScheduledMessages_Exchanges]
GO

ALTER TABLE [dbo].[ScheduledMessages]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledMessages_Messages] FOREIGN KEY([MessageGuid])
REFERENCES [dbo].[Messages] ([MessageGuid])
GO

ALTER TABLE [dbo].[ScheduledMessages] CHECK CONSTRAINT [FK_ScheduledMessages_Messages]
GO

ALTER TABLE [dbo].[ScheduledMessages]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledMessages_MessageSchedules] FOREIGN KEY([MessageScheduleGuid])
REFERENCES [dbo].[MessageSchedules] ([MessageScheduleGuid])
GO

ALTER TABLE [dbo].[ScheduledMessages] CHECK CONSTRAINT [FK_ScheduledMessages_MessageSchedules]
GO


