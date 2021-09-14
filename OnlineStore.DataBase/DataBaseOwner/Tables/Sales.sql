CREATE TABLE [dbo].[Sales]
(
	[Id] INT NOT NULL IDENTITY, 
    [ProductId] INT NULL, 
    [CustomerId] INT NULL, 
    [DateOfSale] NVARCHAR(50) NULL, 
    [Amount] INT NULL, 
    CONSTRAINT [FK_ProductId_ToSales] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_CustomerId_ToSales] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([Id]) ON DELETE CASCADE 
)
