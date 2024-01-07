namespace OptimalStrategyInUncertainity
{
    public class SevidjCriterion : ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            double[] maxColumnValues = new double[matrix.Size];

            for (int j = 1; j <= matrix.Size; j++)
            {
                double maxColumnValue = double.MinValue;
                for (int i = 1; i <= matrix.Size; i++)
                {
                    if (matrix[i, j] > maxColumnValue)
                    {
                        maxColumnValue = matrix[i, j];
                    }
                }

                maxColumnValues[j - 1] = maxColumnValue;
            }

            PaymentMatrix risks = new PaymentMatrix(matrix.Size);
            for (int i = 1; i <= matrix.Size; i++)
            {
                for (int j = 1; j <= matrix.Size; j++)
                {
                    risks[i ,j] = maxColumnValues[j - 1] - matrix[i, j];
                }
            }

            return new MinMaxCriterion().GetOptimalStrategy(risks);
        }
    }
}