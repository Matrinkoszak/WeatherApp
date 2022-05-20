CREATE TABLE [dbo].[User] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [Login]        NVARCHAR (50) NOT NULL,
    [Display_Name] NVARCHAR (50) NULL,
    [Password]     NVARCHAR (50) NOT NULL,
    [IsAdmin]      BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

