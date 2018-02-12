CREATE TABLE [dbo].[FindPatternPerfomance] (
    [DFindPatternPerfomanceId] INT            IDENTITY (1, 1) NOT NULL,
    [Algorithm]                NVARCHAR (MAX) NOT NULL,
    [TextSize]                 INT            NOT NULL,
    [PatternSize]              INT            NOT NULL,
    [Text]                     NVARCHAR (MAX) NOT NULL,
    [Pattern]                  NVARCHAR (MAX) NOT NULL,
    [OutputPresentation]       NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]        BIGINT         NULL,
    [Duration]                 BIGINT         NULL,
    [DurationMilliSeconds]     BIGINT         NULL,
    [DateComplete]             DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([DFindPatternPerfomanceId] ASC)
);

