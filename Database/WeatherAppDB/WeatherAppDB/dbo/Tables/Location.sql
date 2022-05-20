CREATE TABLE [dbo].[location]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [name] NVARCHAR(50) NOT NULL, 
    [latitude] DECIMAL(18, 2) NOT NULL, 
    [longitude] DECIMAL(18, 2) NOT NULL

)
