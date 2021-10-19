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

if not exists (select * from dbo.Customers where FirstName = 'Sasha' and  LastName = 'Zhevak' and Address = 'Main Street' and PhoneNumber = '0669705219' )
begin
INSERT INTO 
dbo.Customers(FirstName, LastName, Address, PhoneNumber)
VALUES 
('Sasha','Zhevak','Main Street','0669705219');
end

if not exists (select * from dbo.Customers where FirstName = 'Andrew' and  LastName = 'Korolenko' and Address = '52 Street' and PhoneNumber = '0669705345' )
begin
INSERT INTO 
dbo.Customers(FirstName, LastName, Address, PhoneNumber)
VALUES 
('Andrew','Korolenko','52 Street','0669705345');
end

if not exists (select * from dbo.Products where ProductName ='Keyboard' and  Price = 200 and UnitOfMeasurement ='pc.' )
begin
INSERT INTO
dbo.Products(ProductName, Price, UnitOfMeasurement)
VALUES
('Keyboard',200,'pc.');
end  

if not exists (select * from dbo.Products where ProductName ='Mouse' and  Price = 120 and UnitOfMeasurement ='pc.' )
begin
INSERT INTO
dbo.Products(ProductName, Price, UnitOfMeasurement)
VALUES
('Mouse',120,'pc.');
end 

if not exists (select * from dbo.Sales where ProductId =1 and  CustomerId = 1 and DateOfSale = '2021-08-25' and Amount =2 )
begin
INSERT INTO 
dbo.Sales(ProductId, CustomerId, DateOfSale, Amount)
VALUES 
(1,1,'2021-08-25',2);
end  

if not exists (select * from dbo.Sales where ProductId =2 and  CustomerId = 2 and DateOfSale = '2021-08-26' and Amount =3 )
begin
INSERT INTO 
dbo.Sales(ProductId, CustomerId, DateOfSale, Amount)
VALUES 
(2,2,'2021-08-26',3);
end  

