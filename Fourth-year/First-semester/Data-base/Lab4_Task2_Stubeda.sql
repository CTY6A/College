USE [AdventureWorks2012]
GO

IF OBJECT_ID('Production.Product_VIEW', 'V') IS NOT NULL 
  DROP VIEW Production.Product_VIEW;
GO

CREATE VIEW Production.Product_VIEW
WITH ENCRYPTION, SCHEMABINDING
AS 
SELECT 
	ProductModelProductDescriptionCulture.ProductModelID,
	ProductModelProductDescriptionCulture.ProductDescriptionID,
	ProductModelProductDescriptionCulture.CultureID,
	ProductModelProductDescriptionCulture.ModifiedDate,
	ProductModel.Name,
	CatalogDescription,
	Instructions,
	ProductDescription.rowguid,
	Description
FROM Production.ProductModelProductDescriptionCulture
	JOIN Production.ProductModel
	ON ProductModel.ProductModelID = ProductModelProductDescriptionCulture.ProductModelID

	JOIN Production.ProductDescription
	ON ProductDescription.ProductDescriptionID = ProductModelProductDescriptionCulture.ProductDescriptionID

	JOIN Production.Culture
	ON Culture.CultureID = ProductModelProductDescriptionCulture.CultureID
GO

CREATE UNIQUE CLUSTERED INDEX Clustered_INDEX
ON Production.Product_VIEW (ProductModelID,CultureID)
GO

CREATE TRIGGER Production.Product_INSERT
ON Production.Product_VIEW
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO Production.ProductModel( 
        Name, CatalogDescription, Instructions, rowguid, ModifiedDate
    )
    SELECT
        i.Name, i.CatalogDescription, i.Instructions, i.rowguid, i.ModifiedDate
    FROM
        inserted i;

	INSERT INTO Production.Culture( 
        CultureID, Name, ModifiedDate
    )
    SELECT
        i.CultureID, i.Name, i.ModifiedDate
    FROM
        inserted i;

	INSERT INTO Production.ProductDescription( 
        Description, rowguid, ModifiedDate
    )
    SELECT
        i.Description, i.rowguid, i.ModifiedDate
    FROM
        inserted i;

    INSERT INTO Production.ProductModelProductDescriptionCulture( 
        ProductModelID, ProductDescriptionID, CultureID, ModifiedDate
    )
    SELECT
        ProductModel.ProductModelID, ProductDescription.ProductDescriptionID, i.CultureID, i.ModifiedDate
    FROM
        inserted i, ProductModel, ProductDescription
	WHERE
		ProductModel.Name = i.Name AND
		ProductDescription.Description = i.Description;
END
GO

CREATE TRIGGER Production.Product_UPDATE
ON Production.Product_VIEW
INSTEAD OF UPDATE
AS
BEGIN
	DELETE sq
	FROM Production.ProductModelProductDescriptionCulture sq
		INNER JOIN deleted d
		ON d.CultureID = sq.CultureID
	WHERE
		d.CultureID = sq.CultureID;

	UPDATE Production.ProductModel
    SET
        Name = i.Name,
		CatalogDescription = i.CatalogDescription,
		Instructions = i.Instructions,
		rowguid = i.rowguid,
		ModifiedDate = i.ModifiedDate
    FROM
        deleted d, inserted i
	WHERE 
		d.Name = ProductModel.Name;

	UPDATE Production.Culture
	SET 
        CultureID = i.CultureID,
		Name = i.Name,
		ModifiedDate = i.ModifiedDate
    FROM
        deleted d, inserted i
	WHERE
		d.CultureID = Culture.CultureID;

	UPDATE Production.ProductDescription
	SET 
        Description = i.Description,
		rowguid = i.rowguid,
		ModifiedDate = i.ModifiedDate
    FROM
        deleted d, inserted i
	WHERE 
		d.rowguid = ProductDescription.rowguid;
	
	INSERT INTO Production.ProductModelProductDescriptionCulture( 
        ProductModelID, ProductDescriptionID, CultureID, ModifiedDate
    )
    SELECT
        ProductModel.ProductModelID, ProductDescription.ProductDescriptionID, i.CultureID, i.ModifiedDate
    FROM
        inserted i, ProductModel, ProductDescription
	WHERE
		ProductModel.Name = i.Name AND
		ProductDescription.Description = i.Description;
END
GO

CREATE TRIGGER Production.Product_DELETE
ON Production.Product_VIEW
INSTEAD OF DELETE
AS
BEGIN
	DELETE sq
	FROM Production.ProductModelProductDescriptionCulture sq
		INNER JOIN deleted d
		ON d.CultureID = sq.CultureID
	WHERE
		d.CultureID = sq.CultureID;

	DELETE sq
	FROM Production.ProductModel sq
		INNER JOIN deleted d
		ON d.ProductModelID = sq.ProductModelID
	WHERE
		d.ProductModelID = sq.ProductModelID;	
		
	DELETE sq
	FROM Production.Culture sq
		INNER JOIN deleted d
		ON d.CultureID = sq.CultureID
	WHERE
		d.CultureID = sq.CultureID;	

	DELETE sq
	FROM Production.ProductDescription sq
		INNER JOIN deleted d
		ON d.ProductDescriptionID = sq.ProductDescriptionID
	WHERE
		d.ProductDescriptionID = sq.ProductDescriptionID;	
END
GO

INSERT INTO Production.Product_VIEW (CultureID, ModifiedDate, Name, rowguid, Description)
VALUES ('aa', GETDATE(), 'AAA', '53E49876-BFE9-43B6-9DF6-F928390F1A8E', 'лол')

UPDATE Production.Product_VIEW
SET Name = 'LOL', CultureID = '69'
WHERE Name = 'AAA'
GO

DELETE
FROM Production.Product_VIEW
WHERE Name = 'LOL'
GO

SELECT *
FROM Production.Product_VIEW
ORDER BY ProductModelID

SELECT *
FROM Production.ProductModelProductDescriptionCulture

SELECT *
FROM Production.ProductModel

SELECT *
FROM Production.Culture

SELECT *
FROM Production.ProductDescription