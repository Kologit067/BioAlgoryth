CREATE TYPE [dbo].[AlgorithmOptimalValueChangeType] AS TABLE (
    [DBName]               NVARCHAR (500) NOT NULL,
    [Algorithm]            NVARCHAR (MAX) NOT NULL,
    [TableSetAsNumber]     INT            NULL,
    [NumberOfIteration]    BIGINT         NULL,
    [Duration]             BIGINT         NULL,
    [DurationMilliSeconds] BIGINT         NULL,
    [OptimalValue]         INT            NULL,
    [OptimalRoute]         VARCHAR (500)  NULL,
    [OptimalNative]        VARCHAR (500)  NULL);

