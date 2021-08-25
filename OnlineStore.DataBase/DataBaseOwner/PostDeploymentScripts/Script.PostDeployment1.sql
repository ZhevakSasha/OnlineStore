/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select * from dbo.Customers where CustomerId = 11 and FirstName = 'Sasha' and  LastName = 'Zhevak' and Addres = 'Main Street' and PhoneNumber = '0669705219' )
begin
INSERT INTO dbo.Customers(CustomerId, FirstName, LastName, Addres, PhoneNumber)
VALUES (11,'Sasha','Zhevak','Main Street','0669705219');
end

if not exists (select * from dbo.Product where ProductId = 21 and PruductName ='Keyboard' and  Price = 200 and UnitOfMeasurement ='pc.' )
begin
INSERT INTO dbo.Product(ProductId, PruductName, Price, UnitOfMeasurement)
VALUES (21,'Keyboard',200,'pc.');
end  

if not exists (select * from dbo.Sales where SalesId =1 and ProductId =21 and  CustomerId = 11 and DateOfSale = '25.08.2021' and Amount =2 )
begin
INSERT INTO dbo.Sales(SalesId, ProductId, CustomerId, DateOfSale, Amount)
VALUES (1 ,21,11,'25.08.2021',2);
end  

