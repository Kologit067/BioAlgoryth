CREATE TABLE [dbo].[TravellingSalesmanPerfomance] (
    [TravellingSalesmanPerfomanceId] INT             IDENTITY (1, 1) NOT NULL,
    [Algorithm]                      NVARCHAR (50)   NOT NULL,
    [InputSize]                      INT             NOT NULL,
    [Limit]                          INT             NOT NULL,
    [Step]                           INT             NOT NULL,
    [InputPresentation]              NVARCHAR (1000) NOT NULL,
    [OutputPresentation]             NVARCHAR (1000) NOT NULL,
    [NumberOfIteration]              BIGINT          NULL,
    [NumberOfComparison]             BIGINT          NULL,
    [Duration]                       BIGINT          NULL,
    [DurationMilliSeconds]           BIGINT          NULL,
    [DateComplete]                   DATETIME        NULL,
    [AdditionalInfo]                 NVARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([TravellingSalesmanPerfomanceId] ASC)
);

