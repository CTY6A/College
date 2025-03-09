use master

RESTORE DATABASE NewDatabase FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\Backup\NewDatabase.bak'
WITH RECOVERY
GO