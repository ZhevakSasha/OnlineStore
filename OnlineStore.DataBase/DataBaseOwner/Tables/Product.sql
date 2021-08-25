CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PruductName] NVARCHAR(50) NULL, 
    [Price] INT NULL, 
    [UnitOfMeasurement] NCHAR(10) NULL
)
