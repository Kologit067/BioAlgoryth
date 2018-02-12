CREATE TYPE [dbo].[FindPatternPerfomanceType] AS TABLE (
    [Algorithm]            NVARCHAR (MAX) NOT NULL,
    [TextSize]             INT            NOT NULL,
    [PatternSize]          INT            NOT NULL,
    [Text]                 NVARCHAR (MAX) NOT NULL,
    [Pattern]              NVARCHAR (MAX) NOT NULL,
    [OutputPresentation]   NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]    BIGINT         NULL,
    [Duration]             BIGINT         NULL,
    [DurationMilliSeconds] BIGINT         NULL,
    [DateComplete]         DATETIME       NULL);

