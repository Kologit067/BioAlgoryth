﻿CREATE TYPE [dbo].[DNAMappingPerfomanceType] AS TABLE (
    [NumberInArray]        INT            NULL,
    [Size]                 INT            NOT NULL,
    [InputData]            NVARCHAR (MAX) NOT NULL,
    [OutputPresentation]   NVARCHAR (MAX) NOT NULL,
    [Algorithm]            NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]    BIGINT         NULL,
    [Duration]             BIGINT         NULL,
    [DurationMilliSeconds] BIGINT         NULL,
    [DateComplete]         DATETIME       NULL,
    [IsComplete]           BIT            NULL,
    [LastRoute]            VARCHAR (500)  NULL,
    [OptimalRoute]         VARCHAR (500)  NULL,
    [CountTerminal]        INT            NULL,
    [UpdateOptcount]       INT            NULL,
    [ElemenationCount]     INT            NULL,
    [IsAllResult]          BIT            NULL);

