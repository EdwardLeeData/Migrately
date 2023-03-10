USE [Migrately]
GO
/****** Object:  Table [dbo].[StripeProducts]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StripeProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ProductId] [varchar](50) NOT NULL,
	[PriceId] [varchar](50) NOT NULL,
	[Amount] [decimal](5, 2) NOT NULL,
	[Term] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StripeProducts_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
