USE [RabbitMQ]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Exchanges](
	[ExchangeGuid] [uniqueidentifier] NOT NULL,
	[ExchangeName] [nvarchar](max) NOT NULL,
	[ExchangeType] [nvarchar](max) NOT NULL,
	[Durable] [bit] NOT NULL,
	[AutoDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Exchanges] PRIMARY KEY CLUSTERED 
(
	[ExchangeGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


