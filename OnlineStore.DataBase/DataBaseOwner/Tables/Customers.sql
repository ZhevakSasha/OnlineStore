CREATE TABLE [dbo].[Customers]
(
	[CustomerId] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Addres] NVARCHAR(50) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL
)
