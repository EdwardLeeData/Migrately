USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeCustomers_GetById]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/17/2022
-- Description:	Pass UserId as param to obtain existing customer
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeCustomers_GetById]

		@UserId int
		/* Test Code

		Declare @UserId int = 8
		execute dbo.StripeCustomers_GetById @UserId
		
		*/
as
begin

		SELECT UserId
			  ,CustomerId
		  FROM dbo.StripeCustomers
		  WHERE UserId = @UserId


end


GO
