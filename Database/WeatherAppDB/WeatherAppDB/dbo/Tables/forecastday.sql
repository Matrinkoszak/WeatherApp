CREATE TABLE [dbo].[forecastday] (
    [Id]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [date]              DATETIME        NOT NULL,
    [max_temp]          DECIMAL (18, 2) NULL,
    [min_temp]          DECIMAL (18, 2) NULL,
    [avg_temp]          DECIMAL (18, 2) NULL,
    [max_wind]          DECIMAL (18, 2) NULL,
    [min_wind]          DECIMAL (18, 2) NULL,
    [avg_wind]          DECIMAL (18, 2) NULL,
    [total_precision]   DECIMAL (18, 2) NULL,
    [avg_visibility]    DECIMAL (18, 4) NULL,
    [avg_humidity]      INT             NULL,
    [condition]         NVARCHAR (55)   NULL,
    [sunrise]           DATETIME        NULL,
    [sunset]            DATETIME        NULL,
    [moonrise]          DATETIME        NULL,
    [moonset]           DATETIME        NULL,
    [moonphase]         NVARCHAR (55)   NULL,
    [moon_illumination] INT             NULL,
    [location_id] BIGINT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_forecastday_location] FOREIGN KEY ([location_id]) REFERENCES [dbo].[location]([Id])
);

