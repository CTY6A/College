USE [AdventureWorks2012]
GO

IF OBJECT_ID('dbo.StateProvince', 'U') IS NOT NULL 
  DROP TABLE dbo.StateProvince;
GO 

SELECT
	StateProvinceID,
	StateProvinceCode,
	CountryRegionCode,
	IsOnlyStateProvinceFlag,
	Name,
	TerritoryID,
	ModifiedDate 
INTO dbo.StateProvince 
FROM Person.StateProvince
Where 1 = 2

ALTER TABLE dbo.StateProvince 
ADD UNIQUE(Name)

ALTER TABLE dbo.StateProvince 
ADD CHECK(CountryRegionCode LIKE '%[^0-9]%')

ALTER TABLE dbo.StateProvince 
ADD DEFAULT GETDATE() FOR ModifiedDate 

INSERT INTO 
	dbo.StateProvince
SELECT
	StateProvinceCode,
	StateProvince.CountryRegionCode,
	IsOnlyStateProvinceFlag,
	StateProvince.Name,
	TerritoryID,
	StateProvince.ModifiedDate 
FROM Person.StateProvince
	INNER JOIN Person.CountryRegion
	ON (StateProvince.CountryRegionCode = CountryRegion.CountryRegionCode)
WHERE (StateProvince.Name = CountryRegion.Name)

ALTER TABLE dbo.StateProvince 
DROP COLUMN IsOnlyStateProvinceFlag

ALTER TABLE dbo.StateProvince 
ADD CountryNum INT NULL

SELECT * 
FROM dbo.StateProvince