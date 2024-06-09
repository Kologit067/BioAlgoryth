using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioAlgorythmModel.RepresentativesModel;
using Dapper;

namespace Representatives.Data
{
    public class RepresentativesRepository
    {
        private string connectionString;
        public RepresentativesRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BioAlgorithm"].ConnectionString;
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
    }
}
