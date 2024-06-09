using BioAlgorithmModel.BipartiteGraphModel;
using BioAlgorithmViewModel.BipartiteGraphModel;
using BioAlgorithmViewModel.Common;
using BioAlgorythmModel.RepresentativesModel;
using Representatives.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private ObservableCollection<RepresentativeAlgorithmGroup> representativeAlgorithmGroups;
        public ObservableCollection<RepresentativeAlgorithmGroup> RepresentativeAlgorithmGroups
        {
            get
            {
                return representativeAlgorithmGroups;
            }
            set
            {
                representativeAlgorithmGroups = value;
                OnPropertyChanged(nameof(RepresentativeAlgorithmGroups));
            }
        }
        public RepresentativesViewModel()
        {
            RepresentativeAlgorithmGroups = new ObservableCollection<RepresentativeAlgorithmGroup>();
            representativesRepository = new RepresentativesRepository();
        }
        //----------------------------------------------------------------------------------------------------------------------
        private ICommand refreshRepresentativeAlgorithmListCommand;
        public ICommand RefreshRepresentativeAlgorithmListCommand
        {
            get
            {
                if (refreshRepresentativeAlgorithmListCommand == null)
                {
                    refreshRepresentativeAlgorithmListCommand = new DelegateCommand(RefreshRepresentativeAlgorithmListAction, CanRefreshRepresentativeAlgorithmListAction);
                }
                return refreshRepresentativeAlgorithmListCommand;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private void RefreshRepresentativeAlgorithmListAction()
        {
            RepresentativeAlgorithmGroups.Clear();
            List<RepresentativeAlgorithmGroup> algorithms = representativesRepository.GetRepresentativeAlgorithmGroups();
            foreach(RepresentativeAlgorithmGroup a in algorithms)
                RepresentativeAlgorithmGroups.Add(a);
        }
        //----------------------------------------------------------------------------------------------------------------------
        private bool CanRefreshRepresentativeAlgorithmListAction()
        {
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
