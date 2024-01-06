namespace MaximumFlow
{
    class Program
    {
        public static void Main()
        {
            // Граф из презентации
            Graph graph = new Graph(6);
            graph.SetThroughput(1, 2, 7);
            graph.SetThroughput(1, 3, 6);
            graph.SetThroughput(2, 4, 6);
            graph.SetThroughput(4, 5, 3);
            graph.SetThroughput(5, 3, 2);
            graph.SetThroughput(4, 6, 4);
            graph.SetThroughput(3, 6, 9);

            IMaxFlowSolveMethod method = new LinearAlgorithm();
            double result = method.Solve(graph, 1, 6);

            Console.WriteLine("Максимальный поток: " + result);
        }
    }
}