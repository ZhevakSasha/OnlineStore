﻿/*
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

if not exists (select * from dbo.Customers where FirstName = 'Sasha' and  LastName = 'Zhevak' and Addres = 'Main Street' and PhoneNumber = '0669705219' )
begin
INSERT INTO dbo.Customers(FirstName, LastName, Addres, PhoneNumber)
VALUES ('Sasha','Zhevak','Main Street','0669705219');
end

if not exists (select * from dbo.Product where PruductName ='Keyboard' and  Price = 200 and UnitOfMeasurement ='pc.' )
begin
INSERT INTO dbo.Product(PruductName, Price, UnitOfMeasurement)
VALUES ('Keyboard',200,'pc.');
end  

if not exists (select * from dbo.Sales where ProductId =1 and  CustomerId = 1 and DateOfSale = '25.08.2021' and Amount =2 )
begin
INSERT INTO dbo.Sales(ProductId, CustomerId, DateOfSale, Amount)
VALUES (1,1,'25.08.2021',2);
end  
