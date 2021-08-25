CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Addres] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL
)
