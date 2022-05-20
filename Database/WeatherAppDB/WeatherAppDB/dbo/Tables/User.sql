CREATE TABLE [dbo].[user] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [login]        NVARCHAR (50) NOT NULL,
    [display_Name] NVARCHAR (50) NULL,
    [password]     NVARCHAR (50) NOT NULL,
    [isAdmin]      BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

