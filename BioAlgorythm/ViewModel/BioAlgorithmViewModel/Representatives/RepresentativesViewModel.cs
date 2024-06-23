using BioAlgorithmViewModel.BipartiteGraphModel;
using BioAlgorithmViewModel.Common;
using BioAlgorythmModel.RepresentativesModel;
using Representatives.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BioAlgorithmViewModel.Representatives
{
    //----------------------------------------------------------------------------------------------------------------------
    // class RepresentativesViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class RepresentativesViewModel : ViewModelBase
    {
        private RepresentativesRepository representativesRepository;
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativePerformanceViewModel representativePerformance;
        public RepresentativePerformanceViewModel RepresentativePerformance
        {
            get
            {
                return representativePerformance;
            }
            set
            {
                representativePerformance = value;
                OnPropertyChanged(nameof(RepresentativePerformance));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativePerformanceAlgorithmViewModel representativePerformanceAlgorithm;
        public RepresentativePerformanceAlgorithmViewModel RepresentativePerformanceAlgorithm
        {
            get
            {
                return representativePerformanceAlgorithm;
            }
            set
            {
                representativePerformanceAlgorithm = value;
                OnPropertyChanged(nameof(RepresentativePerformanceAlgorithm));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private RepresentativePerformanceGroupViewModel representativePerformanceGroup;
        public RepresentativePerformanceGroupViewModel RepresentativePerformanceGroup
        {
            get
            {
                return representativePerformanceGroup;
            }
            set
            {
                representativePerformanceGroup = value;
                OnPropertyChanged(nameof(RepresentativePerformanceGroup));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private BipartiteGraphViewModel bipartiteGraph;
        public BipartiteGraphViewModel BipartiteGraph
        {
            get
            {
                return bipartiteGraph;
            }
            set
            {
                bipartiteGraph = value;
                OnPropertyChanged(nameof(BipartiteGraph));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public RepresentativesViewModel()
        {
            representativesRepository = new RepresentativesRepository();
            RepresentativePerformance = new RepresentativePerformanceViewModel(representativesRepository);
            RepresentativePerformanceAlgorithm = new RepresentativePerformanceAlgorithmViewModel(representativesRepository);
            RepresentativePerformanceGroup = new RepresentativePerformanceGroupViewModel(representativesRepository);
            BipartiteGraph = new BipartiteGraphViewModel();
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
