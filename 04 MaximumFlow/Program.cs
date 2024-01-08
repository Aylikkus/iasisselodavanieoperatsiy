namespace MaximumFlow
{
    class Program
    {
        public static void Main()
        {
            const int I = 1;
            const int S = 6;
            // Граф из презентации
            Graph graph = new Graph(6);
            graph[1, 2] = 7;
            graph[1, 3] = 6;
            graph[2, 4] = 6;
            graph[4, 5] = 3;
            graph[5, 3] = 2;
            graph[4, 6] = 4;
            graph[3, 6] = 9;

            Console.WriteLine("Максимальный поток методом линейного программирования: " +
                graph.GetMaxFlow(new LinearAlgorithm(), I, S));
            Console.WriteLine("Максимальный поток методом Форда Фулкерсона: " +
                graph.GetMaxFlow(new FordFulkersonAlgorithm(), I, S));
            Console.WriteLine("Максимальный поток методом Диница: " +
                graph.GetMaxFlow(new DinicsAlgorithm(), I, S));
        }
    }
}