CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductName] NVARCHAR(50) NULL, 
    [Price] INT NULL, 
    [UnitOfMeasurement] NVARCHAR(50) NULL
)
