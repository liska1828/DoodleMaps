namespace Doodle_Maps;
using Doodle_Maps.MapGeneration;

class Program
{
    static void Main()
    {
        var options = new MapGeneratorOptions
        {
            Height = 30,
            Width = 30,
            Noise = 0.3f,
            AddTraffic = true,
            TrafficSeed = 1234,
            Seed = 1
        };

        var generator = new MapGenerator(options);
        var map = generator.Generate();

        var start = new Point(0, 0);
        var mid = new Point(1, 3);
        var end = new Point(2, 5);

        Console.WriteLine("Generated map:");
        new MapPrinter().Print(map, new List<Point>());

        var bfs = new BreadthFirstSearch();

        var pathBfs = bfs.FindPathThroughMiddle(map, start, mid, end);

        Console.WriteLine("\nBFS:");
        Console.WriteLine($"Довжина шляху: {pathBfs.Count}");
        new MapPrinter().Print(map, pathBfs);
        

        var dijkstra = new DijkstraPathFinder();
        var (pathDij, visitedDij) = dijkstra.FindPath(map, start, end);
        PrintResult("Dijkstra", map, pathDij, visitedDij);
        
        var astar = new AStar();
        var (pathAst, visitedAst) = astar.FindPath(map, start, end);
        PrintResult("A*", map, pathAst, visitedAst);

        Console.WriteLine(pathAst.Count);

        if (pathBfs.Count < pathDij.Count)
            Console.WriteLine($"\nНайкоротший шлях: BFS ({pathBfs.Count} кроків)");
        else if (pathDij.Count < pathBfs.Count)
            Console.WriteLine($"\nНайкоротший шлях: Dijkstra ({pathDij.Count} кроків)");
        else
            Console.WriteLine($"\nШляхи однакової довжини: {pathBfs.Count} кроків");
    }

    static void PrintResult(string algorithmName, string[,] map, List<Point> path, int visited)
    {
        Console.WriteLine($"\n{algorithmName}:");
        Console.WriteLine($"Відвідано вузлів: {visited}");
        Console.WriteLine($"Довжина шляху: {path.Count}");
        new MapPrinter().Print(map, path);
    }
}