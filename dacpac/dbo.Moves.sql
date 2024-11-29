CREATE TABLE [dbo].[Moves] (
    [MoveId]         INT            IDENTITY (1, 1) NOT NULL,
    [GameId]         INT            NULL,
    [PlayerId]       INT            NULL,
    [FromPosition]   NVARCHAR (MAX) NULL,
    [ToPosition]     NVARCHAR (MAX) NULL,
    [TimeTaken]      INT            NULL,
    [TblGamesGameID] NCHAR (10)     NULL,
    CONSTRAINT [PK_Moves] PRIMARY KEY CLUSTERED ([MoveId] ASC)
);

