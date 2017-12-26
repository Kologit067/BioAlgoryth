﻿

CREATE VIEW [dbo].[vw_place_eof_vs_place_eol]
AS
SELECT [DBName],
       [Tables],
       [ElementCount],
       [TableSetAsNumber]
      ,a1_NumberOfIteration
      ,a2_NumberOfIteration
      ,a1_DurationMilliSeconds
      ,a2_DurationMilliSeconds
      ,a1_Duration
      ,a2_Duration
      ,a1_ElemenationCount
      ,a2_ElemenationCount
      ,a1_OptimalValue
      ,a2_OptimalValue
      ,a1_OptimalRoute
      ,a2_OptimalRoute
      ,a1_CountTerminal
      ,a2_CountTerminal
      ,a1_UpdateOptcount
      ,a2_UpdateOptcount
      ,a1_IsComplete
      ,a2_IsComplete
      ,a1_LastRoute
      ,a2_LastRoute
FROM PV_ComparePerfomance2Algorithm('EOF_BEP_LEN_ETF_ISN_STN','EOL_BEP_LEN_ETF_ISN_STN')
--WHERE [ElementCount] = 2
--	AND a1_NumberOfIteration > a2_NumberOfIteration
--ORDER BY [TableSetAsNumber] DESC

