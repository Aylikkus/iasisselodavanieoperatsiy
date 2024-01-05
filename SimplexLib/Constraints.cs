namespace SimplexMethod
{
    public class Constraints
    {
        private LinearExpression[] expressions;

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public LinearExpression this[int i]
        {
            get
            {
                return expressions[i];
            }
            set
            {
                expressions[i] = value;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return expressions[i][j];
            }
            set
            {
                expressions[i][j] = value;
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
