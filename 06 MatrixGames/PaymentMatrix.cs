using SimplexLib;

namespace MatrixGames
{
    public class PaymentMatrix
    {
        private double[,] values;
        private int n;
        private int m;

        public PaymentMatrix(double[,] values)
        {
            n = values.GetLength(0);
            m = values.GetLength(1);

            this.values = (double[,])values.Clone();
        }

        public double this[int i, int j]
        {
            get
            {
                return values[i - 1, j - 1];
            }
            set
            {
                values[i - 1, j - 1] = value;
            }
        }

        public int LowerPrice
        {
            get
            {
                double max_i = double.MinValue;
                int max_index = 0;
                for (int i = 1; i <= n; i++)
                {
                    double min_j = double.MaxValue;

                    for (int j = 1; j <= m; j++)
                    {
                        if (this[i, j] < min_j) min_j = this[i, i];
                    }

                    if (min_j > max_i)
                    {
                        max_i = min_j;
                        max_index = i;
                    }
                }

                return max_index;
            }
        }

        public int HigherPrice
        {
            get
            {
                double min_j = double.MaxValue;
                int min_index = 0;
                for (int j = 1; j <= m; j++)
                {
                    double max_i = double.MinValue;

                    for (int i = 1; i <= n; i++)
                    {
                        if (this[i, j] > max_i) max_i = this[i, j];
                    }

                    if (max_i < min_j)
                    {
                        min_j = max_i;
                        min_index = j;
                    }
                }

                return min_index;
            }
        }

        // Оптимальные стратегии для игрока А и Б соответственно
        public (double[], double[]) Solve()
        {
            int alpha = LowerPrice;
            int beta = HigherPrice;

            // Если имеется седловая точка
            if (alpha == beta) return ([alpha], [beta]);

            // Метод линейного программирования
            // Система игрока A 
            TargetFunction targetA = new TargetFunction(n)
            {
                Target = Target.Min
            };
            for (int i = 0; i < n; i++)
            {
                targetA[i] = 1;
            }

            Constraints constraintsA = new Constraints(n * 2, n);
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    constraintsA[i, j] = values[j, i];
                    constraintsA[i].Sign = Sign.GreaterThanEqual;
                    constraintsA[i].B = 1;
                }
            }
            for (int i = 0; i < n; i++)
            {
                constraintsA[i + n, i] = 1;
                constraintsA[i + n].Sign = Sign.GreaterThanEqual;
                constraintsA[i + n].B = 0;
            }

            (double[] optimalsA, _) = new SimplexMethod(constraintsA, targetA).SolveWithSimplexMethod();
            double sumA = optimalsA.Sum();
            for (int i = 0; i < optimalsA.Length; i++)
            {
                optimalsA[i] *= 1 / sumA; 
            }

            // Система игрока B
            TargetFunction targetB = new TargetFunction(m)
            {
                Target = Target.Max
            };
            for (int i = 0; i < n; i++)
            {
                targetB[i] = 1;
            }

            Constraints constraintsB = new Constraints(m * 2, m);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    constraintsB[i, j] = values[i, j];
                    constraintsB[i].Sign = Sign.LessThanEqual;
                    constraintsB[i].B = 1;
                }
            }
            for (int j = 0; j < m; j++)
            {
                constraintsA[j + n, j] = 1;
                constraintsA[j + n].Sign = Sign.GreaterThanEqual;
                constraintsA[j + n].B = 0;
            }

            (double[] optimalsB, _) = new SimplexMethod(constraintsB, targetB).SolveWithSimplexMethod();
            double sumB = optimalsB.Sum();
            for (int i = 0; i < optimalsB.Length; i++)
            {
                optimalsB[i] *= 1 / sumB;
            }
            return (optimalsA, optimalsB);
        }
    }
}