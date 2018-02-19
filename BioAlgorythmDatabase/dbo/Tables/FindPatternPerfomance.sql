CREATE TABLE [dbo].[FindPatternPerfomance] (
    [FindPatternPerfomanceId] INT            IDENTITY (1, 1) NOT NULL,
    [Algorithm]               NVARCHAR (MAX) NOT NULL,
    [TextSize]                INT            NOT NULL,
    [PatternSize]             INT            NOT NULL,
    [AlphabetSize]            INT            NOT NULL,
    [Text]                    NVARCHAR (MAX) NOT NULL,
    [Pattern]                 NVARCHAR (MAX) NOT NULL,
    [OutputPresentation]      NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]       BIGINT         NULL,
    [NumberOfComparison]      BIGINT         NULL,
    [Duration]                BIGINT         NULL,
    [DurationMilliSeconds]    BIGINT         NULL,
    [DateComplete]            DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([FindPatternPerfomanceId] ASC)
);







