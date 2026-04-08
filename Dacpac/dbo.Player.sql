USE [ServerDB]
GO

/****** Object: Table [dbo].[Player] Script Date: 4/8/2026 11:03:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Player] (
    [Id]          INT            NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (MAX) NOT NULL,
    [Country]     NVARCHAR (MAX) NOT NULL,
    [SessionID]   INT            NULL
);


