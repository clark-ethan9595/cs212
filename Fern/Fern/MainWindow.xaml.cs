using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FernNamespace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Create a fern with the initial set values from the sliders once the window is opened
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Fern f = new Fern(sizeSlider.Value, reduxSlider.Value, branchesSlider.Value, canvas);
        }

        //Create a new fern with the values from the sliders (altered by the user) when the Draw Button is clicked
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Fern f = new Fern(sizeSlider.Value, reduxSlider.Value, branchesSlider.Value, canvas);
        }
    }

}
