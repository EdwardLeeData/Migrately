USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeInvoices_Insert]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/26/2022
-- Description:	Insert Invoice period into table
-- Code Reviewer: Christ Bast

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[StripeInvoices_Insert]
			@CustomerId varchar(50)
			,@InvoiceStart datetime2(7)
			,@InvoiceEnd datetime2(7)
			,@Id int output

			/* TEST CODE
			DECLARE @CustomerId varchar(50) = 'cus_N0opSiK8b3yg7m'
					,@InvoiceStart datetime2(7) = '12/26/2022'
					,@InvoiceEnd datetime2(7) = '12/26/2023'
					,@Id int = 0

			EXECUTE [dbo].[StripeInvoices_Insert] @CustomerId
													,@InvoiceStart
													,@InvoiceEnd
													,@Id

			*/

as
begin

			INSERT INTO dbo.StripeInvoices
					(
						CustomerId
						,InvoiceStart
						,InvoiceEnd
					)
			SELECT c.Id
					,@InvoiceStart
					,@InvoiceEnd

			FROM dbo.StripeCustomers as c
			WHERE CustomerId = @CustomerId

			SET @Id = SCOPE_IDENTITY();

end


GO
