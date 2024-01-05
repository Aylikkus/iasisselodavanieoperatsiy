namespace SimplexMethod
{
    public class SimplexMethod
    {
        private Constraints constraints;
        private TargetFunction targetFunction;

        public Constraints Constraints
        {
            get
            {
                return constraints;
            }
            set
            {
                constraints = value;
            }
        }

        public TargetFunction TargetFunction
        {
            get
            {
                return targetFunction;
            }
            set
            {
                targetFunction = value;
            }
        }

        public SimplexMethod(Constraints c, TargetFunction target)
        {
            Constraints = c;
            TargetFunction = target;
        }

        private void recalculateTable(double[,] table, int k, int l, int[] rows, int[] cols)
        {
            int n = Constraints.Rows + 1;
            int m = Constraints.Columns + 1;
            int temp;

            temp = rows[k - 1];
            rows[k - 1] = cols[l];
            cols[l] = temp;

            double[,] tempArr = table.Clone() as double[,];
            table[k, l] = 1 / tempArr[k, l];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == k && j == l)
                    {
                        continue;
                    }

                    if (i == k)
                    {
                        table[i, j] *= 1 / tempArr[k, l];
                    }
                    else
                    if (j == l)
                    {
                        table[i, j] *= -1 / tempArr[k, l];
                    }
                    else
                    {
                        table[i, j] -= tempArr[i, l] * tempArr[k, j] / tempArr[k, l];
                    }
                }
            }
        }

        private bool isAllowable(double[,] table, int[] rows, int[] cols)
        {
            int n = Constraints.Rows + 1;
            int m = Constraints.Columns + 1;

            bool hasNegativeB = false;
            double minB = 0;
            int k = -1;
            int l = -1;
            for (int i = 1; i < n; i++)
            {
                if (table[i, m - 1] >= 0)
                {
                    continue;
                }

                hasNegativeB = true;
                if (table[i, m - 1] < minB)
                {
                    minB = table[i, m - 1];
                    k = i;
                }
            }

            if (hasNegativeB)
            {
                double minX = 0;
                for (int j = 0; j < m - 1; j++)
                {
                    if (table[k, j] < minX)
                    {
                        minX = table[k, j];
                        l = j;
                    }
                }

                if (l == -1)
                {
                    return false;
                }
            }
            else
            {
                return isOptimal(table, rows, cols);
            }

            recalculateTable(table, k, l, rows, cols);
            return isAllowable(table, rows, cols);
        }

        private bool isOptimal(double[,] table, int[] rows, int[] cols)
        {
            int n = Constraints.Rows + 1;
            int m = Constraints.Columns + 1;

            bool hasNegativeF = false;
            double minX = 0;
            int k = -1;
            int l = -1;
            for (int j = 0; j < m - 1; j++)
            {
                if (table[0, j] >= 0)
                {
                    continue;
                }

                hasNegativeF = true;
                if (table[0, j] < minX)
                {
                    minX = table[0, j];
                    l = j;
                }
            }

            if (hasNegativeF)
            {
                double minRelation = double.MaxValue;

                for (int i = 1; i < n; i++)
                {
                    double b_i = table[i, n - 1];
                    double a_il = table[i, l];
                    double relation = b_i / a_il;
                    if (a_il > 0 && b_i > 0 && relation < minRelation)
                    {
                        minRelation = relation;
                        k = i;
                    }
                }
            }
            else
            {
                return true;
            }

            recalculateTable(table, k, l, rows, cols);
            return isAllowable(table, rows, cols);
        }

        public double[,] TableView
        {
            get
            {
                Constraints.ToCanonicalForm();
                TargetFunction canonical = TargetFunction.GetCanonicalForm();

                int n = Constraints.Rows + 1;
                int m = Constraints.Columns + 1;
                double[,] Table = new double[n, m];

                for (int i = 0; i < m - 1; i++)
                {
                    Table[0, i] = canonical[i];
                }

                Table[0, m - 1] = canonical.B;

                for (int i = 1; i < n; i++)
                {
                    for (int j = 0; j < m - 1; j++)
                    {
                        Table[i, j] = Constraints[i - 1, j];
                    }
                    Table[i, m - 1] = Constraints[i - 1].B;
                }
                return Table;
            }
        }

        public int[] TableColumns
        {
            get
            {
                int n = Constraints.Columns;
                int[] cols = new int[n];
                for (int i = 1; i <= n; i++)
                {
                    cols[i - 1] = i;
                }
                return cols;
            }
        }

        public int[] TableRows
        {
            get
            {
                int m = Constraints.Rows;
                int[] rows = new int[m];
                for (int i = 1; i <= m; i++)
                {
                    rows[i - 1] = i + Constraints.Columns;
                }
                return rows;
            }
        }

        public (double[], double) SolveWithSimplexMethod()
        {
            double[,] table = TableView;
            int[] rows = TableRows;
            int[] columns = TableColumns;

            double[] x = new double[columns.Length];
            double b = 0;

            if (isAllowable(table, rows, columns))
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i] <= Constraints.Rows)
                    {
                        x[rows[i] - 1] = table[i + 1, columns.Length];
                    }
                }

                b = table[0, columns.Length];

                if (TargetFunction.Target == Target.Min)
                {
                    b *= -1;
                }
            }

            return (x, b);
        }
    }
}
