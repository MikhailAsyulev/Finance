
CREATE FUNCTION [dbo].[GetNewAmount] (@DepositId int)
RETURNS money
AS
BEGIN
   DECLARE @NewAmount money = (SELECT TOP 1 Amount FROM DepositPayment where @DepositId = DepositPayment.DepositId ORDER BY Date DESC)
    
   RETURN(@NewAmount);
END;	
GO

CREATE FUNCTION [dbo].[getamount] (@DepositTypeId int, @Balance money)
RETURNS money
AS
BEGIN
   DECLARE @percent float = (SELECT TOP 1 [Percent] FROM DepositType where DepositType.Id = @DepositTypeId)
    
   RETURN((@percent / 12.0) * @Balance);
END; 
GO

CREATE PROCEDURE [dbo].[DepositOvercharge]   
AS 
BEGIN
	DECLARE @date datetime = (SELECT dbo.GetCurrentDate())
	INSERT INTO DepositPayment (DepositId, Amount, [Type], Date) 
	SELECT Deposit.Id, dbo.getamount(Deposit.DepositTypeId, Deposit.Balance), 2, @date from Deposit
	WHERE Deposit.EndDate > @date 
		AND Deposit.Balance > 0 
		AND DATEDIFF(day, Deposit.StartDate ,@date) % 30 = 0

	UPDATE Deposit SET Balance = Balance + dbo.GetNewAmount(Deposit.Id)
	WHERE Deposit.EndDate > @date 
		AND Deposit.Balance > 0
		AND DATEDIFF(day, Deposit.StartDate ,@date) % 30 = 0
END
