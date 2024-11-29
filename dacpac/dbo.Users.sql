CREATE TABLE [dbo].[Users] (
    [Id]                INT            NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [PhoneNumber]       NVARCHAR (MAX) NULL,
    [Country]           NVARCHAR (MAX) NULL,
    [RegisteritionDate] DATETIME2 (7)  NOT NULL,
    [GamesPlayed]       INT            NULL,
    [Password]          NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

