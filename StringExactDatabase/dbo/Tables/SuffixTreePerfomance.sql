CREATE TABLE [dbo].[SuffixTreePerfomance] (
    [SuffixTreePerfomanceId] INT             IDENTITY (1, 1) NOT NULL,
    [Algorithm]              NVARCHAR (50)   NOT NULL,
    [TextSize]               INT             NOT NULL,
    [AlphabetSize]           INT             NOT NULL,
    [Text]                   NVARCHAR (500)  NOT NULL,
    [OutputPresentation]     NVARCHAR (1000) NOT NULL,
    [NumberOfIteration]      BIGINT          NULL,
    [NumberOfComparison]     BIGINT          NULL,
    [Duration]               BIGINT          NULL,
    [DurationMilliSeconds]   BIGINT          NULL,
    [DateComplete]           DATETIME        NULL,
    [AdditionalInfo]         NVARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([SuffixTreePerfomanceId] ASC)
);

