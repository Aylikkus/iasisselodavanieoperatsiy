using System;
using System.Collections.Generic;

/// <summary>
///  Класс, представляющий граф
/// </summary>
public class Graph
{
    private Dictionary<int, List<Edge>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<Edge>>();
    }

    /// <summary>
    /// Метод для добавления вершины в граф
    /// </summary>
    /// <param name="vertex"></param>
    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<Edge>();
        }
    }

    /// <summary>
    /// Метод для добавления ребра между вершинами
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <param name="weight"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddEdge(int source, int destination, int weight)
    {
        if (!adjacencyList.ContainsKey(source) || !adjacencyList.ContainsKey(destination))
        {
            throw new ArgumentException("Both source and destination vertices must exist in the graph.");
        }

        adjacencyList[source].Add(new Edge(destination, weight));
    }

    /// <summary>
    /// Метод для получения списка вершин
    /// </summary>
    /// <returns></returns>
    public IEnumerable<int> GetVertices()
    {
        return adjacencyList.Keys;
    }

    /// <summary>
    /// Метод для получения соседей вершины
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    public IEnumerable<Edge> GetNeighbors(int vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            return adjacencyList[vertex];
        }

        return new List<Edge>();
    }

    /// <summary>
    /// Метод для получения количества вершин в графе
    /// </summary>
    /// <returns></returns>
    public int GetVerticesCount()
    {
        return adjacencyList.Count;
    }
}

/// <summary>
/// Класс, представляющий ребро графа
/// </summary>
public class Edge
{
    public int Destination { get; set; }
    public int Weight { get; set; }

    public Edge(int destination, int weight)
    {
        Destination = destination;
        Weight = weight;
    }
}

/// <summary>
/// Класс, предоставляющий различные алгоритмы для работы с графом
/// </summary>
public class AlgorithmService
{
    /// <summary>
    /// Алгоритм Дейкстры для поиска кратчайшего пути
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public List<int> Dijkstra(Graph graph, int source, int destination)
    {
        // Расстояния от начальной вершины до каждой другой:
        Dictionary<int, int> distance = new Dictionary<int, int>();
        // Предыдущие вершины на пути к кратчайшему пути:
        Dictionary<int, int> previous = new Dictionary<int, int>();
        // Посещенные вершины:
        HashSet<int> visited = new HashSet<int>();

        // Инициализация расстояний и предыдущих вершин:
        foreach (var vertex in graph.GetVertices())
        {
            distance[vertex] = int.MaxValue;
            previous[vertex] = -1;
        }

        // Расстояние от начальной вершины до неё самой равно 0:
        distance[source] = 0;

        // Основной цикл алгоритма:
        while (visited.Count < graph.GetVerticesCount())
        {
            // Выбор вершины с минимальным расстоянием:
            int currentVertex = GetMinDistanceVertex(distance, visited);
            visited.Add(currentVertex);

            // Обновление расстояний до соседей текущей вершины:
            foreach (var neighbor in graph.GetNeighbors(currentVertex))
            {
                int potentialDistance = distance[currentVertex] + neighbor.Weight;

                // Обновление расстояния, если нашли более короткий путь:
                if (potentialDistance < distance[neighbor.Destination])
                {
                    distance[neighbor.Destination] = potentialDistance;
                    previous[neighbor.Destination] = currentVertex;
                }
            }
        }

        // Получение кратчайшего пути от начальной вершины до конечной:
        return GetShortestPath(previous, source, destination);
    }

