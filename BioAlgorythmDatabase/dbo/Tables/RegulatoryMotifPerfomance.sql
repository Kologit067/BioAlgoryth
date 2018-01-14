CREATE TABLE [dbo].[RegulatoryMotifPerfomance] (
    [RegulatoryMotifPerfomanceId] INT            IDENTITY (1, 1) NOT NULL,
    [Size]                        INT            NOT NULL,
    [NumberOfSequence]            INT            NOT NULL,
    [SequenceLengthes]            VARCHAR (500)  NOT NULL,
    [MotifLength]                 INT            NOT NULL,
    [InputData]                   NVARCHAR (MAX) NOT NULL,
    [OutputPresentation]          NVARCHAR (MAX) NOT NULL,
    [Algorithm]                   NVARCHAR (MAX) NOT NULL,
    [NumberOfIteration]           BIGINT         NULL,
    [Duration]                    BIGINT         NULL,
    [DurationMilliSeconds]        BIGINT         NULL,
    [DateComplete]                DATETIME       NULL,
    [IsComplete]                  BIT            NULL,
    [LastRoute]                   VARCHAR (500)  NULL,
    [OptimalRoute]                VARCHAR (500)  NULL,
    [OptimalValue]                INT            NULL,
    [StartPosition]               VARCHAR (500)  NULL,
    [Motif]                       VARCHAR (500)  NULL,
    [CountTerminal]               INT            NULL,
    [UpdateOptcount]              INT            NULL,
    [ElemenationCount]            INT            NULL,
    [IsOptimizitaion]             BIT            NULL,
    [IsSumAsCriteria]             BIT            NULL,
    [IsAllResult]                 BIT            NULL,
    PRIMARY KEY CLUSTERED ([RegulatoryMotifPerfomanceId] ASC)
);









