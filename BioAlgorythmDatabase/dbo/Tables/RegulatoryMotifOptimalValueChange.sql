CREATE TABLE [dbo].[RegulatoryMotifOptimalValueChange] (
    [RegulatoryMotifOptimalValueChangeId] INT           IDENTITY (1, 1) NOT NULL,
    [RegulatoryMotifPerfomanceId]         INT           NOT NULL,
    [NumberOfIteration]                   BIGINT        NULL,
    [Duration]                            BIGINT        NULL,
    [DurationMilliSeconds]                BIGINT        NULL,
    [OptimalValue]                        INT           NULL,
    [StartPosition]                       VARCHAR (500) NULL,
    [Motif]                               VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([RegulatoryMotifOptimalValueChangeId] ASC)
);



