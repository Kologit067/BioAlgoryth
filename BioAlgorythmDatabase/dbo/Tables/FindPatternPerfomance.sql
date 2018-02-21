CREATE TABLE [dbo].[FindPatternPerfomance] (
    [FindPatternPerfomanceId] INT             IDENTITY (1, 1) NOT NULL,
    [Algorithm]               NVARCHAR (50)   NOT NULL,
    [TextSize]                INT             NOT NULL,
    [PatternSize]             INT             NOT NULL,
    [AlphabetSize]            INT             NOT NULL,
    [Text]                    NVARCHAR (500)  NOT NULL,
    [Pattern]                 NVARCHAR (100)  NOT NULL,
    [OutputPresentation]      NVARCHAR (1000) NOT NULL,
    [NumberOfIteration]       BIGINT          NULL,
    [NumberOfComparison]      BIGINT          NULL,
    [Duration]                BIGINT          NULL,
    [DurationMilliSeconds]    BIGINT          NULL,
    [DateComplete]            DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([FindPatternPerfomanceId] ASC)
);










GO
CREATE NONCLUSTERED INDEX [Idx_FindPatternPerfomance_algorythm_size]
    ON [dbo].[FindPatternPerfomance]([Algorithm] ASC, [TextSize] ASC, [PatternSize] ASC, [AlphabetSize] ASC);

