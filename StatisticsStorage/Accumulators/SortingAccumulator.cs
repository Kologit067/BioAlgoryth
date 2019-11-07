using BaseContract;
using StatisticsStorage.Savers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------------------------------------
    //  class SortingAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class SortingAccumulator : ISortingAccumulator
    {
        public static readonly int SolutionLimit = 20;
        protected SortingSaver _sortingSaver;
        protected int _size;
        protected string _algorythm;
        //--------------------------------------------------------------------------------------------------------------------
        public SortingAccumulator(SortingSaver sortingSaver, string algorythm,  int size)
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void CreateStatistics(string text, string pattern)
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void IterationCountInc(int count = 1)
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void NumberOfComparisonInc(int count = 1)
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo)
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void RemoveStatisticData()
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveRemain()
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public string Delete()
        {
            throw new NotImplementedException();
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------
    //  class FakeSortingAccumulator
    //--------------------------------------------------------------------------------------------------------------------
    public class FakeSortingAccumulator : ISortingAccumulator
    {
        //--------------------------------------------------------------------------------------------------------------------
        public FakeSortingAccumulator()
        {
        }
        public void IterationCountInc(int count = 1)
        { }
        public void NumberOfComparisonInc(int count = 1)
        { }
        public void CreateStatistics(string text, string pattern) { }
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete, string additionalInfo) { }
        public void SaveRemain() { }
        public string Delete()
        {
            return string.Empty;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void RemoveStatisticData()
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------------------------------------

}
