USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeSubscriptions_SelectByUserIdV2]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/19/2022
-- Description:	
-- Code Reviewer: 

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeSubscriptions_SelectByUserIdV2]
			@UserId int

			/* TEST CODE
			DECLARE @UserId int = 77

			EXECUTE [dbo].[StripeSubscriptions_SelectByUserIdV2] @UserId
			
			*/
as

begin
			SELECT TOP 1 sub.SubscriptionId
				  ,cust.CustomerId
			  FROM dbo.StripeSubscriptions as sub
			  INNER JOIN dbo.StripeCustomers as cust
			  ON sub.CustomerId = cust.Id
			  ORDER BY sub.Id DESC


end




GO
