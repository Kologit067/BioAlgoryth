using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using BioAlgorythmModel.RepresentativesModel;
using Dapper;
using System;

namespace Representatives.Data
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativesRepository
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativesRepository
    {
        private string connectionString;
        public RepresentativesRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BioAlgorithm"].ConnectionString;
        }

        public void DeleteRepresentativeAlgorithmGroup(RepresentativeAlgorithmGroupDimension selectedAlgorithmGroup)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = $@"DELETE FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
WHERE [Algorithm] = '{selectedAlgorithmGroup.Algorithm}' AND [NumberOfSet] = {selectedAlgorithmGroup.NumberOfSet} 
AND [Dimension] = {selectedAlgorithmGroup.Dimension} AND [Step] = {selectedAlgorithmGroup.Step}
";
                db.Execute(sql);
            }
        }

        public void DeleteRepresentativeAlgorithm(RepresentativeAlgorithmGroup selectedAlgorithm)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = $@"DELETE FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
WHERE [Algorithm] = '{selectedAlgorithm.Algorithm}' 
";
                db.Execute(sql);
            }
        }
        public List<RepresentativeAlgorithmGroupDimension> GetRepresentativeAlgorithmGroupDimensions(string algorithmGroupListSort)
        {
            List<RepresentativeAlgorithmGroupDimension> algorithmGroups = new List<RepresentativeAlgorithmGroupDimension>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                algorithmGroups = db.Query<RepresentativeAlgorithmGroupDimension>(
                    $@"SELECT [Algorithm], [NumberOfSet], [Dimension], [Step], COUNT(*) as TotalCount, 
       SUM([NumberOfIteration]) as NumberOfIteration, SUM([Duration]) as TotalDuration,
	   SUM([Duration])/COUNT(*) as AverageDuration
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
GROUP BY [Algorithm], [NumberOfSet], [Dimension], [Step]
ORDER BY {algorithmGroupListSort}
").ToList();
            }
            return algorithmGroups;
        }

        public List<RepresentativeAlgorithWithDimension> GetRepresentativeAlgorithmWithDimensions()
        {
            List<RepresentativeAlgorithWithDimension> algorithmWithDimensions = new List<RepresentativeAlgorithWithDimension>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                algorithmWithDimensions = db.Query<RepresentativeAlgorithWithDimension>(
                    $@"SELECT [Algorithm], [NumberOfSet], [Dimension], [Step]
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
GROUP BY [Algorithm], [NumberOfSet], [Dimension], [Step]
").ToList();
            }
            return algorithmWithDimensions;
        }

        public List<RepresentativeAlgorithmGroup> GetRepresentativeAlgorithmGroups()
        {
            List<RepresentativeAlgorithmGroup> algorithmGroups = new List<RepresentativeAlgorithmGroup>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                algorithmGroups = db.Query<RepresentativeAlgorithmGroup>(
                    @"WITH CTE AS
(
SELECT [Algorithm], [NumberOfSet], [Dimension], COUNT(*) as cnt, SUM([NumberOfIteration]) as SumNumberOfIteration, SUM([Duration]) as SumDuration
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
GROUP BY [Algorithm], [NumberOfSet], [Dimension]
)
SELECT [Algorithm], 
	SUM(cnt) AS TotalCount, 
	COUNT(*) as CountByDimension, 
	SUM(SumNumberOfIteration) as NumberOfIteration, 
	SUM(SumDuration) as TotalDuration,
	SUM(SumDuration)/SUM(cnt) AS AverageDuration
FROM CTE
GROUP BY [Algorithm]").ToList();
            }
            return algorithmGroups;
        }

        public List<RepresentativesPerfomance> GetRepresentativePerformanceList(RepresentativesPerfomanceFilter representativesPerfomanceFilter, string order)
        {
            string top = "";
            if (representativesPerfomanceFilter.Top.HasValue)
            {
                top = $"TOP ({representativesPerfomanceFilter.Top})";
            }
            string where = "";
            List<string> whereList = new List<string>();
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.Algorithm))
            {
                whereList.Add($"[Algorithm] = '{representativesPerfomanceFilter.Algorithm}'");
            }
            if (representativesPerfomanceFilter.NumberOfSet.HasValue)
            {
                whereList.Add($"[NumberOfSet] = {representativesPerfomanceFilter.NumberOfSet}");
            }
            if (representativesPerfomanceFilter.Dimension.HasValue)
            {
                whereList.Add($"[Dimension] = {representativesPerfomanceFilter.Dimension}");
            }
            if (representativesPerfomanceFilter.Step.HasValue)
            {
                whereList.Add($"[Step] = {representativesPerfomanceFilter.Step}");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.InputLen))
            {
                whereList.Add($"[InputLen] = '{representativesPerfomanceFilter.InputLen}'");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceFilter.InputLenSort))
            {
                whereList.Add($"[InputLenSort] = '{representativesPerfomanceFilter.InputLenSort}'");
            }
            if (representativesPerfomanceFilter.NumberOfIterationFrom.HasValue)
            {
                whereList.Add($"[NumberOfIteration] >= {representativesPerfomanceFilter.NumberOfIterationFrom}");
            }
            if (representativesPerfomanceFilter.NumberOfIterationTo.HasValue)
            {
                whereList.Add($"[NumberOfIteration] <= {representativesPerfomanceFilter.NumberOfIterationTo}");
            }
            if (representativesPerfomanceFilter.DurationFrom.HasValue)
            {
                whereList.Add($"[Duration] >= {representativesPerfomanceFilter.DurationFrom}");
            }
            if (representativesPerfomanceFilter.DurationTo.HasValue)
            {
                whereList.Add($"[Duration] <= {representativesPerfomanceFilter.DurationTo}");
            }
            if (representativesPerfomanceFilter.CountTerminalFrom.HasValue)
            {
                whereList.Add($"[CountTerminal] >= {representativesPerfomanceFilter.CountTerminalFrom}");
            }
            if (representativesPerfomanceFilter.CountTerminalTo.HasValue)
            {
                whereList.Add($"[CountTerminal] <= {representativesPerfomanceFilter.CountTerminalTo}");
            }
            if (representativesPerfomanceFilter.BestValue.HasValue)
            {
                whereList.Add($"[BestValue] = {representativesPerfomanceFilter.BestValue}");
            }
            if (representativesPerfomanceFilter.IsComplete.HasValue)
            {
                whereList.Add($"[IsComplete] = {representativesPerfomanceFilter.IsComplete}");
            }
            if (whereList.Count > 0)
            {
                where = "WHERE " + string.Join(" AND ", whereList);
            }
            List<RepresentativesPerfomance> representativesPerfomances = new List<RepresentativesPerfomance>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                representativesPerfomances = db.Query<RepresentativesPerfomance>(
                    $@"SELECT {top} [RepresentativesPerfomanceId]
      ,[NumberOfSet]
      ,[Dimension]
      ,[Step]
      ,[InputLen]
      ,[InputLenSort]
      ,[InputLenAvg]
      ,[InputData]
      ,[InputDataShort]
      ,[Algorithm]
      ,[NumberOfIteration]
      ,[Duration]
      ,[DurationMilliSeconds]
      ,[DateComplete]
      ,[IsComplete]
      ,[LastRoute]
      ,[OptimalRoute]
      ,[CountTerminal]
      ,[BestValue]
      ,[UpdateOptcount]
      ,[ElemenationCount]
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance]
{where}
ORDER BY {order}").ToList();
            }
            return representativesPerfomances;
        }

        public List<RepresentativesPerfomanceCompare> GetRepresentativePerformanceCompareList(RepresentativesPerfomanceCompareFilter representativesPerfomanceCompareFilter)
        {
            string top = "";
            if (representativesPerfomanceCompareFilter.Top.HasValue)
            {
                top = $"TOP ({representativesPerfomanceCompareFilter.Top})";
            }
            string where = "";
            List<string> whereList = new List<string>();

            if (representativesPerfomanceCompareFilter.NumberOfSet.HasValue)
            {
                whereList.Add($"ra.[NumberOfSet] = {representativesPerfomanceCompareFilter.NumberOfSet}");
            }
            if (representativesPerfomanceCompareFilter.Dimension.HasValue)
            {
                whereList.Add($"ra.[Dimension] = {representativesPerfomanceCompareFilter.Dimension}");
            }
            if (representativesPerfomanceCompareFilter.Step.HasValue)
            {
                whereList.Add($"ra.[Step] = {representativesPerfomanceCompareFilter.Step}");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.BestValueCompare) && representativesPerfomanceCompareFilter.BestValueCompare != "N/A")
            {
                whereList.Add($"(ra.BestValue {representativesPerfomanceCompareFilter.BestValueCompare.Replace("1", "").Replace("2", "")} rb.BestValue OR ra.BestValue IS NULL OR rb.BestValue IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.NumberIterationCompare) && representativesPerfomanceCompareFilter.NumberIterationCompare != "N/A")
            {
                whereList.Add($"(ra.[NumberOfIteration] {representativesPerfomanceCompareFilter.NumberIterationCompare.Replace("1", "").Replace("2", "")} rb.[NumberOfIteration] OR ra.[NumberOfIteration] IS NULL OR rb.[NumberOfIteration] IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.DurationCompare) && representativesPerfomanceCompareFilter.DurationCompare != "N/A")
            {
                whereList.Add($"(ra.Duration {representativesPerfomanceCompareFilter.DurationCompare.Replace("1", "").Replace("2", "")} rb.Duration OR ra.Duration IS NULL OR rb.Duration IS NULL )");
            }
            if (!string.IsNullOrWhiteSpace(representativesPerfomanceCompareFilter.ElemenationCountCompare) && representativesPerfomanceCompareFilter.ElemenationCountCompare != "N/A")
            {
                whereList.Add($"(ra.ElemenationCount {representativesPerfomanceCompareFilter.ElemenationCountCompare.Replace("1", "").Replace("2", "")} rb.ElemenationCount OR ra.ElemenationCount IS NULL OR rb.ElemenationCount IS NULL )");
            }
            if (whereList.Count > 0)
            {
                where = "WHERE " + string.Join(" AND ", whereList);
            }
            List<RepresentativesPerfomanceCompare> representativesPerfomancesCompare = new List<RepresentativesPerfomanceCompare>();
            string query = $@"SELECT ra.[NumberOfSet], ra.[Dimension], ra.[InputData], ra.[InputDataShort], ra.Step,
       ra.Algorithm as Algorithm1, rb.Algorithm as Algorithm2, 
       ra.BestValue as BestValue1, rb.BestValue as BestValue2, 
	   ra.OptimalRoute as OptimalRoute1, rb.OptimalRoute as OptimalRoute2,
	   ra.[NumberOfIteration] as NumberOfIteration1, rb.[NumberOfIteration] as NumberOfIteration2,
       ra.[Duration] as Duration1, rb.[Duration] as Duration2,
       ra.ElemenationCount as ElemenationCount1, rb.ElemenationCount as ElemenationCount2
FROM [BioAlgorithm].[dbo].[RepresentativesPerfomance] AS ra
INNER JOIN [BioAlgorithm].[dbo].[RepresentativesPerfomance] rb
ON (ra.InputData = rb.InputData AND ra.[NumberOfSet] = rb.[NumberOfSet] AND ra.[Dimension] = rb.[Dimension] AND
ra.[Algorithm] = '{representativesPerfomanceCompareFilter.Algorithm1}' AND rb.[Algorithm] = '{representativesPerfomanceCompareFilter.Algorithm2}') 
{where}
";

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                representativesPerfomancesCompare = db.Query<RepresentativesPerfomanceCompare>(query).ToList();
            }
            return representativesPerfomancesCompare;
        }
    }
}
