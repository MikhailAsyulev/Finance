﻿CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1), 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [UserProfileId] INT NOT NULL, 
    [IsInternal] BIT NOT NULL, 
    [RequestId] INT NOT NULL, 
    [Date] DATETIME NOT NULL, 
    CONSTRAINT [FK_Comment_UserProfile] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_Comment_Request] FOREIGN KEY ([RequestId]) REFERENCES [Request]([Id])
)
