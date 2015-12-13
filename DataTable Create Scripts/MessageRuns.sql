USE [RabbitMQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MessageRuns](
	[MessageRunGuid] [uniqueidentifier] NOT NULL,
	[ScheduledMessageGuid] [uniqueidentifier] NOT NULL,
	[SendAttemptTime] [datetimeoffset](7) NOT NULL,
	[SentSuccessfully] [bit] NOT NULL,
 CONSTRAINT [PK_MessageRuns] PRIMARY KEY CLUSTERED 
(
	[MessageRunGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MessageRuns]  WITH CHECK ADD  CONSTRAINT [FK_MessageRuns_ScheduledMessages] FOREIGN KEY([ScheduledMessageGuid])
REFERENCES [dbo].[ScheduledMessages] ([ScheduledMessageGuid])
GO

ALTER TABLE [dbo].[MessageRuns] CHECK CONSTRAINT [FK_MessageRuns_ScheduledMessages]
GO


