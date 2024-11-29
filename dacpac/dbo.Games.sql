CREATE TABLE [dbo].[Games] (
    [GameID]                 INT           IDENTITY (1, 1) NOT NULL,
    [StartDate]              DATETIME      NULL,
    [EndDate]                DATETIME      NULL,
    [GameDuration]           INT           NULL,
    [Moves]                  INT           NULL,
    [PlayerID]               INT           NULL,
    [TblQueryResultsQueryId] INT           NULL,
    [TblUsersId]             INT           DEFAULT ((0)) NOT NULL,
    [Result]                 NVARCHAR (50) NULL,
    CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Games_QueryResults_TblQueryResultsQueryId] FOREIGN KEY ([TblQueryResultsQueryId]) REFERENCES [dbo].[QueryResults] ([QueryId]),
    CONSTRAINT [FK_Games_Users_TblUsersId] FOREIGN KEY ([TblUsersId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Games_TblQueryResultsQueryId]
    ON [dbo].[Games]([TblQueryResultsQueryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Games_TblUsersId]
    ON [dbo].[Games]([TblUsersId] ASC);

