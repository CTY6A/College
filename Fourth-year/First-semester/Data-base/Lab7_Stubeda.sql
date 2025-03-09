USE [AdventureWorks2012]
GO

--1) {

IF OBJECT_ID('tempdb..#TmpTable', 'U') IS NOT NULL
  DROP TABLE #TmpTable
GO

DECLARE @X XML

SET @X = 
	(
	SELECT 
		BusinessEntityID AS "@ID",
		NationalIDNumber, 
		JobTitle
	FROM HumanResources.Employee
	FOR XML PATH('Employee'), ROOT('Employees')
	)

SELECT 
	Employees.value('./@ID', 'int') AS BusinessEntityID,
	Employees.value('(./NationalIDNumber)[1]', 'nvarchar(15)') AS NationalIDNumber,
	Employees.value('(./JobTitle)[1]', 'nvarchar(50)') AS JobTitle
INTO #TmpTable
FROM @X.nodes('/Employees/Employee') A (Employees)

SELECT *
FROM #TmpTable

--}