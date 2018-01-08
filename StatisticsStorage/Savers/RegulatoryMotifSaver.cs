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
    public class RegulatoryMotifSaver
    {
        private string _connectionString;
        public RegulatoryMotifSaver()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bioalgorythm"].ConnectionString;
        }
        public string Save(List<RegulatoryMotifPerfomance> regulatoryMotifPerfomances)
        {
            string error = null;

            try
            {
                DataTable performance = new DataTable();
                performance.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Size", System.Type.GetType("System.Int32"));
                performance.Columns.Add("NumberOfSequence", System.Type.GetType("System.Int32"));
                performance.Columns.Add("averageSequenceLength", System.Type.GetType("System.Int32"));
                performance.Columns.Add("MotifLength", System.Type.GetType("System.Int32"));
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
                performance.Columns.Add("OptimalValue", System.Type.GetType("System.Int64"));
                performance.Columns.Add("StartPosition", System.Type.GetType("System.String"));
                performance.Columns.Add("Motif", System.Type.GetType("System.String"));
                performance.Columns.Add("CountTerminal", System.Type.GetType("System.Int64"));
                performance.Columns.Add("UpdateOptcount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("ElemenationCount", System.Type.GetType("System.Int64"));
                performance.Columns.Add("IsOptimizitaion", System.Type.GetType("System.Boolean"));
                performance.Columns.Add("IsSumAsCriteria", System.Type.GetType("System.Boolean"));
                performance.Columns.Add("IsAllResult", System.Type.GetType("System.Boolean"));

                DataTable valueChanges = new DataTable();
                valueChanges.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                valueChanges.Columns.Add("NumberOfIteration", System.Type.GetType("System.Int64"));
                valueChanges.Columns.Add("Duration", System.Type.GetType("System.Int64"));
                valueChanges.Columns.Add("DurationMilliSeconds", System.Type.GetType("System.Int64"));
                valueChanges.Columns.Add("OptimalValue", System.Type.GetType("System.Int64"));
                valueChanges.Columns.Add("StartPosition", System.Type.GetType("System.String"));
                valueChanges.Columns.Add("Motif", System.Type.GetType("System.String"));

                DataTable solutions = new DataTable();
                solutions.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                solutions.Columns.Add("StartPosition", System.Type.GetType("System.String"));
                solutions.Columns.Add("Motif", System.Type.GetType("System.String"));

                int number = 0;
                foreach (var ps in regulatoryMotifPerfomances)
                {
                    performance.Rows.Add(number, ps.Size, ps.NumberOfSequence, ps.AverageSequenceLength, 
                        ps.MotifLength, ps.InputData, ps.OutputPresentation, ps.Algorithm, ps.IterationCount, 
                        ps.Duration, ps.DurationMilliSeconds, ps.DateComplete, ps.IsComplete, 
                        ps.LastRoute, ps.OptimalRoute, ps.OptimalValue, ps.SolutionStartPositionList[0],
                        ps.ListOfMotif[0], ps.CountTerminal, ps.UpdateOptcount, ps.ElemenationCount, 
                        ps.AlgorythmParameters.IsOptimizitaion, ps.AlgorythmParameters.IsSumAsCriteria, ps.AlgorythmParameters.IsAllResult);

                    for (int i = 0; i < ps.ListOfMotif.Count; i++)
                    {
                        solutions.Rows.Add(number,ps.SolutionStartPositionList[i], ps.ListOfMotif[i]);
                    }

                    foreach ( var ch in ps.RegulatoryMotifOptimalValueChanges)
                    {
                        valueChanges.Rows.Add(number, ch.IterationCount, ch.Duration, ch.DurationMilliSeconds,
                            ch.OptimalValue, ch.StartPosition, ch.Motif);
                    }
                    number++;
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
    }
}
