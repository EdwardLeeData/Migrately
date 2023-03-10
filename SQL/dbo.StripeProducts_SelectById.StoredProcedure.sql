USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeProducts_SelectById]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/15/2022
-- Description:	Select plan by Id
-- Code Reviewer: Kenny Rosa

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeProducts_SelectById]
			@Id int

		/* Test Code
			Declare @Id int = 1
			execute [dbo].[StripeProducts_SelectById] @Id
		*/
as
begin

		SELECT Id
			  ,[Name]
			  ,ProductId
			  ,PriceId
			  ,Amount
			  ,Term
		  FROM dbo.StripeProducts
		  WHERE Id = @Id;


end


GO
