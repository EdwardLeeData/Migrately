USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentAccounts_Delete_ById]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Delete by Id>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================

CREATE proc [dbo].[PaymentAccounts_Delete_ById]
		@Id int
as
begin
		DELETE FROM [dbo].[PaymentAccounts]
		WHERE Id = @Id
end

GO
