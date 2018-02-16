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
                performance.Columns.Add("Text", System.Type.GetType("System.String"));
                performance.Columns.Add("Pattern", System.Type.GetType("System.String"));
                performance.Columns.Add("OutputPresentation", System.Type.GetType("System.String"));
                performance.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                performance.Columns.Add("DateComplete", System.Type.GetType("System.DateTime"));


                foreach (var ps in findPatternPerfomances)
                {

                    performance.Rows.Add(ps.Algorithm, ps.TextSize, ps.PatternSize,
                        ps.Text, ps.Pattern, ps.OutputPresentation, ps.IterationCount,
                        ps.Duration, ps.DurationMilliSeconds, ps.DateComplete);

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

        public string Delete(string algorythm, int patternLength, int textLength)
        {
            string error = null;


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            try
            {
                SqlCommand addCommand = new SqlCommand("[dbo].[deleteFindPatternPerfomance]", connection);
                addCommand.CommandType = CommandType.StoredProcedure;
                addCommand.CommandTimeout = 300;
                SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@Algorithm", algorythm);
                tvpParam2.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam3 = addCommand.Parameters.AddWithValue("@TextSize", patternLength);
                tvpParam3.SqlDbType = SqlDbType.Int;
                SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@PatternSize", textLength);
                tvpParam.SqlDbType = SqlDbType.Int;

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
