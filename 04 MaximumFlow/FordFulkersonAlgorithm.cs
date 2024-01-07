namespace MaximumFlow
{
    public class FordFulkersonAlgorithm : IMaxFlowSolveMethod
    {
        // Количество вершин
        int V;

        // Остаточное дерево
        bool bfs(double[, ] rGraph, int I, int S, int[] parent)
        {
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
                visited[i] = false;
    
            List<int> queue = new List<int>();
            queue.Add(I);
            visited[I] = true;
            parent[I] = -1;
    
            while (queue.Count != 0) 
            {
                int u = queue[0];
                queue.RemoveAt(0);
    
                for (int v = 0; v < V; v++) 
                {
                    if (visited[v] == false && rGraph[u, v] > 0) 
                    {
                        if (v == S) {
                            parent[v] = u;
                            return true;
                        }
                        queue.Add(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return false;
        }
    
        public double Solve(Graph graph, int I, int S)
        {
            int u, v;
            V = graph.VerticesCount;

            // Для избежания проблем с индексами
            I--;
            S--;
    
            double[, ] rGraph = new double[V, V];
    
            for (u = 0; u < V; u++)
            {
                for (v = 0; v < V; v++)
                {
                    rGraph[u, v] = graph.GetThroughput(u + 1, v + 1);
                }
            }
                
    
            int[] parent = new int[V];
    
            double maxFlow = 0;
    
            while (bfs(rGraph, I, S, parent)) 
            {
                double pathFlow = double.MaxValue;
                for (v = S; v != I; v = parent[v]) 
                {
                    u = parent[v];
                    pathFlow = Math.Min(pathFlow, rGraph[u, v]);
                }
    
                for (v = S; v != I; v = parent[v]) 
                {
                    u = parent[v];
                    rGraph[u, v] -= pathFlow;
                    rGraph[v, u] += pathFlow;
                }
    
                maxFlow += pathFlow;
            }
    
            return maxFlow;
        }
    }
}