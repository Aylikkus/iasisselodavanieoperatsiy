namespace MaximumFlow
{
    public class DinicsAlgorithm : IMaxFlowSolveMethod
    {
        int V;
        int[] level;
        List<Edge>[] adj;

        public bool bfs(int I, int S)
        {
            for (int j = 0; j < V; j++)
            {
                level[j] = -1;
            }

            level[I] = 0;

            Queue<int> q = new Queue<int>();
            q.Enqueue(I);

            List<Edge>.Enumerator i;
            while (q.Count != 0)
            {
                int u = q.Dequeue();

                for (i = adj[u].GetEnumerator(); i.MoveNext();)
                {
                    Edge e = i.Current;
                    if (level[e.to] < 0 && e.flow < e.capacity)
                    {
                        level[e.to] = level[u] + 1;
                        q.Enqueue(e.to);
                    }
                }
            }

            return level[S] >= 0;
        }

        public double sendFlow(int u, double flow, int S, int[] start)
        {
            if (u == S)
            {
                return flow;
            }

            for (; start[u] < adj[u].Count; start[u]++)
            {
                Edge e = adj[u][start[u]];

                if (level[e.to] == level[u] + 1 && e.flow < e.capacity)
                {
                    double curr_flow = Math.Min(flow, e.capacity - e.flow);
                    double temp_flow = sendFlow(e.to, curr_flow, S, start);

                    if (temp_flow > 0)
                    {
                        e.flow += temp_flow;

                        adj[e.to][e.reverse].flow -= temp_flow;
                        return temp_flow;
                    }
                }
            }

            return 0;
        }

        public double Solve(Graph graph, int I, int S)
        {
            I--;
            S--;
            V = graph.VerticesCount;
            level = new int[V];
            adj = new List<Edge>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<Edge>();
            }

            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (graph[i + 1, j + 1] == 0) continue;
                    Edge a = new Edge(j, 0, graph[i + 1, j + 1], adj[j].Count);
                    Edge b = new Edge(i, 0, 0, adj[i].Count);
                    adj[i].Add(a);
                    adj[j].Add(b);
                }
            }

            double total = 0;

            while (bfs(I, S))
            {
                int[] start = new int[V + 1];

                while (true)
                {
                    double flow = sendFlow(I, double.MaxValue, S, start);
                    if (flow == 0)
                    {
                        break;
                    }

                    total += flow;
                }
            }

            return total;
        }

        private class Edge(int to, double flow, double capacity, int reverse)
        {
            public int to = to;

            public double flow = flow;

            public double capacity = capacity;

            public int reverse = reverse;
        }
    }
}