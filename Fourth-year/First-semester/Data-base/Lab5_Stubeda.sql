USE [AdventureWorks2012]
GO

IF OBJECT_ID (N'Sales.scalar_valued', N'FN') IS NOT NULL
	DROP FUNCTION Sales.scalar_valued
GO

IF OBJECT_ID (N'Production.inline_table_valued', N'IF') IS NOT NULL
	DROP FUNCTION Production.inline_table_valued
GO

CREATE FUNCTION Sales.scalar_valued (@SalesOrderID int )  
RETURNS MONEY  
BEGIN  
	RETURN (
		SELECT MAX(UnitPrice) 
		FROM Sales.SalesOrderDetail  
		WHERE SalesOrderID=@SalesOrderID
	)  
END
GO

CREATE FUNCTION Production.inline_table_valued (@ProductID int, @Count int)
RETURNS TABLE
RETURN (
	SELECT TOP (@Count) *
	FROM Production.ProductInventory
	WHERE ProductID = @ProductID AND Shelf = 'A'
	ORDER BY Quantity DESC)
GO

CREATE FUNCTION Production.multistatement_table_valued (@ProductID int, @Count int)
RETURNS @ResultTable TABLE (ProductID int, LocationID int, Shelf nvarchar(10), Bin tinyint, Quantity smallint, rowguid uniqueidentifier, ModifiedDate datetime)
AS
BEGIN
	INSERT INTO @ResultTable
		SELECT TOP (@Count) *
		FROM Production.ProductInventory
		WHERE ProductID = @ProductID AND Shelf = 'A'
		ORDER BY Quantity DESC
	RETURN
END
GO

SELECT *, Sales.scalar_valued(43659)
FROM Sales.SalesOrderDetail
WHERE SalesOrderID = 43659

SELECT *
FROM Production.ProductInventory
CROSS APPLY Production.inline_table_valued(ProductID, 2)

SELECT *
FROM Production.ProductInventory
OUTER APPLY Production.inline_table_valued(ProductID, 2)

SELECT *
FROM Production.ProductInventory
CROSS APPLY Production.multistatement_table_valued(ProductID, 2)