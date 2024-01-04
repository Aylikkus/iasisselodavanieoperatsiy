using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplexMethod
{
    public class Constraints : INotifyPropertyChanged
    {
        private LinearExpression[] expressions;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        [IndexerName("Item")]
        public LinearExpression this[int i]
        {
            get
            {
                return expressions[i];
            }
            set
            {
                expressions[i] = value;
                NotifyPropertyChanged("Item[]");
            }
        }

        [IndexerName("Item")]
        public double this[int i, int j]
        {
            get
            {
                return expressions[i][j];
            }
            set
            {
                expressions[i][j] = value;
                NotifyPropertyChanged("Item[,]");
            }
        }

        public Constraints(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            expressions = new LinearExpression[rows];

            for (int i = 0; i < rows; i++)
            {
                expressions[i] = new LinearExpression(columns);
            }
        }

        private Constraints() { }

        public void TransformExpressions()
        {
            for (int i = 0; i < expressions.Length; i++)
            {
                LinearExpression expression = expressions[i];
                if (expression.Sign == Sign.GreaterThanEqual)
                {
                    expression.InvertGreaterThan();
                }
            }
        }

        public void ToCanonicalForm()
        {
            for (int i = 0; i < expressions.Length; i++)
            {
                TransformExpressions();
                expressions[i].Sign = Sign.Equal;
            }
        }
    }
}
