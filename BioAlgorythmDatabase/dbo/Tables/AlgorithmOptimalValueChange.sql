CREATE TABLE [dbo].[AlgorithmOptimalValueChange] (
    [AlgorithmOptimalValueChangeId] INT           IDENTITY (1, 1) NOT NULL,
    [AlgorithmPerfomanceId]         INT           NOT NULL,
    [NumberOfIteration]             BIGINT        NULL,
    [Duration]                      BIGINT        NULL,
    [DurationMilliSeconds]          BIGINT        NULL,
    [OptimalValue]                  INT           NULL,
    [OptimalRoute]                  VARCHAR (500) NULL,
    [OptimalNative]                 VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([AlgorithmOptimalValueChangeId] ASC),
    CONSTRAINT [FK_AlgorithmOptimalValueChange_AlgorithmPerfomance] FOREIGN KEY ([AlgorithmPerfomanceId]) REFERENCES [dbo].[AlgorithmPerfomance] ([AlgorithmPerfomance])
);

