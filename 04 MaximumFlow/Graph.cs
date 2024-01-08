using System.Runtime.InteropServices;

namespace MaximumFlow
{
    public class Graph
    {
        private int verticesCount;
        private double[,] adjacencyMatrix;

        public Graph(int numOfVertices)
        {
            verticesCount = numOfVertices;
            adjacencyMatrix = new double[verticesCount, verticesCount];
        }

        public double this[int i, int j]
        {
            get
            {
                return adjacencyMatrix[i - 1, j - 1];
            }
            set
            {
                if (i == j) throw new ArgumentException("Vertice can't have throughput to itself");
                adjacencyMatrix[i - 1, j - 1] = value;
            }
        }

        public double GetThroughput(int i, int j)
        {
            return this[i, j];
        }

        public void SetThroughput(int i, int j, double value)
        {
            this[i, j] = value;
        }

        public int VerticesCount
        {
            get
            {
                return verticesCount;
            }
        }

        public double GetMaxFlow(IMaxFlowSolveMethod method, int I, int S)
        {
            return method.Solve(this, I, S);
        }
    }
}