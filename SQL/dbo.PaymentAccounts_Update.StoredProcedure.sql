USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_Update]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Update>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[PaymentAccounts_Update]
		@AccountId nvarchar(250)
		,@PaymentTypeId int
		,@UserId int
		,@Id int

		/* -- Test Code --
			DECLARE @AccountId nvarchar(250) = 'a'
					,@PaymentTypeId int = 1
					,@ModifiedBy int = 1
					,@Id int = 1
			EXECUTE dbo.PaymentAccounts_Update @AccountId
												,@PaymentTypeId
												,@ModifiedBy
												,@Id
		*/
as
begin


		UPDATE dbo.PaymentAccounts
		   SET AccountId = @AccountId
			  ,PaymentTypeId = @PaymentTypeId
			  ,DateModified = GETUTCDATE()
			  ,ModifiedBy = @UserId
		 WHERE Id = @Id


end

GO
