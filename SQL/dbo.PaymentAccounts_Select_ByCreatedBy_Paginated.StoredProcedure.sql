USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_Select_ByCreatedBy_Paginated]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Select by 'CreatedBy' (UserId) Pagination>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[PaymentAccounts_Select_ByCreatedBy_Paginated]
		@CreatedBy int
		,@PageIndex int
		,@PageSize int

		/* -- Test Code --
			declare @CreatedBy int = 1
					,@PageIndex int = 0
					,@PageSize int = 1

			execute [dbo].[PaymentAccounts_Select_ByCreatedBy_Paginated] @CreatedBy
																		,@PageIndex
																		,@PageSize

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
		WHERE CreatedBy = @CreatedBy
		ORDER BY Id

		OFFSET @offset rows
		FETCH NEXT @PageSize rows only


end

GO
