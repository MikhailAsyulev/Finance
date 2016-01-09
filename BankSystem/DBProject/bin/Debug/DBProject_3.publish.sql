﻿/*
Deployment script for Finance

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Finance"
:setvar DefaultFilePrefix "Finance"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Client].[AccountId] is being dropped, data loss could occur.

The column [dbo].[Client].[UserProfileId] on table [dbo].[Client] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Client])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[Employee].[AccountId] is being dropped, data loss could occur.

The column [dbo].[Employee].[UserProfileId] on table [dbo].[Employee] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Employee])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[DF_Employee_IsActive]...';


GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [DF_Employee_IsActive];


GO
PRINT N'Dropping [dbo].[FK_Client_Account]...';


GO
ALTER TABLE [dbo].[Client] DROP CONSTRAINT [FK_Client_Account];


GO
PRINT N'Dropping [dbo].[FK_ClientCredit_Client]...';


GO
ALTER TABLE [dbo].[ClientCredit] DROP CONSTRAINT [FK_ClientCredit_Client];


GO
PRINT N'Dropping [dbo].[FK_ClientDeposit_Client]...';


GO
ALTER TABLE [dbo].[ClientDeposit] DROP CONSTRAINT [FK_ClientDeposit_Client];


GO
PRINT N'Dropping [dbo].[FK_ClientInfo_Client]...';


GO
ALTER TABLE [dbo].[ClientInfo] DROP CONSTRAINT [FK_ClientInfo_Client];


GO
PRINT N'Dropping [dbo].[FK_Employee_Account]...';


GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_Account];


GO
PRINT N'Dropping [dbo].[FK_Employee_EmployeeType]...';


GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_EmployeeType];


GO
PRINT N'Starting rebuilding table [dbo].[Client]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Client] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [UserProfileId]        INT           NOT NULL,
    [IdentityNumber]       NVARCHAR (50) NULL,
    [PassportSerialNumber] NVARCHAR (50) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Client])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Client] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Client] ([Id], [IdentityNumber], [PassportSerialNumber])
        SELECT   [Id],
                 [IdentityNumber],
                 [PassportSerialNumber]
        FROM     [dbo].[Client]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Client] OFF;
    END

DROP TABLE [dbo].[Client];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Client]', N'Client';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Client]', N'PK_Client', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Employee]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Employee] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [UserProfileId]  INT NOT NULL,
    [EmployeeTypeId] INT NOT NULL,
    [IsActive]       BIT CONSTRAINT [DF_Employee_IsActive] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Employee])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Employee] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Employee] ([Id], [EmployeeTypeId], [IsActive])
        SELECT   [Id],
                 [EmployeeTypeId],
                 [IsActive]
        FROM     [dbo].[Employee]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Employee] OFF;
    END

DROP TABLE [dbo].[Employee];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Employee]', N'Employee';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Employee]', N'PK_Employee', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_ClientCredit_Client]...';


GO
ALTER TABLE [dbo].[ClientCredit] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientCredit_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ClientDeposit_Client]...';


GO
ALTER TABLE [dbo].[ClientDeposit] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientDeposit_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating [dbo].[FK_ClientInfo_Client]...';


GO
ALTER TABLE [dbo].[ClientInfo] WITH NOCHECK
    ADD CONSTRAINT [FK_ClientInfo_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Client_UserProfile]...';


GO
ALTER TABLE [dbo].[Client] WITH NOCHECK
    ADD CONSTRAINT [FK_Client_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([UserId]);


GO
PRINT N'Creating [dbo].[FK_Employee_Account]...';


GO
ALTER TABLE [dbo].[Employee] WITH NOCHECK
    ADD CONSTRAINT [FK_Employee_Account] FOREIGN KEY ([UserProfileId]) REFERENCES [dbo].[UserProfile] ([UserId]);


GO
PRINT N'Creating [dbo].[FK_Employee_EmployeeType]...';


GO
ALTER TABLE [dbo].[Employee] WITH NOCHECK
    ADD CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY ([EmployeeTypeId]) REFERENCES [dbo].[EmployeeType] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[ClientCredit] WITH CHECK CHECK CONSTRAINT [FK_ClientCredit_Client];

ALTER TABLE [dbo].[ClientDeposit] WITH CHECK CHECK CONSTRAINT [FK_ClientDeposit_Client];

ALTER TABLE [dbo].[ClientInfo] WITH CHECK CHECK CONSTRAINT [FK_ClientInfo_Client];

ALTER TABLE [dbo].[Client] WITH CHECK CHECK CONSTRAINT [FK_Client_UserProfile];

ALTER TABLE [dbo].[Employee] WITH CHECK CHECK CONSTRAINT [FK_Employee_Account];

ALTER TABLE [dbo].[Employee] WITH CHECK CHECK CONSTRAINT [FK_Employee_EmployeeType];


GO
PRINT N'Update complete.';


GO