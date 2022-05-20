CREATE TABLE [dbo].[forecasthour] (
    [Id]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [forecastday_id] BIGINT          NOT NULL,
    [time]           DATETIME        NOT NULL,
    [temp]           DECIMAL (18, 2) NULL,
    [condition]      NVARCHAR (55)   NULL,
    [wind_speed]     DECIMAL (18, 2) NULL,
    [wind_direction] NVARCHAR (55)   NULL,
    [pressure]       DECIMAL (18, 2) NULL,
    [humidity]       INT             NULL,
    [cloud_coverage] INT             NULL,
    [visibility]     DECIMAL (18, 4) NULL,
    [rain]           BIT             DEFAULT ((0)) NULL,
    [snow]           BIT             DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_forecasthour_day] FOREIGN KEY ([forecastday_id]) REFERENCES [dbo].[forecastday] ([Id])
);

