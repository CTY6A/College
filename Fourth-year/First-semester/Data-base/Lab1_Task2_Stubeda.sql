USE [AdventureWorks2012]
GO

SELECT Name, GroupName FROM AdventureWorks2012.HumanResources.Department
WHERE GroupName = 'Executive General and Administration'
GO

SELECT MAX(VacationHours) AS MaxVacationHours
FROM AdventureWorks2012.HumanResources.Employee
GO

SELECT *
FROM AdventureWorks2012.HumanResources.Employee
WHERE JobTitle LIKE '%Engineer%'
GO


