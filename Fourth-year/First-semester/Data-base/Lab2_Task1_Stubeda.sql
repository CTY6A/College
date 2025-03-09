USE [AdventureWorks2012]
GO

SELECT Name, JobTitle, COUNT(Employee.BusinessEntityID)
FROM HumanResources.Employee
	INNER JOIN HumanResources.EmployeeDepartmentHistory
	ON (Employee.BusinessEntityID = EmployeeDepartmentHistory.BusinessEntityID)

	INNER JOIN HumanResources.Department
	ON (Department.DepartmentID = EmployeeDepartmentHistory.DepartmentID)
GROUP BY Name, JobTitle
ORDER BY Name

SELECT Employee.BusinessEntityID, JobTitle, Name, StartTime, EndTime
FROM HumanResources.Employee
	INNER JOIN HumanResources.EmployeeDepartmentHistory
	ON (Employee.BusinessEntityID = EmployeeDepartmentHistory.BusinessEntityID)

	INNER JOIN HumanResources.Shift
	ON (Shift.ShiftID = EmployeeDepartmentHistory.ShiftID) AND (EmployeeDepartmentHistory.ShiftID = '3')

SELECT 
	Employee.BusinessEntityID,
	JobTitle,
	Rate,
	LAG(Rate, 1, 0) 
		OVER (
			PARTITION BY Employee.BusinessEntityID 
			ORDER BY Employee.BusinessEntityID)
		PrevRate,
	Rate - LAG(Rate, 1, 0)
		OVER(
			PARTITION BY Employee.BusinessEntityID
			ORDER BY Employee.BusinessEntityID)
		Increased
FROM HumanResources.Employee
	INNER JOIN HumanResources.EmployeePayHistory
	ON (Employee.BusinessEntityID = EmployeePayHistory.BusinessEntityID)
GROUP BY Employee.BusinessEntityID, JobTitle, Rate
ORDER BY Employee.BusinessEntityID
