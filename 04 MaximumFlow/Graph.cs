namespace MaximumFlow
{
    public class Graph
    {
        private int verticesCount;
        double[,] adjacencyMatrix;

        public Graph(int numOfVertices)
        {
            verticesCount = numOfVertices;
            adjacencyMatrix = new double[verticesCount, verticesCount];
        }

        public double GetThroughput(int i, int j)
        {
            return adjacencyMatrix[i - 1, j - 1];
        }

        public void SetThroughput(int i, int j, double value)
        {
            if (i == j) throw new ArgumentException("Vertice can't have throughput to itself");
            adjacencyMatrix[i - 1, j - 1] = value;
        }

        public int VerticesCount 
        {
            get
            {
                return verticesCount;
            }
        }

        public int GetEdgesCount()
        {
            int count = 0;
            for (int i = 1; i <= verticesCount; i++)
            {
                for (int j = 1; j <= verticesCount; j++)
                {
                    if (GetThroughput(i, j) != 0) count++;
                }
            }
            return count;
        }
    }
}