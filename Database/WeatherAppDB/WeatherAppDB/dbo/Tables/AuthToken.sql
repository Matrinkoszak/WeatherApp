CREATE TABLE [dbo].[authToken]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [creationDate] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [terminationDate] DATETIME NOT NULL, 
    [user_id] BIGINT NOT NULL, 
    [code] NVARCHAR(155) NOT NULL, 
    CONSTRAINT [FK_authToken_user] FOREIGN KEY ([user_id]) REFERENCES [User]([Id])  
)
