USE [AdventureWorks2012]
GO

IF OBJECT_ID (N'dbo.EmpCountByDep', N'P') IS NOT NULL
	DROP PROCEDURE dbo.EmpCountByDep
GO

CREATE PROCEDURE dbo.EmpCountByDep (@Years varchar(100))
AS
BEGIN
	EXECUTE ('
	SELECT Name, ' + @Years + '
	FROM 
	(
		SELECT Name, YEAR(StartDate) AS StartYear
		FROM HumanResources.Department
			INNER JOIN HumanResources.EmployeeDepartmentHistory
			ON Department.DepartmentID = EmployeeDepartmentHistory.DepartmentID
	
	) AS SourceTable
	PIVOT 
	(
		COUNT(StartYear)
		FOR StartYear 
		IN (' + @Years + ')
		) AS Pvt')
END
GO

EXECUTE dbo.EmpCountByDep '[2001],[2002],[2003],[2004],[2005],[2006],[2007]'