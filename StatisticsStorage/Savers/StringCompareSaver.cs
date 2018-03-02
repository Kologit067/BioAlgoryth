using StatisticsStorage.Accumulators;
using StatisticsStorage.Accumulators.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StatisticsStorage.Savers
{
    public class StringCompareSaver
    {
        private string _connectionString;
        public StringCompareSaver()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bioalgorythm"].ConnectionString;
        }
        public string Save(List<FindPatternPerfomance> findPatternPerfomances)
        {
            string error = null;

            try
            {
                DataTable performance = new DataTable();
                performance.Columns.Add("Algorithm", System.Type.GetType("System.String"));
                performance.Columns.Add("TextSize", System.Type.GetType("System.Int32"));
                performance.Columns.Add("PatternSize", System.Type.GetType("System.Int32"));
                performance.Columns.Add("AlphabetSize", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Text", System.Type.GetType("System.String"));
                performance.Columns.Add("Pattern", System.Type.GetType("System.String"));
                performance.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));
                performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("NumberOfComparison", System.Type.GetType("System.Int64"));
                performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));
                performance.Columns.Add("AdditionalInfo", System.Type.GetType("System.String"));


                foreach (var ps in findPatternPerfomances)
                {

                    performance.Rows.Add(ps.Algorithm, ps.TextSize, ps.PatternSize, ps.AlphabetSize,
                        ps.Text, ps.Pattern, ps.OutputPresentation, ps.IterationCount, ps.NumberOfComparison,
                        ps.Duration, ps.DurationMilliSeconds, ps.DateComplete, ps.AdditionalInfo);

                }



                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addFindPatternPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@FindPatternPerfomanceType", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.FindPatternPerfomanceType";
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

        public string Delete(string algorythm, int patternLength, int textLength, int alphabetSize)
        {
            string error = null;


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                SqlCommand addCommand = new SqlCommand("[dbo].[deleteFindPatternPerfomance]", connection);
                addCommand.CommandType = CommandType.StoredProcedure;
                addCommand.CommandTimeout = 1800;
                SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@Algorithm", algorythm);
                tvpParam2.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam3 = addCommand.Parameters.AddWithValue("@TextSize", textLength);
                tvpParam3.SqlDbType = SqlDbType.Int;
                SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@PatternSize", patternLength);
                tvpParam.SqlDbType = SqlDbType.Int;
                SqlParameter tvpParam4 = addCommand.Parameters.AddWithValue("@AlphabetSize", alphabetSize);
                tvpParam4.SqlDbType = SqlDbType.Int;

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