    /// <summary>
    /// Алгоритм Ли(Волновой алгоритм)
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public List<int> WaveAlgorithm(Graph graph, int source, int destination)
    {
        // Очередь для обхода графа "волной":
        Queue<int> queue = new Queue<int>();
        // Словарь для хранения предыдущих вершин на пути к кратчайшему пути:
        Dictionary<int, int> path = new Dictionary<int, int>();

        // Начальная вершина добавляется в очередь и помечается:
        queue.Enqueue(source);
        path[source] = -1;

        // Основной цикл обхода графа:
        while (queue.Count > 0)
        {
            // Текущая вершина извлекается из очереди:
            int currentVertex = queue.Dequeue();

            // Просмотр соседей текущей вершины:
            foreach (var neighbor in graph.GetNeighbors(currentVertex))
            {
                // Если соседняя вершина еще не посещена, добавляем её в очередь и помечаем предыдущей:
                if (!path.ContainsKey(neighbor.Destination))
                {
                    queue.Enqueue(neighbor.Destination);
                    path[neighbor.Destination] = currentVertex;
                }
            }
        }

        // Получение кратчайшего пути от начальной вершины до конечной:
        return GetShortestPath(path, source, destination);
    }

    /// <summary>
    /// Алгорим A*
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public List<int> AStar(Graph graph, int source, int destination)
    {
        // Расстояния от начальной вершины до каждой другой:
        Dictionary<int, int> distance = new Dictionary<int, int>();
        // Предыдущие вершины на пути к кратчайшему пути:
        Dictionary<int, int> previous = new Dictionary<int, int>();
        // Эвристические значения для каждой вершины:
        Dictionary<int, int> heuristic = new Dictionary<int, int>();

        // Инициализация расстояний, предыдущих вершин и эвристик:
        foreach (var vertex in graph.GetVertices())
        {
            distance[vertex] = int.MaxValue;
            previous[vertex] = -1;
            heuristic[vertex] = CalculateHeuristic(vertex, destination);
        }

        // Расстояние от начальной вершины до неё самой равно 0:
        distance[source] = 0;

        // Посещенные вершины:
        HashSet<int> visited = new HashSet<int>();

        // Основной цикл алгоритма:
        while (visited.Count < graph.GetVerticesCount())
        {
            // Выбор вершины с минимальным значением A*:
            int currentVertex = GetMinAStarValueVertex(distance, heuristic, visited);
            visited.Add(currentVertex);

            // Обновление расстояний до соседей текущей вершины:
            foreach (var neighbor in graph.GetNeighbors(currentVertex))
            {
                int potentialDistance = distance[currentVertex] + neighbor.Weight;
                int totalEstimate = potentialDistance + heuristic[neighbor.Destination];

                // Обновление расстояния, если нашли более короткий путь:
                if (totalEstimate < distance[neighbor.Destination])
                {
                    distance[neighbor.Destination] = potentialDistance;
                    previous[neighbor.Destination] = currentVertex;
                }
            }
        }
        // Получение кратчайшего пути от начальной вершины до конечной:
        return GetShortestPath(previous, source, destination);
    }


    /// <summary>
    /// Вспомогательный метод для получения вершины с минимальным значением A* функции
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="heuristic"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    private int GetMinAStarValueVertex(Dictionary<int, int> distance, Dictionary<int, int> heuristic, HashSet<int> visited)
    {
        int minAStarValue = int.MaxValue;
        int minVertex = -1;

        foreach (var vertex in distance.Keys)
        {
            if (!visited.Contains(vertex))
            {
                int aStarValue = distance[vertex] + heuristic[vertex];
                if (aStarValue < minAStarValue)
                {
                    minAStarValue = aStarValue;
                    minVertex = vertex;
                }
            }
        }

        return minVertex;
    }

    /// <summary>
    /// Вспомогательный метод для расчета эвристической функции
    /// </summary>
    /// <param name="currentVertex"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    private int CalculateHeuristic(int currentVertex, int destination)
    {
        // Здесь необходимо реализовать вашу эвристическую функцию
        // Например, можно использовать расстояние между вершинами или другие критерии
        return Math.Abs(currentVertex - destination);
    }

