USE [Migrately]
GO
/****** Object:  Table [dbo].[StripePaymentIntents]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripePaymentIntents](
	[AccountId] [varchar](50) NOT NULL,
	[PaymentIntentId] [varchar](50) NOT NULL,
	[ChargeId] [varchar](50) NOT NULL,
	[TransferId] [varchar](50) NOT NULL,
	[Amount] [decimal](5, 2) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
