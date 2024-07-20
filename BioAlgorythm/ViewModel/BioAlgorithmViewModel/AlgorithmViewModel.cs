using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Representatives;

namespace BioAlgorithmViewModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class AlgorithmViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class AlgorithmViewModel : ViewModelBase
    {
        private RepresentativesViewModel representatives;
        public RepresentativesViewModel Representatives
        {
            get
            {
                return representatives;
            }
            set
            {
                representatives = value;
                OnPropertyChanged(nameof(Representatives));
            }
        }
        public AlgorithmViewModel()
        {
            Representatives = new RepresentativesViewModel();
        }

    }
}
