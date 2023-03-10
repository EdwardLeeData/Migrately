USE [Migrately]
GO
/****** Object:  Table [dbo].[StripeInvoices]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeInvoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[InvoiceStart] [datetime2](7) NOT NULL,
	[InvoiceEnd] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StripeInvoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StripeInvoices]  WITH CHECK ADD  CONSTRAINT [FK_StripeInvoices_StripeInvoices] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[StripeCustomers] ([Id])
GO
ALTER TABLE [dbo].[StripeInvoices] CHECK CONSTRAINT [FK_StripeInvoices_StripeInvoices]
GO
