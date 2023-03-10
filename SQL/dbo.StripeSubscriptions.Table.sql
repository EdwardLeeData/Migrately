USE [Migrately]
GO
/****** Object:  Table [dbo].[StripeSubscriptions]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeSubscriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionId] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[CustomerId] [varchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateExpire] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StripeSubscriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeSubscriptions]  WITH CHECK ADD  CONSTRAINT [FK_StripeSubscriptions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[StripeSubscriptions] CHECK CONSTRAINT [FK_StripeSubscriptions_Users]
GO
