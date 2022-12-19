﻿USE [Migrately]
GO
/****** Object:  StoredProcedure [dbo].[StripePaymentIntents_Insert]    Script Date: 12/17/2022 5:25:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jaeyun Lee
-- Create date: 12/12/2022
-- Description:	Insert into StripePaymentIntents table
-- Code Reviewer: Kenny Rosa

-- MODIFIED BY: 
-- MODIFIED DATE:
-- Code Reviewer:
-- Note:
-- =============================================
CREATE proc [dbo].[StripePaymentIntents_Insert]
				@AccountId varchar(50)
				,@PaymentIntentId varchar(50)
				,@ChargeId varchar (50)
				,@TransferId varchar(50)
				,@Amount int
				,@DateCreated datetime2(7)
as
begin



		INSERT INTO dbo.StripePaymentIntents
				   (AccountId
				   ,PaymentIntentId
				   ,ChargeId
				   ,TransferId
				   ,Amount
				   ,DateCreated)
			 VALUES
				   (@AccountId
				   ,@PaymentIntentId
				   ,@ChargeId
				   ,@TransferId
				   ,@Amount
				   ,@DateCreated)






end
GO
