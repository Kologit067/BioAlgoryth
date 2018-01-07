CREATE TYPE [dbo].[RegulatoryMotifOptimalValueChangeType] AS TABLE (
    [NumberInArray]        INT           NULL,
    [NumberOfIteration]    BIGINT        NULL,
    [Duration]             BIGINT        NULL,
    [DurationMilliSeconds] BIGINT        NULL,
    [OptimalValue]         INT           NULL,
    [StartPosition]        VARCHAR (500) NULL,
    [Motif]                VARCHAR (500) NULL);