    /// <summary>
    /// Вспомогательный метод для получения вершины с минимальным расстоянием
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    private int GetMinDistanceVertex(Dictionary<int, int> distance, HashSet<int> visited)
    {
        int minDistance = int.MaxValue;
        int minVertex = -1;

        foreach (var vertex in distance.Keys)
        {
            if (!visited.Contains(vertex) && distance[vertex] < minDistance)
            {
                minDistance = distance[vertex];
                minVertex = vertex;
            }
        }

        return minVertex;
    }

    /// <summary>
    /// Вспомогательный метод для получения кратчайшего пути
    /// </summary>
    /// <param name="previous"></param>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    private List<int> GetShortestPath(Dictionary<int, int> previous, int source, int destination)
    {
        List<int> path = new List<int>();
        int current = destination;

        while (current != -1)
        {
            path.Insert(0, current);
            current = previous[current];
        }

        return path.Count > 1 ? path : null;
    }
}

/// <summary>
/// Класс для работы с консольным приложением
/// </summary>
class ConsoleApp
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите количество вершин:");
        int verticesCount = int.Parse(Console.ReadLine());

        Graph graph = new Graph();

        for (int i = 1; i <= verticesCount; i++)
        {
            graph.AddVertex(i);
        }

        Console.WriteLine("Теперь введите веса ребер в формате матрицы смежности:");

        int[,] adjacencyMatrix = new int[verticesCount, verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = i + 1; j < verticesCount; j++)
            {
                Console.Write($"Введите вес ребра между вершиной {i + 1} и вершиной {j + 1} (если нет ребра, введите 0): ");
                adjacencyMatrix[i, j] = int.Parse(Console.ReadLine());

                if (adjacencyMatrix[i, j] != 0)
                {
                    graph.AddEdge(i + 1, j + 1, adjacencyMatrix[i, j]);
                }
            }
        }

        // Вывод взвешенного графа в виде таблицы
        Console.WriteLine("\nВзвешенный граф в виде таблицы:");
        PrintWeightedGraph(adjacencyMatrix, verticesCount);

        Console.WriteLine("Введите начальную вершину:");
        int sourceVertex = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите конечную вершину:");
        int destinationVertex = int.Parse(Console.ReadLine());

        AlgorithmService algorithmService = new AlgorithmService();

        // Вызов алгоритма Ли
        List<int> shortestPathWave = algorithmService.WaveAlgorithm(graph, sourceVertex, destinationVertex);
        PrintShortestPath("Ли", shortestPathWave);

        // Вызов алгоритма Дейкстры
        List<int> shortestPathDijkstra = algorithmService.Dijkstra(graph, sourceVertex, destinationVertex);
        PrintShortestPath("Дейкстры", shortestPathDijkstra);

        // Вызов алгоритма A*
        List<int> shortestPathAStar = algorithmService.AStar(graph, sourceVertex, destinationVertex);
        PrintShortestPath("A*", shortestPathAStar);
    }

    static void PrintWeightedGraph(int[,] adjacencyMatrix, int verticesCount)
    {
        Console.Write("   ");
        for (int i = 1; i <= verticesCount; i++)
        {
            Console.Write($" {i}");
        }
        Console.WriteLine();

        for (int i = 0; i < verticesCount; i++)
        {
            Console.Write($"{i + 1} |");
            for (int j = 0; j < verticesCount; j++)
            {
                if (i == j)
                {
                    Console.Write(" X");
                }
                else
                {
                    Console.Write($" {adjacencyMatrix[i, j]}");
                }
            }
            Console.WriteLine();
        }
    }




    /// <   summary>
    /// Вспомогательный метод для вывода кратчайшего пути в консоль
    /// </summary>
    /// <param name="algorithmName"></param>
    /// <param name="path"></param>
    static void PrintShortestPath(string algorithmName, List<int> path)
    {
        Console.WriteLine($"Кратчайший путь ({algorithmName}):");
        if (path != null)
        {
            Console.WriteLine(string.Join(" -> ", path));
        }
        else
        {
            Console.WriteLine("Путь не найден.");
        }
    }
}