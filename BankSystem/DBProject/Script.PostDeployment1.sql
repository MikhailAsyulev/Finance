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

IF not exists (select * from webpages_Roles)

Insert into webpages_Roles (RoleId,RoleName)
Values (1,'Admin'),
(2,'Client'),
(3,'Operator'),
(4,'SecurityWorker')
go
 