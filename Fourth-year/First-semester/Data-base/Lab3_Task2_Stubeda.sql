USE [AdventureWorks2012]
GO

IF OBJECT_ID('dbo.StateProvince', 'U') IS NOT NULL 
  DROP TABLE dbo.StateProvince;
GO

IF OBJECT_ID('tempdb..#StateProvince', 'U') IS NOT NULL 
  DROP TABLE #StateProvince;
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

ALTER TABLE dbo.StateProvince 
ADD SalesYTD MONEY

ALTER TABLE dbo.StateProvince 
ADD SumSales MONEY

ALTER TABLE dbo.StateProvince 
ADD SalesPercent AS (SumSales / SalesYTD * 100)

SELECT
	StateProvinceID,
	StateProvinceCode,
	CountryRegionCode,
	Name,
	TerritoryID,
	ModifiedDate,
	CountryNum,
	SalesYTD,
	SumSales
INTO #StateProvince 
FROM dbo.StateProvince
Where 1 = 2

ALTER TABLE dbo.StateProvince
ADD PRIMARY KEY (StateProvinceID)

INSERT INTO 
	#StateProvince 
SELECT
	StateProvinceCode,
	StateProvince.CountryRegionCode,
	StateProvince.Name,
	StateProvince.TerritoryID,
	SalesTerritory.ModifiedDate,
	CountryNum,
	SalesTerritory.SalesYTD,
	SumSales
FROM dbo.StateProvince
INNER JOIN Sales.SalesTerritory
ON Sales.SalesTerritory.TerritoryID = dbo.StateProvince.TerritoryID

;WITH CTE_SumSales (TerritoryID, SumSales)
 AS
 (
   SELECT TerritoryID, SUM(SalesYTD) AS SumSales
   FROM Sales.SalesPerson
   GROUP BY TerritoryID
 )
UPDATE #StateProvince
SET #StateProvince.SumSales = CTE_SumSales.SumSales
FROM CTE_SumSales
WHERE #StateProvince.TerritoryID = CTE_SumSales.TerritoryID

DELETE FROM dbo.StateProvince
WHERE StateProvinceID = 5

MERGE dbo.StateProvince t
	USING dbo.#StateProvince s
ON (t.StateProvinceID = s.StateProvinceID)
   WHEN MATCHED THEN
	   UPDATE SET 
			t.SalesYTD = s.SalesYTD,
			t.SumSales = s.SumSales
   WHEN NOT MATCHED BY TARGET 
   THEN INSERT
   (
		StateProvinceCode,
		CountryRegionCode,
		Name,
		TerritoryID,
		ModifiedDate,
		CountryNum,
		SalesYTD,
		SumSales
	) 
	VALUES
	(	 
		s.StateProvinceCode,
		s.CountryRegionCode,
		s.Name,
		s.TerritoryID,
		s.ModifiedDate,
		s.CountryNum,
		s.SalesYTD,
		s.SumSales
	)
	WHEN NOT MATCHED BY SOURCE 
    THEN DELETE;

SELECT * 
FROM StateProvince

SELECT * 
FROM #StateProvince
