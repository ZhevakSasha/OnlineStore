CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductName] NVARCHAR(50) NULL, 
    [Price] INT NULL, 
    [UnitOfMeasurement] NVARCHAR(50) NULL
)
