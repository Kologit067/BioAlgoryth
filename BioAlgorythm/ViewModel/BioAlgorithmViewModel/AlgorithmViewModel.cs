using BioAlgorithmViewModel.BipartiteGraphModel;
using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Representatives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
