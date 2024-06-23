using BioAlgorithmViewModel.Common;

namespace BioAlgorithmViewModel.Representatives
{
    public class RepresentativesPerfomanceFilterViewModel : ViewModelBase
    {
        private string algorithm;
        public string Algorithm
        {
            get
            {
                return algorithm;
            }
            set
            {
                algorithm = value;
                OnPropertyChanged(nameof(Algorithm));
            }
        }
        private int? numberOfSet;
        public int? NumberOfSet
        {
            get
            {
                return numberOfSet;
            }
            set
            {
                numberOfSet = value;
                OnPropertyChanged(nameof(NumberOfSet));
            }
        }
        private int? dimension;
        public int? Dimension
        {
            get
            {
                return dimension;
            }
            set
            {
                dimension = value;
                OnPropertyChanged(nameof(Dimension));
            }
        }
        private long? step;
        public long? Step
        {
            get
            {
                return step;
            }
            set
            {
                step = value;
                OnPropertyChanged(nameof(Step));
            }
        }
        private string inputLen;
        public string InputLen
        {
            get
            {
                return inputLen;
            }
            set
            {
                inputLen = value;
                OnPropertyChanged(nameof(InputLen));
            }
        }
        private string inputLenSort;
        public string InputLenSort
        {
            get
            {
                return inputLenSort;
            }
            set
            {
                inputLenSort = value;
                OnPropertyChanged(nameof(InputLenSort));
            }
        }
        private long? numberOfIterationFrom;
        public long? NumberOfIterationFrom
        {
            get
            {
                return numberOfIterationFrom;
            }
            set
            {
                numberOfIterationFrom = value;
                OnPropertyChanged(nameof(NumberOfIterationFrom));
            }
        }
        private long? numberOfIterationTo;
        public long? NumberOfIterationTo
        {
            get
            {
                return numberOfIterationTo;
            }
            set
            {
                numberOfIterationTo = value;
                OnPropertyChanged(nameof(NumberOfIterationTo));
            }
        }
        private long? durationFrom;
        public long? DurationFrom
        {
            get
            {
                return durationFrom;
            }
            set
            {
                durationFrom = value;
                OnPropertyChanged(nameof(DurationFrom));
            }
        }
        private long? durationTo;
        public long? DurationTo
        {
            get
            {
                return durationTo;
            }
            set
            {
                durationTo = value;
                OnPropertyChanged(nameof(DurationTo));
            }
        }
        private bool? isComplete;
        public bool? IsComplete
        {
            get
            {
                return isComplete;
            }
            set
            {
                isComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }
 
        private int? countTerminalFrom;
        public int? CountTerminalFrom
        {
            get
            {
                return countTerminalFrom;
            }
            set
            {
                countTerminalFrom = value;
                OnPropertyChanged(nameof(CountTerminalFrom));
            }
        }
        private int? countTerminalTo;
        public int? CountTerminalTo
        {
            get
            {
                return countTerminalTo;
            }
            set
            {
                countTerminalTo = value;
                OnPropertyChanged(nameof(CountTerminalTo));
            }
        }
        private int? bestValue;
        public int? BestValue
        {
            get
            {
                return bestValue;
            }
            set
            {
                bestValue = value;
                OnPropertyChanged(nameof(BestValue));
            }
        }
        public RepresentativesPerfomanceFilterViewModel()
        {

        }
    }
}
