namespace OptimalStrategyInUncertainity
{
    public interface ICriterion
    {
        public int GetOptimalStrategy(PaymentMatrix matrix);
    }
}