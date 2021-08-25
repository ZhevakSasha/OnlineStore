CREATE TABLE [dbo].[Sales]
(
	[SalesId] INT NULL, 
    [ProductId] INT NULL, 
    [CustomerId] INT NULL, 
    [DateOfSale] NVARCHAR(50) NULL, 
    [Amount] INT NULL, 
    CONSTRAINT [FK_ProductId_ToSales] FOREIGN KEY ([ProductId]) REFERENCES [Product]([ProductId]), 
    CONSTRAINT [FK_CustomerId_ToSales] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([CustomerId]) 
)
