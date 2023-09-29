using LiveCharts;
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
using System.Threading;
using LiveCharts.Wpf;

namespace GraphMethod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel dataModel;

        public MainWindow()
        {
            InitializeComponent();
            chart1.MinHeight = 100;
            chart1.MinWidth = 100;
            dataModel = new DataModel(chart1);
            dataModel.Draw();
            chart1.AnimationsSpeed = new TimeSpan(1000);
            
            // DataModel.Intersect(new LinearEquation(chart1, 1, 2, 3, Sign.Equal), new LinearEquation(chart1, 1, 2, 3, Sign.Equal));
        }

        private void chart1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //dataModel.Draw();
        }

        private void chart1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            dataModel.Draw();
        }
    }
}
