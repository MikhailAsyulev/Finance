CREATE FUNCTION [dbo].[GetCreditPaymentAmount] (@CreditTypeId int, @MainDebt money)
RETURNS money
AS
BEGIN
   DECLARE @percent float = (SELECT TOP 1 [Percent] FROM CreditType where CreditType.Id = @CreditTypeId)
    
   RETURN((POWER(@percent + 1.0, 1.0/12.0) - 1) * @MainDebt);
END;
GO

CREATE FUNCTION [dbo].[GetCreditOverdueAmount] (@CreditTypeId int, @MainDebt money)
RETURNS money
AS
BEGIN
   DECLARE @percent float = (SELECT TOP 1 [OverduePercent] FROM CreditType where CreditType.Id = @CreditTypeId)
    
   RETURN((POWER(@percent + 1.0, 1.0/12.0) - 1) * @MainDebt);
END;
GO

CREATE FUNCTION [dbo].[GetNewCreditPaymentAmount] (@CreditId int)
RETURNS money
AS
BEGIN
   DECLARE @NewAmount money = (SELECT TOP 1 PercentsAmount FROM CreditPayment where @CreditId = CreditPayment.CreditId ORDER BY Date DESC)
    
   RETURN(@NewAmount);
END;
GO

CREATE PROCEDURE [dbo].[CreditOvercharge]
AS 
BEGIN
	DECLARE @date datetime = (SELECT dbo.GetCurrentDate())
	INSERT INTO CreditPayment (CreditId, PercentsAmount, MainAmount, [Type], Date) 
	SELECT Credit.Id, dbo.GetCreditPaymentAmount(Credit.CreditTypeId, Credit.MainDebt), 0, 1, @date from Credit
	WHERE Credit.MainDebt > 0
		AND Credit.EndDate > @date
		AND DATEDIFF(day, Credit.StartDate ,@date) % 30 = 0
		AND Credit.IsClosed = 0
	
	UPDATE Credit SET PercentageDebt = PercentageDebt + dbo.GetNewCreditPaymentAmount(Credit.Id)
	WHERE Credit.MainDebt > 0
		AND Credit.EndDate > @date
		AND DATEDIFF(day, Credit.StartDate ,@date) % 30 = 0
		AND Credit.IsClosed = 0

	--Set overdue percents
	INSERT INTO CreditPayment (CreditId, PercentsAmount, MainAmount, [Type], Date) 
	SELECT Credit.Id, dbo.GetCreditOverdueAmount(Credit.CreditTypeId, Credit.MainDebt), 0, 1, @date from Credit
	WHERE Credit.MainDebt > 0
		AND Credit.EndDate < @date
		AND DATEDIFF(day, Credit.StartDate ,@date) % 30 = 0
		AND Credit.IsClosed = 0
	
	UPDATE Credit SET PercentageDebt = PercentageDebt + dbo.GetNewCreditPaymentAmount(Credit.Id)
	WHERE Credit.MainDebt > 0
		AND Credit.EndDate < @date
		AND DATEDIFF(day, Credit.StartDate ,@date) % 30 = 0
		AND Credit.IsClosed = 0
END