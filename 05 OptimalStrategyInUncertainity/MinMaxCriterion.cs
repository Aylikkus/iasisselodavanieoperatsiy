namespace OptimalStrategyInUncertainity
{
    public class MinMaxCriterion : ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            double minValue = double.MaxValue;
            int minValueIndex = 0;
            for (int i = 1; i <= matrix.Size; i++)
            {
                double maxRowValue = double.MinValue;
                for (int j = 1; j <= matrix.Size; j++)
                {
                    if (maxRowValue < matrix[i, j])
                    {
                        maxRowValue = matrix[i, j];
                    }
                }

                if (maxRowValue < minValue)
                {
                    minValueIndex = i;
                    minValue = maxRowValue;
                }
            }

            return minValueIndex;
        }
    }
}