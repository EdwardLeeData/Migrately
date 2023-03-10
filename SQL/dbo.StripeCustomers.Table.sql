USE [Migrately]
GO
/****** Object:  Table [dbo].[StripeCustomers]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeCustomers](
	[Id] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CustomerId] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StripeCustomers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeCustomers]  WITH CHECK ADD  CONSTRAINT [FK_StripeCustomers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[StripeCustomers] CHECK CONSTRAINT [FK_StripeCustomers_Users]
GO
