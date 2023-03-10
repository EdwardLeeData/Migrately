USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeInvoices_SelectById]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/26/2022
-- Description:	Return Invoice Start and End date passing Id as parameter
-- Code Reviewer: Christ Bast

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeInvoices_SelectById]
			@UserId int

			/* Test Code
			
			DECLARE @UserId int = 77
			EXECUTE [dbo].[StripeInvoices_SelectById] @UserId
			
			*/

as
begin
		
			SELECT TOP 1 InvoiceStart
						  ,InvoiceEnd 
			  FROM dbo.StripeInvoices
			  WHERE CustomerId IN ( SELECT Id
							FROM dbo.StripeCustomers
							WHERE UserId = @UserId)
			 ORDER BY Id DESC



end




GO
