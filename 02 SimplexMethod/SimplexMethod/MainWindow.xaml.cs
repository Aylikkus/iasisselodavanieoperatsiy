using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SimplexMethod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataModel model;
        public int Rows { get; set; } = 2;
        public int Cols { get; set; } = 2;

        public MainWindow()
        {
            InitializeComponent();
            model = new DataModel(new Constraints(Rows, Cols), new TargetFunction(Cols));

            // Значения по-умолчанию
            model.TargetFunction[0] = 4;
            model.TargetFunction[1] = 1;
            //model.TargetFunction.Target = Target.Min;
            model.TargetFunction.Target = Target.Max;

            model.Constraints[0][0] = 4;
            model.Constraints[0][1] = 3;
            model.Constraints[0].Sign = Sign.GreaterThanEqual;
            model.Constraints[0].B = 6;

            model.Constraints[1][0] = 1;
            model.Constraints[1][1] = 2;
            model.Constraints[1].Sign = Sign.LessThanEqual;
            model.Constraints[1].B = 4;

            initializeTargetGrid();
            initializeConstaintsGrid();
        }

        private void initializeTargetGrid()
        {
            targetGrid.Children.Clear();
            targetGrid.RowDefinitions.Clear();
            targetGrid.ColumnDefinitions.Clear();
            int columns = model.TargetFunction.CoefficientsCount;

            var header = new RowDefinition();
            targetGrid.RowDefinitions.Add(header);
            header.Height = new GridLength(50);

            var row = new RowDefinition();
            targetGrid.RowDefinitions.Add(row);
            row.Height = new GridLength(50);

            for (int i = 0; i < columns; i++)
            {
                Label headerText = new Label();
                headerText.Content = $"x{i + 1}";
                headerText.SetValue(Grid.RowProperty, 0);
                headerText.SetValue(Grid.ColumnProperty, i);

                targetGrid.Children.Add(headerText);

                var column = new ColumnDefinition();
                column.Width = new GridLength(50);
                targetGrid.ColumnDefinitions.Add(column);

                TextBox input = new TextBox();
                input.SetValue(Grid.RowProperty, 1);
                input.SetValue(Grid.ColumnProperty, i);

                targetGrid.Children.Add(input);

                Binding valBinding = new Binding();
                valBinding.Source = model.TargetFunction;
                valBinding.Path = new PropertyPath($"Item[{i}]");
                valBinding.Mode = BindingMode.TwoWay;
                BindingOperations.SetBinding(input, TextBox.TextProperty, valBinding);
            }

            var signHeader = new RowDefinition();
            constraintsGrid.RowDefinitions.Add(signHeader);
            signHeader.Height = new GridLength(50);

            Label signHeaderText = new Label();
            signHeaderText.Content = $"знак";
            signHeaderText.SetValue(Grid.RowProperty, 0);
            signHeaderText.SetValue(Grid.ColumnProperty, columns);

            targetGrid.Children.Add(signHeaderText);

            var bHeader = new RowDefinition();
            targetGrid.RowDefinitions.Add(bHeader);
            bHeader.Height = new GridLength(50);

            Label bHeaderText = new Label();
            bHeaderText.Content = $"b";
            bHeaderText.SetValue(Grid.RowProperty, 0);
            bHeaderText.SetValue(Grid.ColumnProperty, columns + 1);

            targetGrid.Children.Add(bHeaderText);

            var targetColumn = new ColumnDefinition();
            targetGrid.ColumnDefinitions.Add(targetColumn);
            targetColumn.Width = new GridLength(50);

            TextBox targetInput = new TextBox();
            targetInput.SetValue(Grid.RowProperty, 1);
            targetInput.SetValue(Grid.ColumnProperty, columns);

            targetGrid.Children.Add(targetInput);

            Binding targetBinding = new Binding();
            targetBinding.Source = model.TargetFunction;
            targetBinding.Path = new PropertyPath("TargetString");
            targetBinding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(targetInput, TextBox.TextProperty, targetBinding);

            var bColumn = new ColumnDefinition();
            targetGrid.ColumnDefinitions.Add(bColumn);
            bColumn.Width = new GridLength(50);

            TextBox bInput = new TextBox();
            bInput.SetValue(Grid.RowProperty, 1);
            bInput.SetValue(Grid.ColumnProperty, columns + 1);

            targetGrid.Children.Add(bInput);

            Binding bBinding = new Binding();
            bBinding.Source = model.TargetFunction;
            bBinding.Path = new PropertyPath("B");
            bBinding.Mode = BindingMode.TwoWay;
            BindingOperations.SetBinding(bInput, TextBox.TextProperty, bBinding);
        }

        private void initializeConstaintsGrid()
        {
            constraintsGrid.Children.Clear();
            constraintsGrid.RowDefinitions.Clear();
            constraintsGrid.ColumnDefinitions.Clear();
            int rows = model.Constraints.Rows;
            int columns = model.Constraints.Columns;

            var header = new RowDefinition();
            constraintsGrid.RowDefinitions.Add(header);

            for (int i = 0; i < rows; i++)
            {
                var row = new RowDefinition();
                constraintsGrid.RowDefinitions.Add(row);

                for (int j = 0; j < columns; j++)
                {
                    Label headerText = new Label();
                    headerText.Content = $"x{j + 1}";
                    headerText.SetValue(Grid.RowProperty, 0);
                    headerText.SetValue(Grid.ColumnProperty, j);

                    constraintsGrid.Children.Add(headerText);

                    var column = new ColumnDefinition();
                    column.Width = new GridLength(50);
                    constraintsGrid.ColumnDefinitions.Add(column);

                    TextBox input = new TextBox();
                    input.SetValue(Grid.RowProperty, i + 1);
                    input.SetValue(Grid.ColumnProperty, j);

                    constraintsGrid.Children.Add(input);

                    Binding valBinding = new Binding();
                    valBinding.Source = model.Constraints;
                    valBinding.Path = new PropertyPath($"Item[{i}][{j}]");
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
                signBinding.Path = new PropertyPath("SignString");
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

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            model = new DataModel(new Constraints(Rows, Cols), new TargetFunction(Cols));

            initializeTargetGrid();
            initializeConstaintsGrid();
        }
        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            var solution = model.SolveWithSimplexMethod();
            double[] x = solution.Item1;
            double b = solution.Item2;
            StringBuilder sb = new StringBuilder();

            sb.Append("Иксы: ");
            for (int i = 0; i < x.Length; i++)
            {
                sb.Append($"x{i + 1}=" + x[i] + " ");
            }

            sb.AppendLine("B: " + b);

            MessageBox.Show(sb.ToString());
        }
    }
}
