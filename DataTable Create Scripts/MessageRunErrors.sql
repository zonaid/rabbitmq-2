USE [RabbitMQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MessageRunErrors](
	[MessageRunGuid] [uniqueidentifier] NOT NULL,
	[Error] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[MessageRunErrors]  WITH CHECK ADD  CONSTRAINT [FK_MessageRunErrors_MessageRuns] FOREIGN KEY([MessageRunGuid])
REFERENCES [dbo].[MessageRuns] ([MessageRunGuid])
GO

ALTER TABLE [dbo].[MessageRunErrors] CHECK CONSTRAINT [FK_MessageRunErrors_MessageRuns]
GO


