USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_SelectAll_Paginated]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Select All Pagination>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[PaymentAccounts_SelectAll_Paginated]
		@PageIndex int
		,@PageSize int

		/* -- Test Code --
		declare @PageIndex int = 0
				,@PageSize int = 5

		execute dbo.PaymentAccounts_SelectAll_Paginated @PageIndex, @PageSize
		*/

as
begin
		DECLARE @offset int = @PageIndex * @PageSize

		SELECT Id
			  ,AccountId
			  ,PaymentTypeId
			  ,DateCreated
			  ,DateModified
			  ,CreatedBy
			  ,ModifiedBy
			  ,TotalCount = Count(1) OVER()
		FROM dbo.PaymentAccounts
		ORDER BY Id

		OFFSET @offset ROWS
		FETCH NEXT @PageSize rows only



end

GO
