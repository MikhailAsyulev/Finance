﻿CREATE TABLE [dbo].[DepositType] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [ReturnTerm] BIGINT         NOT NULL,
    [Percent]    FLOAT (53)     NOT NULL,
    [IsActive] BIT NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [CurrencyShort] VARCHAR(3) NOT NULL, 
    CONSTRAINT [PK_DepositType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

