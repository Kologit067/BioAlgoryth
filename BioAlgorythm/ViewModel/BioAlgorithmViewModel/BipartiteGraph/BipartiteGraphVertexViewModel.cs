using BioAlgorythmModel.BipartiteGraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioAlgorythmViewModel.BipartiteGraphModel
{
    //----------------------------------------------------------------------------------------------------------------------
    // class BipartiteGraphVertexViewModel
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphVertexViewModel : BipartiteGraphElementViewModel
    {
        private int number;
        public List<int> AdjacentVertices { get; set; }
        //----------------------------------------------------------------------------------------------------------------------
        public string AdjacentsAsString
        {
            get
            {
                return string.Join(",", AdjacentVertices);
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double centerX;
        public double CenterX
        {
            get
            {
                return centerX;
            }
            set
            {
                centerX = value;
                OnPropertyChanged(nameof(CenterX));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double centerY;
        public double CenterY
        {
            get
            {
                return centerY;
            }
            set
            {
                centerY = value;
                OnPropertyChanged(nameof(CenterY));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double radius;
        public double Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                OnPropertyChanged(nameof(Radius));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private double diameter;
        public double Diameter
        {
            get
            {
                return diameter;
            }
            set
            {
                diameter = value;
                OnPropertyChanged(nameof(Diameter));
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        private string vertexNumber;
        public string VertexNumber
        {
            get
            {
                return vertexNumber;
            }
            set
            {
                vertexNumber = value;
                OnPropertyChanged(nameof(VertexNumber));
            }
        }        
        //----------------------------------------------------------------------------------------------------------------------
        public double Left
        {
            get
            {
                return CenterX - Radius;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public double Top
        {
            get
            {
                return CenterY - Radius;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public double Width
        {
            get
            {
                return Diameter;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public double Height
        {
            get
            {
                return Diameter;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
        public BipartiteGraphVertexViewModel(BipartiteVertex vertex,int number, double centerX, double centerY, double radius)
        {
            AdjacentVertices = vertex.AdjacentVertices.ToList();
            this.CenterX = centerX;
            this.CenterY = centerY;
            this.Radius = radius;
            this.Diameter = radius*2;
            this.number = number;
            vertexNumber = number.ToString();
        }
        //----------------------------------------------------------------------------------------------------------------------
        public override void NotifyUI()
        {
            OnPropertyChanged(nameof(Top));
            OnPropertyChanged(nameof(Left));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphLeftVertexViewModel : BipartiteGraphVertexViewModel
    {
        public BipartiteGraphLeftVertexViewModel(BipartiteVertex vertex, int number, double centerX, double centerY, double radius) :
            base( vertex, number, centerX, centerY, radius)
        { 
        }
        //----------------------------------------------------------------------------------------------------------------------
        public double AdjacentVerticesLeftPosition
        {
            get
            {
                return CenterX - Radius - AdjacentsAsString.Length*6 - 4;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
    public class BipartiteGraphRightVertexViewModel : BipartiteGraphVertexViewModel
    {
        public BipartiteGraphRightVertexViewModel(BipartiteVertex vertex, int number, double centerX, double centerY, double radius) :
            base(vertex, number, centerX, centerY, radius)
        {
        }
        //----------------------------------------------------------------------------------------------------------------------
        public double AdjacentVerticesLeftPosition
        {
            get
            {
                return CenterX + Radius;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------
    }
    //----------------------------------------------------------------------------------------------------------------------
}
