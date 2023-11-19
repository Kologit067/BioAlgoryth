using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BioAlgorythmViewModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteGraphEdgeViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphEdgeViewModel : BipartiteGraphElementViewModel
    {
        private double rightPointX;
        private double rightPointY;
        private double leftPointX;
        private double leftPointY;
        //----------------------------------------------------------------------------------------------------------------------
        public PointCollection Points
        {
            get
            {
                PointCollection points = new PointCollection();
                points.Add(new Point(leftPointX, leftPointY));
                points.Add(new Point(rightPointX, rightPointY));
                return points;
            }
        }
        public BipartiteGraphEdgeViewModel(double leftPointX, double leftPointY, double rightPointX, double rightPointY)
        {
            this.rightPointX = rightPointX;
            this.rightPointY = rightPointY;
            this.leftPointX = leftPointX; 
            this.leftPointY = leftPointY;
        }
        //----------------------------------------------------------------------------------------------------------------------
        public override void NotifyUI()
        {
            OnPropertyChanged(nameof(Points));
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
