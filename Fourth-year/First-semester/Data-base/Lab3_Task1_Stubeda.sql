USE [AdventureWorks2012]
GO

ALTER TABLE dbo.StateProvince 
ADD CountryRegionName NVARCHAR(50)
GO

DECLARE @dboStateProvince
TABLE
(
	StateProvinceID INT IDENTITY(1,1),
	StateProvinceCode NCHAR(3),
	CountryRegionCode NVARCHAR(3),
	Name NAME,
	TerritoryID INT,
	ModifiedDate DATETIME,
	CountryNum INT NULL,
	CountryRegionName NVARCHAR(50)
)


INSERT INTO @dboStateProvince
SELECT 
	StateProvinceCode,
	CountryRegionCode,
	Name,
	TerritoryID,
	ModifiedDate,
	CountryNum,
	CountryRegionName
FROM dbo.StateProvince

UPDATE dbo.StateProvince
SET StateProvince.CountryRegionName = Person.CountryRegion.Name
FROM Person.CountryRegion
	INNER JOIN StateProvince
	ON (Person.CountryRegion.CountryRegionCode = StateProvince.CountryRegionCode)

UPDATE dbo.StateProvince
SET dbo.StateProvince.CountryRegionName = m.CountryRegionName
FROM @dboStateProvince m


DELETE FROM dbo.StateProvince 
WHERE  
 Name NOT IN (
 SELECT CountryRegion.Name
 FROM Person.Address
	INNER JOIN StateProvince
	ON Person.Address.StateProvinceID = StateProvince.StateProvinceID

	INNER JOIN Person.CountryRegion
	ON StateProvince.CountryRegionCode = CountryRegion.CountryRegionCode
 )

ALTER TABLE dbo.StateProvince 
DROP COLUMN CountryRegionName
GO

SELECT * 
FROM dbo.StateProvince
GO

SELECT *
FROM AdventureWorks2012.INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'StateProvince';
GO

--Я удаляю таблицу каждый раз во второй лабораторной во втором задании
--Во втором задании добавляю сюда ограничения, поэтому не могу удалить по имени 