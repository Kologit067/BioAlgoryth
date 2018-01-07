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
        public void Save(List<RegulatoryMotifPerfomance> regulatoryMotifPerfomances)
        {
            string error = null;

            try
            {
                DataTable performance = new DataTable();
                performance.Columns.Add("NumberInArray", System.Type.GetType("System.Int32"));
                performance.Columns.Add("Size", System.Type.GetType("System.Int32"));
                performance.Columns.Add("NumberOdSequence", System.Type.GetType("System.Int32"));
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

                foreach (var ps in regulatoryMotifPerfomances)
                {
                    performance.Rows.Add(ps.DBName, ps.Tables, ps.Algorithm, ps.NumberOfIteration, ps.Duration,
                        ps.DurationMilliSeconds, ps.DateComplete, ps.IsComplete, ps.ElementCount, ps.TableSetAsNumber,
                        ps.LastRoute, ps.CountTerminal, ps.UpdateOptcount, ps.OptimalValue, ps.ElemenationCount, ps.OptimalRoute);

                    foreach (var sl in ps.)
                    number++;
                }

 

                SqlConnection connection = new SqlConnection(connectionStringDTD);
                connection.Open();
                try
                {
                    SqlCommand addCommand = new SqlCommand("addAlgorithmPerfomance", connection);
                    addCommand.CommandType = CommandType.StoredProcedure;
                    addCommand.CommandTimeout = 300;
                    SqlParameter tvpParam = addCommand.Parameters.AddWithValue("@AlgorithmPerfomances", performance);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.AlgorithmPerfomanceType";
                    SqlParameter tvpParam1 = addCommand.Parameters.AddWithValue("@AlgorithmOptimalValueChange", valueChanges);
                    tvpParam1.SqlDbType = SqlDbType.Structured;
                    tvpParam1.TypeName = "dbo.AlgorithmOptimalValueChangeType";
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
