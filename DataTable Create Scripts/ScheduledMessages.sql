USE [RabbitMQ]
GO

/****** Object:  Table [dbo].[ScheduledMessages]    Script Date: 12/14/2015 12:47:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ScheduledMessages](
	[ScheduledMessageGuid] [uniqueidentifier] NOT NULL,
	[MessageGuid] [uniqueidentifier] NOT NULL,
	[ExchangeGuid] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[CronExpression] [varchar](50) NOT NULL,
	[EffectiveDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_ScheduledMessages] PRIMARY KEY CLUSTERED 
(
	[ScheduledMessageGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
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


