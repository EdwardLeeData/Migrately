﻿USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeSubscriptions_SelectByUserId]    Script Date: 12/17/2022 5:25:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/16/2022
-- Description:	Grab Product name when passed a UserId as a parameter. Used in ReactJs to indicate what plan customer is already subscribed to.
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeSubscriptions_SelectByUserId]
			@UserId int

			/* Test Code
			Declare @UserId int = 77

			execute [dbo].[StripeSubscriptions_SelectByUserId] @UserId
			
			
			
			*/
as

begin


		SELECT TOP 1 prod.Id
				,prod.Name
		  FROM dbo.StripeSubscriptions as sub
		  INNER JOIN dbo.StripeProductSubscription as pro
		  ON sub.Id = pro.SubscriptionId
		  INNER JOIN dbo.StripeProducts as prod
		  ON pro.ProductId = prod.Id
		  WHERE sub.UserId = @UserId
		  ORDER BY prod.Id DESC


		  


end



GO
