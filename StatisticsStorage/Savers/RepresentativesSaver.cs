using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using StatisticsStorage.Accumulators.Objects;

namespace StatisticsStorage.Savers
{
    public class RepresentativesSaver
    {
        private string _connectionString;
        public RepresentativesSaver()
        {
 //           _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bioalgorythm"].ConnectionString;
            _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=BioAlgorythm;Integrated Security=true";
        }

        public string Save(List<RepresentativesPerfomance> representativesPerfomances)
        {
            string error = null;

            try
            {

                DataTable performance = new DataTable();
                performance.Columns.Add("NumberOfSet", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Dimension", System.Type.GetType("System.Int32"));
                performance.Columns.Add("InputLen", System.Type.GetType("System.String"));
                performance.Columns.Add("InputLenSort", System.Type.GetType("System.String"));
                performance.Columns.Add("InputLenAvg", System.Type.GetType("System.Decimal"));
                performance.Columns.Add("InputData", System.Type.GetType("System.String"));
                performance.Columns.Add("InputDataShort", System.Type.GetType("System.String"));
                performance.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));
                performance.Columns.Add("IsComplete", System.Type.GetType("System.Boolean"));
                performance.Columns.Add("LastRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("OptimalRoute", System.Type.GetType("System.String"));
                performance.Columns.Add("CountTerminal", System.Type.GetType("System.Int64"));
                performance.Columns.Add("BestValue", System.Type.GetType("System.Int64"));
                performance.Columns.Add("UpdateOptcount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("ElemenationCount", System.Type.GetType("System.Int64"));

                DataTable solutions = new DataTable();
                solutions.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                solutions.Columns.Add("Dimension", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("Limit", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("InputData", System.Type.GetType("System.String"));
                solutions.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));

                foreach (var ps in representativesPerfomances)
                {
                    performance.Rows.Add(
                        ps.NumberOfSet, 
                        ps.Dimension, 
                        ps.InputLen,
                        ps.InputLenSort,
                        (decimal)ps.InputLenAvg,
                        ps.InputData,
                        ps.InputDataShort, 
                        ps.Algorithm,
                        ps.IterationCount, 
                        ps.Duration, 
                        ps.DurationMilliSeconds, 
                        ps.DateComplete, 
                        ps.IsComplete,
                        ps.LastRoute,
                        ps.OptimalRoute,
                        ps.CountTerminal, 
                        ps.BestValue, 
                        ps.UpdateOptcount, 
                        ps.ElemenationCount);

                    for (int i = 0; i < ps.OptimalSets.Count; i++)
                    {
                        solutions.Rows.Add(ps.Algorithm, ps.NumberOfSet, ps.Dimension, ps.InputData, ps.OptimalSets[i]);
                    }

                }

                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addRepresentativesPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@RepresentativesPerfomances", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.RepresentativesPerfomanceType";
                    SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@RepresentativesSolutions", solutions);
                    tvpParam2.SqlDbType = SqlDbType.Structured;
                    tvpParam2.TypeName = "dbo.RepresentativesSolutionType";
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

        public string Delete(string algorithm, int? numberOfSet = null, int? dimension = null)
        {
            string error = null;


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                SqlCommand addCommand = new SqlCommand("dbo.deleteRepresentativesPerfomance", connection);
                addCommand.CommandType = CommandType.StoredProcedure;
                addCommand.CommandTimeout = 300;
                SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@Dimension", dimension);
                tvpParam2.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam3 = addCommand.Parameters.AddWithValue("@Algorithm", algorithm);
                tvpParam3.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@NumberOfSet", numberOfSet);
                tvpParam.SqlDbType = SqlDbType.VarChar;
                addCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                connection.Close();
            }
            return error;

        }

    }
}
