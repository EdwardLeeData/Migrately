USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[PaymentTypes_SelectAll]    Script Date: 12/28/2022 4:19:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	<Jaeyun Lee>
-- Create date: <11/21/2022>
-- Description:	<Select All>
-- Code Reviewer: Juan Pereida

-- MODIFIED BY: author
-- MODIFIED DATE:12/1/2020
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[PaymentTypes_SelectAll]

	/*		 -- Test Code --
			execute dbo.PaymentTypes_SelectAll
	*/

as	

begin

	SELECT Id
		  ,PaymentType
	FROM dbo.PaymentTypes

end



GO
