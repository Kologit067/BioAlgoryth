﻿using CommonLibrary.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsStorage.Accumulators
{
    //--------------------------------------------------------------------------------------
    // class DNAMappingPerfomance
    //--------------------------------------------------------------------------------------
    public class DNAMappingPerfomance : StatisyicsPerfomance
    {
        //--------------------------------------------------------------------------------------
        public DNAMappingPerfomance(int size, string inputData, string algorithm, AlgorythmParameters algorythmParameters) : base(size, inputData, algorithm)
        {
        }
        //--------------------------------------------------------------------------------------------------------------------
        protected List<List<int>> _listOfSolution;
        public List<List<int>> ListOfSolution
        {
            get
            {
                return _listOfSolution;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        protected AlgorythmParameters _algorythmParameters;
        public AlgorythmParameters AlgorythmParameters
        {
            get
            {
                return _algorythmParameters;
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveStatisticData(string outputPresentation, long duration, long durationMilliSeconds, DateTime dateComplete,
            bool isComplete, string lastRoute, string optimalRoute, List<List<int>> listOfSolution)
        {
            SaveStatisticData(outputPresentation, duration, durationMilliSeconds, dateComplete,
            isComplete, lastRoute, optimalRoute);
            _listOfSolution = listOfSolution;
        }
        //--------------------------------------------------------------------------------------
    }
    //--------------------------------------------------------------------------------------
}
