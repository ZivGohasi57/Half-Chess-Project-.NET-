CREATE TABLE [dbo].[QueryResults] (
    [QueryId]   INT           NOT NULL,
    [QueryDate] DATETIME2 (7) NULL,
    [UserId]    INT           NULL,
    PRIMARY KEY CLUSTERED ([QueryId] ASC)
);

