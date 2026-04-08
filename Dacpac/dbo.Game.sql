USE [ServerDB]
GO

/****** Object: Table [dbo].[Game] Script Date: 4/8/2026 11:04:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Game] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PlayerId]    INT            NOT NULL,
    [GameDate]    DATETIME2 (7)  NOT NULL,
    [Duration]    INT            NOT NULL,
    [GameOutcome] NVARCHAR (MAX) NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Game_PlayerId]
    ON [dbo].[Game]([PlayerId] ASC);


GO
ALTER TABLE [dbo].[Game]
    ADD CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Game]
    ADD CONSTRAINT [FK_Game_Player_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [dbo].[Player] ([Id]) ON DELETE CASCADE;


