USE [Migrately]
GO
/****** Object:  Table [dbo].[StripeProductSubscription]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeProductSubscription](
	[ProductId] [int] NOT NULL,
	[SubscriptionId] [int] NOT NULL,
 CONSTRAINT [PK_StripeProductSubscription] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[SubscriptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeProductSubscription]  WITH CHECK ADD  CONSTRAINT [FK_StripeProductSubscription_StripeProductSubscription] FOREIGN KEY([ProductId])
REFERENCES [dbo].[StripeProducts] ([Id])
GO
ALTER TABLE [dbo].[StripeProductSubscription] CHECK CONSTRAINT [FK_StripeProductSubscription_StripeProductSubscription]
GO
ALTER TABLE [dbo].[StripeProductSubscription]  WITH CHECK ADD  CONSTRAINT [FK_StripeProductSubscription_StripeSubscriptions] FOREIGN KEY([SubscriptionId])
REFERENCES [dbo].[StripeSubscriptions] ([Id])
GO
ALTER TABLE [dbo].[StripeProductSubscription] CHECK CONSTRAINT [FK_StripeProductSubscription_StripeSubscriptions]
GO
