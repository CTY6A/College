USE [AdventureWorks2012]
GO

IF OBJECT_ID('Production.WorkOrderHst', 'U') IS NOT NULL 
  DROP TABLE Production.WorkOrderHst;
GO

IF OBJECT_ID(N'Production.Production_trigger', N'TR') IS NOT NULL 
  DROP TRIGGER Production.Production_trigger;
GO

IF OBJECT_ID(N'Production.WorkOrder_VIEW', N'V') IS NOT NULL 
  DROP VIEW Production.WorkOrder_VIEW;
GO

CREATE TABLE Production.WorkOrderHst (
	ID int IDENTITY(1,1) PRIMARY KEY,
	Action CHAR (6),
	ModifiedDate DATE,
	SourceID int,
	UserName VARCHAR (20)
)
GO

CREATE TRIGGER Production.Production_trigger
ON Production.WorkOrder
AFTER INSERT, UPDATE, DELETE
AS
IF EXISTS (SELECT TOP 1 * FROM Inserted) AND NOT EXISTS (SELECT TOP 1 * FROM Deleted) --INSERT
	INSERT INTO Production.WorkOrderHst (Action)
	VALUES ('INSERT')

IF EXISTS (SELECT TOP 1 * FROM Inserted) AND EXISTS (SELECT TOP 1 * FROM Deleted) --UPDATE
	INSERT INTO Production.WorkOrderHst (Action)
	VALUES ('UPDATE')

IF NOT EXISTS (SELECT TOP 1 * FROM Inserted) AND EXISTS (SELECT TOP 1 * FROM Deleted) --DELETE
	INSERT INTO Production.WorkOrderHst (Action)
	VALUES ('DELETE')
GO

CREATE VIEW Production.WorkOrder_VIEW
AS 
SELECT *
FROM Production.WorkOrder
GO

INSERT INTO Production.WorkOrder (ScrappedQty, ProductID, OrderQty, StartDate, DueDate)
VALUES (777, 777, 777, GETDATE(), GETDATE())

UPDATE Production.WorkOrder
SET ScrappedQty = 69
WHERE ScrappedQty = 777

DELETE Production.WorkOrder
WHERE ScrappedQty = 69

SELECT *
FROM Production.WorkOrderHst

SELECT *
FROM Production.WorkOrder_VIEW