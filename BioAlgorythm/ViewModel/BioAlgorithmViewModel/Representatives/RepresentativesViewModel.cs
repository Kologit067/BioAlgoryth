using BioAlgorithmViewModel.BipartiteGraphModel;
using BioAlgorithmViewModel.Common;
using BioAlgorithmViewModel.Mappings;
using BioAlgorithmViewModel.Representatives.Messages;
using BioAlgorithmViewModel.Representatives.Utility;
using Representatives.Data;
using System;

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
        private int selectedTab;
        public int SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                selectedTab = value;
                OnPropertyChanged(nameof(SelectedTab));
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
            Messenger.Default.Register<RepresentativeTabChangeMessage>(this, OnAlgorithmGroupToFilterMessageReceived, typeof(RepresentativeTabChangeMessage));
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void OnAlgorithmGroupToFilterMessageReceived(RepresentativeTabChangeMessage message)
        {
            SelectedTab = message.RepresentativeTabName switch
            {
                "RepresentativePerformance" => 0,
                "BipartiteGraph" => 3,
                _ => 0
            };
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
