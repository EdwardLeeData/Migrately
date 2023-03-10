USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_Select_ById]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Select By Id>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PaymentAccounts_Select_ById]
		@Id int
	/* -- Test Code --
	
		declare @Id int = 1
		execute dbo.PaymentAccounts_Select_ById @Id
	
	*/
as
begin

		SELECT Id
			  ,AccountId
			  ,PaymentTypeId
			  ,DateCreated
			  ,DateModified
			  ,CreatedBy
			  ,ModifiedBy
		FROM dbo.PaymentAccounts
		WHERE Id = @Id

end
GO
