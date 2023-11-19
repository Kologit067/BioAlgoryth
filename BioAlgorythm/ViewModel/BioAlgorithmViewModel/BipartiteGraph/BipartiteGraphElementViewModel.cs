using BioAlgorythmViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BioAlgorythmViewModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteGraphElementViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphElementViewModel : ViewModelBase
    {
        public static SolidColorBrush commonColor;
        public static SolidColorBrush selectedColor;
        protected bool isSelected;
        //--------------------------------------------------------------------------------------
        static BipartiteGraphElementViewModel()
        {
            commonColor = new SolidColorBrush();
            commonColor.Color = Colors.Silver;
            selectedColor = new SolidColorBrush();
            selectedColor.Color = Colors.White;
        }
        //--------------------------------------------------------------------------------------
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                OnPropertyChanged(nameof(StrokeThickness));
                OnPropertyChanged(nameof(RectangleFill));
            }
        }

        //--------------------------------------------------------------------------------------
        public int StrokeThickness
        {
            get { return IsSelected ? 2 : 0; }
            set { }
        }

        //--------------------------------------------------------------------------------------
        public SolidColorBrush RectangleFill
        {
            get
            {
                var c = IsSelected ? selectedColor : commonColor;
                return c;
            }
            set { }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public virtual void NotifyUI()
        {
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
