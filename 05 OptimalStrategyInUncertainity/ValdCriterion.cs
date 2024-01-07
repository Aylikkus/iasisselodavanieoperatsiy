namespace OptimalStrategyInUncertainity
{
    public class ValdCriterion : ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix)
        {
            double maxValue = double.MinValue;
            int maxIndex = 0;
            for (int i = 1; i <= matrix.Size; i++)
            {
                double minRowValue = double.MaxValue;
                for (int j = 1; j <= matrix.Size; j++)
                {
                    if (minRowValue > matrix[i, j])
                    {
                        minRowValue = matrix[i, j];
                    }
                }

                if (minRowValue > maxValue)
                {
                    maxIndex = i;
                    maxValue = minRowValue;
                }
            }

            return maxIndex;
        }
    }
}