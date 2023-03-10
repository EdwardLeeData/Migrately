USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_Insert]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Insert>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PaymentAccounts_Insert]
			@AccountId nvarchar(250)
			,@PaymentTypeId int
			,@UserId int
			,@Id int output

		/* -- Test Code --
			declare @Id int = 1
					,@AccountId nvarchar = 'a'
					,@PaymentTypeId int = 1
					,@CreatedBy int = 1
			execute dbo.PaymentAccounts_Insert @Id
												,@AccountId
												,@PaymentTypeId
												,@CreatedBy

			select *
			from dbo.PaymentAccounts
		
		*/
as
begin

		INSERT INTO [dbo].[PaymentAccounts]
				   (AccountId
				   ,PaymentTypeId
				   ,CreatedBy)
		VALUES
				(@AccountId
				,@PaymentTypeId
				,@UserId)
		SET @Id = SCOPE_IDENTITY();


end
GO
