using SimplexLib;

namespace MaximumFlow
{
    public class LinearAlgorithm : IMaxFlowSolveMethod
    {
        private int graphEdgeCount;
        private Dictionary<(int, int), int> edgeMap = new Dictionary<(int, int), int>();
        private List<int> targetX = new List<int>();

        private void mapEdges(Graph graph)
        {
            edgeMap.Clear();
            int count = 0;
            for (int i = 1; i <= graph.VerticesCount; i++)
            {
                for (int j = 1; j <= graph.VerticesCount; j++)
                {
                    if (graph.GetThroughput(i, j) != 0)
                    {
                        count++;
                        edgeMap[(i, j)] = count;
                    }
                }
            }
            graphEdgeCount = count;
        }

        private TargetFunction buildTarget(Graph graph, int I)
        {
            targetX.Clear();
            TargetFunction result = new TargetFunction(graphEdgeCount)
            {
                Target = Target.Max
            };
            for (int j = 1; j <= graph.VerticesCount; j++)
            {
                if (graph.GetThroughput(I, j) != 0)
                {
                    int x_ij = edgeMap[(I, j)];
                    result[x_ij - 1] = 1;
                    targetX.Add(x_ij);
                }
            }
            return result;
        }

        private Constraints buildConstraints(Graph graph, int I, int S)
        {
            int rows = graphEdgeCount + graph.VerticesCount - 2;
            int cols = graphEdgeCount;
            Constraints constraints = new Constraints(rows, cols);
            for (int i = 1; i <= graph.VerticesCount; i++)
            {
                bool isSinkFrom = i == I || i == S;
                for (int j = 1; j <= graph.VerticesCount; j++)
                {
                    bool isSinkTo = j == I || j == S;
                    double c_ij = graph.GetThroughput(i, j);
                    if (c_ij != 0)
                    {
                        int x_ij = edgeMap[(i, j)];
                        constraints[x_ij - 1, x_ij - 1] = 1;
                        constraints[x_ij - 1].Sign = Sign.LessThanEqual;
                        constraints[x_ij - 1].B = c_ij;
                        if (! isSinkFrom)
                        {
                            constraints[i + graphEdgeCount - 2, x_ij - 1] = -1;
                        }
                        if (! isSinkTo)
                        {
                            constraints[j + graphEdgeCount - 2, x_ij - 1] = 1;
                        }
                    }
                }
            }
            return constraints;
        }

        public double Solve(Graph graph, int I, int S)
        {
            mapEdges(graph);
            TargetFunction targetFunction = buildTarget(graph, I);
            Constraints constraints = buildConstraints(graph, I, S);
            SimplexMethod simplexMethod = new SimplexMethod(constraints, targetFunction);
            var solution = simplexMethod.SolveWithSimplexMethod();
            double maxFlow = 0;
            foreach(int x in targetX)
            {
                maxFlow += solution.Item1[x - 1];
            }
            return maxFlow;
        }
    }
}