using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SimplexMethod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataModel model;

        public MainWindow()
        {
            InitializeComponent();
            model = new DataModel(new Constraints(3, 3));
            
            initializeConstaintsGrid();
        }

        private void initializeConstaintsGrid()
        {
            int rows = model.Constraints.Rows;
            int columns = model.Constraints.Columns;

            var header = new RowDefinition();
            constraintsGrid.RowDefinitions.Add(header);

            for (int i = 0; i < rows; i++)
            {
                var row = new RowDefinition();
                constraintsGrid.RowDefinitions.Add(row);
                
                Label headerText = new Label();
                headerText.Content = $"x{i + 1}";
                headerText.SetValue(Grid.RowProperty, 0);
                headerText.SetValue(Grid.ColumnProperty, i);

                constraintsGrid.Children.Add(headerText);

                for (int j = 0; j < columns; j++)
                {
                    var column = new ColumnDefinition();
                    column.Width = new GridLength(50);
                    constraintsGrid.ColumnDefinitions.Add(column);

                    TextBox input = new TextBox();
                    input.SetValue(Grid.RowProperty, i + 1);
                    input.SetValue(Grid.ColumnProperty, j);

                    constraintsGrid.Children.Add(input);

                    Binding valBinding = new Binding();
                    valBinding.Source = model.Constraints;
                    valBinding.Path = new PropertyPath($"[{i}][{j}]");
                    valBinding.Mode = BindingMode.TwoWay;
                    BindingOperations.SetBinding(input, TextBox.TextProperty, valBinding);
                }
            }
            
            var signHeader = new RowDefinition();
            constraintsGrid.RowDefinitions.Add(signHeader);

            Label signHeaderText = new Label();
            signHeaderText.Content = $"знак";
            signHeaderText.SetValue(Grid.RowProperty, 0);
            signHeaderText.SetValue(Grid.ColumnProperty, columns);

            constraintsGrid.Children.Add(signHeaderText);

            var bHeader = new RowDefinition();
            constraintsGrid.RowDefinitions.Add(bHeader);

            Label bHeaderText = new Label();
            bHeaderText.Content = $"b";
            bHeaderText.SetValue(Grid.RowProperty, 0);
            bHeaderText.SetValue(Grid.ColumnProperty, columns + 1);

            constraintsGrid.Children.Add(bHeaderText);

            for (int i = 0; i < rows; i++)
            {
                var signColumn = new ColumnDefinition();
                constraintsGrid.ColumnDefinitions.Add(signColumn);

                TextBox signInput = new TextBox();
                signInput.SetValue(Grid.RowProperty, i + 1);
                signInput.SetValue(Grid.ColumnProperty, columns);

                constraintsGrid.Children.Add(signInput);

                Binding signBinding = new Binding();
                signBinding.Source = model.Constraints[i];
                signBinding.Path = new PropertyPath("Sign");
                signBinding.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(signInput, TextBox.TextProperty, signBinding);

                var bColumn = new ColumnDefinition();
                constraintsGrid.ColumnDefinitions.Add(bColumn);

                TextBox bInput = new TextBox();
                bInput.SetValue(Grid.RowProperty, i + 1);
                bInput.SetValue(Grid.ColumnProperty, columns + 1);

                constraintsGrid.Children.Add(bInput);

                Binding bBinding = new Binding();
                bBinding.Source = model.Constraints[i];
                bBinding.Path = new PropertyPath("B");
                bBinding.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(bInput, TextBox.TextProperty, bBinding);
            }
        }

        private void printConstaints_Click(object sender, RoutedEventArgs e)
        {
            double[,] dump = new double[model.Constraints.Rows, model.Constraints.Columns];
            for (int i = 0; i < model.Constraints.Rows; i++)
            {
                for (int j = 0; j < model.Constraints.Columns; j++)
                {
                    dump[i, j] = model.Constraints[i, j];
                }
            }
            int x = 0;
        }
    }
}
