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
using LiveCharts.Definitions.Charts;
using LiveCharts.Wpf.Charts.Base;

namespace GraphMethod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataModel dataModel;
        private bool isMouseDragging = false;

        public MainWindow()
        {
            InitializeComponent();
            chart1.AxisX[0].MaxValue = 50;
            chart1.AxisY[0].MaxValue = 50;
            chart1.AxisX[0].MinValue = -50;
            chart1.AxisY[0].MinValue = -50;
            dataModel = new DataModel(chart1);
            dataModel.TargetFunction = new TargetFunction(dataModel, chart1, 3, 4, Target.Max);
            dataModel.SystemEquations = new SystemEquations(dataModel,
                new List<LinearEquation> {
            new LinearEquation(dataModel, 4, 9, 36, Sign.Less),
            new LinearEquation(dataModel, 2, 1, 11, Sign.Less),
            new LinearEquation(dataModel, 1, 0, 5, Sign.Less),
            new LinearEquation(dataModel, 1, 0, 0, Sign.More),
            new LinearEquation(dataModel, 0, 1, 0, Sign.More)
                });
            dataModel.Draw();
            dataModel.DrawRoAV();
            chart1.AnimationsSpeed = new TimeSpan(1000);

            chart1.MouseDown += Chart1_MouseDown;
            chart1.MouseMove += Chart1_MouseMove;
            chart1.MouseUp += Chart1_MouseUp;
        }

        private void Chart1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = true;
            dataModel.Draw();
        }

        private void Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDragging)
            {
                dataModel.Draw();
            }
        }

        private void Chart1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDragging = false;
            dataModel.Draw();
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
