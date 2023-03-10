USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripeCustomers_Insert]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/8/2022
-- Description:	Insert CustomerId associated with UserId upon checkout
-- Code Reviewer: Justin Wilson

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripeCustomers_Insert]
		@UserId int
		,@CustomerId varchar(50)

		/* Test Code
			declare @UserId int = 77
					,@CustomerId varchar(50) = 'test999999999'
			execute dbo.StripeCustomers_Insert @UserId, @CustomerId
			
			select *
			from dbo.StripeCustomers
		
		*/
as
begin


		INSERT INTO dbo.StripeCustomers
				   (UserId
				   ,CustomerId)
			 VALUES
				   (@UserId
				   ,@CustomerId)


end

GO
