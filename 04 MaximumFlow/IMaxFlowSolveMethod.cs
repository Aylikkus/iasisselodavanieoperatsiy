namespace MaximumFlow
{
    public interface IMaxFlowSolveMethod
    {
        public double Solve(Graph graph, int I, int S);
    }
}