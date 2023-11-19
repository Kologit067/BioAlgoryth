using BioAlgorythmViewModel.BipartiteGraphModel;
using FindingRegulatoryMotifs.Enumeration;
using StatisticsStorage.Accumulators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BioAlgorythm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // arrange
            string excpectedMotif = "agcgt";
            string expectedSolutionStartPosition = "3,5,1";
            int expectedResult = 5;

            char[][] charSets = new char[][] { new char[] {'a','g','t','a','g','c','g','t','a','a' },
            new char[] {'t','g','t','g','c','a','g','c','g','t' },
            new char[] {'a','a','g','c','g','t','t','a','c','c' }};
            char[] alphabet = new char[] { 'a', 'c', 'g', 't' };
            int substringLength = 5;
            RegulatoryMotifsSubSequencesEnumeration enumeration = new RegulatoryMotifsSubSequencesEnumeration(charSets, alphabet, substringLength)
            {
                StatisticAccumulator = new FakeRegulatoryMotifsStatisticAccumulator()
            };
            // act
            enumeration.Execute();
            // assert
            string solutionStartPosition = string.Join(",", enumeration.SolutionStartPositionList);
            string motif = string.Join("", enumeration.Motif);
            if (motif != excpectedMotif)
                MessageBox.Show($"Motif is wrong.");
            if (solutionStartPosition != expectedSolutionStartPosition)
            MessageBox.Show($"Positions are wrong.");
            if (enumeration.OptimalValue != expectedResult)
                MessageBox.Show($"Result is wrong.");

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = new BipartiteGraphViewModel();
            DataContext = viewModel;
        }
    }
    //-------------------------------------------------------------------------------------------------------------------
    // class VisibilityConverter
    //-------------------------------------------------------------------------------------------------------------------
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityConverter : IValueConverter
    {
        //-------------------------------------------------------------------------------------------------------------------
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool visibility = (bool)value;
            if (visibility)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
        //-------------------------------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
        //-------------------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------------------

}
