using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Savers
{
    public class DNAMappingSaver 
    {
        private string _connectionString;
        public DNAMappingSaver()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bioalgorythm"].ConnectionString;
        }
        public string Save(List<DNAMappingPerfomance> dnaMappingPerfomances)
        {
            string error = null;

            try
            {
                DataTable performance = new DataTable();
                performance.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Size", System.Type.GetType("System.Int32"));
                performance.Columns.Add("InputData", System.Type.GetType("System.String"));
                performance.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));
                performance.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));
                performance.Columns.Add("IsComplete", System.Type.GetType("System.Boolean"));
                performance.Columns.Add("LastRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("OptimalRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("CountTerminal", System.Type.GetType("System.Int64"));
                performance.Columns.Add("UpdateOptcount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("ElemenationCount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("IsAllResult", System.Type.GetType("System.Boolean"));

                DataTable solutions = new DataTable();
                solutions.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));

                int number = 0;
                foreach (var ps in dnaMappingPerfomances)
                {
                    performance.Columns.Add("InputData", System.Type.GetType("System.String"));
                    performance.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));
                    performance.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                    performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));
                    performance.Columns.Add("IsComplete", System.Type.GetType("System.Boolean"));
                    performance.Columns.Add("LastRoute", System.Type.GetType("System.String"));
                    performance.Columns.Add("OptimalRoute", System.Type.GetType("System.String"));
                    performance.Columns.Add("CountTerminal", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("UpdateOptcount", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("ElemenationCount", System.Type.GetType("System.Int64"));
                    performance.Columns.Add("IsAllResult", System.Type.GetType("System.Boolean"));
                    performance.Rows.Add(number, ps.Size, ps.InputData, ps.OutputPresentation, ps.Algorithm, 
                        ps.IterationCount, ps.Duration, ps.DurationMilliSeconds, ps.DateComplete, ps.IsComplete,
                        ps.LastRoute, ps.OptimalRoute, ps.CountTerminal, ps.UpdateOptcount, ps.ElemenationCount,
                        ps.AlgorythmParameters.IsAllResult);

                    for (int i = 0; i < ps.ListOfSolution.Count; i++)
                    {
                        solutions.Rows.Add(number, string.Join(",", ps.ListOfSolution[i]));
                    }

                    number++;
                }

                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addDNAMappingPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@DNAMappingPerfomances", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.DNAMappingPerfomanceType";
                    SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@DNAMappingSolutions", solutions);
                    tvpParam2.SqlDbType = SqlDbType.Structured;
                    tvpParam2.TypeName = "dbo.DNAMappingSolutionType";
                    addCommand.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return error;

        }
    }
}
