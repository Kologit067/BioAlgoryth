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
                    performance.Rows.Add(ps.Size, ps.NumberOfSequence, ps.SequenceLengthes,
                        ps.MotifLength, ps.InputData, ps.OutputPresentation, ps.Algorithm, ps.IterationCount,
                        ps.Duration, ps.DurationMilliSeconds, ps.DateComplete, ps.IsComplete,
                        ps.LastRoute, ps.OptimalRoute, ps.OptimalValue,
                        (ps.SolutionStartPositionList?.Count ?? 0) == 0 ? "" : string.Join(",", ps.SolutionStartPositionList[0]),
                        (ps.ListOfMotif?.Count ?? 0) == 0 ? "" : string.Join("", ps.ListOfMotif[0]),
                        ps.CountTerminal, ps.UpdateOptcount, ps.ElemenationCount,
                        ps.AlgorythmParameters.IsOptimizitaion, ps.AlgorythmParameters.IsSumAsCriteria, ps.AlgorythmParameters.IsAllResult, ps.AlgorythmParameters.AcceptibleDistance);

                    for (int i = 0; i < (ps.ListOfMotif?.Count ?? 0); i++)
                    {
                        solutions.Rows.Add(ps.Algorithm, ps.SequenceLengthes,
                        ps.MotifLength, ps.AlgorythmParameters.IsOptimizitaion,
                        ps.AlgorythmParameters.IsSumAsCriteria,
                        ps.AlgorythmParameters.IsAllResult, ps.AlgorythmParameters.AcceptibleDistance,
                        ps.InputData,
                        string.Join(",", ps.SolutionStartPositionList[i]),
                        string.Join("", ps.ListOfMotif[i]));
                    }

                    if (ps.RegulatoryMotifOptimalValueChanges != null)
                        foreach (var ch in ps.RegulatoryMotifOptimalValueChanges)
                        {
                            valueChanges.Rows.Add(ps.Algorithm, ps.SequenceLengthes,
                            ps.MotifLength, ps.AlgorythmParameters.IsOptimizitaion,
                            ps.AlgorythmParameters.IsSumAsCriteria, ps.AlgorythmParameters.IsAllResult,
                            ps.AlgorythmParameters.AcceptibleDistance,
                            ps.InputData, ch.IterationCount, ch.Duration, ch.DurationMilliSeconds,
                            ch.OptimalValue, ch.StartPosition, ch.Motif);
                        }
                }



                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addRegulatoryMotifPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@RegulatoryMotifPerfomances", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.RegulatoryMotifPerfomanceType";
                    SqlParameter tvpParam1 = addCommand.Parameters.AddWithValue("@RegulatoryMotifOptimalValueChanges", valueChanges);
                    tvpParam1.SqlDbType = SqlDbType.Structured;
                    tvpParam1.TypeName = "dbo.RegulatoryMotifOptimalValueChangeType";
                    SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@RegulatoryMotifSolutions", solutions);
                    tvpParam2.SqlDbType = SqlDbType.Structured;
                    tvpParam2.TypeName = "dbo.RegulatoryMotifSolutionType";
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
                SqlCommand addCommand = new SqlCommand("[dbo].[deleteRegulatoryMotifPerfomance]", connection);
                addCommand.CommandType = CommandType.StoredProcedure;
                addCommand.CommandTimeout = 300;
                SqlParameter tvpParam2 = addCommand.Parameters.AddWithValue("@SequenceLengthes", sequenceLengthes);
                tvpParam2.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam3 = addCommand.Parameters.AddWithValue("@Algorithm", algorythm);
                tvpParam3.SqlDbType = SqlDbType.VarChar;
                SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@MotifLength", patternLength);
                tvpParam.SqlDbType = SqlDbType.VarChar;
                addCommand.Parameters.AddWithValue("@IsOptimizitaion", isOptimizitaion).SqlDbType = SqlDbType.Bit;
                addCommand.Parameters.AddWithValue("@IsSumAsCriteria", isSumAsCriteria).SqlDbType = SqlDbType.Bit;
                addCommand.Parameters.AddWithValue("@IsAllResult", isAllResult).SqlDbType = SqlDbType.Bit;
                addCommand.Parameters.AddWithValue("@AcceptibleDistance", acceptibleDistance).SqlDbType = SqlDbType.Int;

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
