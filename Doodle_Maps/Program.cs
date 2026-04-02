namespace Doodle_Maps;
using Doodle_Maps.MapGeneration;

class Program
{
    static void Main()
    {
        var options = new MapGeneratorOptions
        {
            Height = 10,
            Width = 30,
            Noise = 0.1f,
            AddTraffic = true,
            TrafficSeed = 1234
        };
        var generator = new MapGenerator(options);
        var map = generator.Generate();

        var start = new Point(0, 0);
        var end = new Point(options.Width - 2, options.Height - 2);

        Console.WriteLine("Generated map:");
        new MapPrinter().Print(map, new List<Point>());

        var bfs = new BreadthFirstSearch();
        var (pathBfs, visitedBfs) = bfs.FindPath(map, start, end);
        PrintResult("BFS", map, pathBfs, visitedBfs);
        
        var dijkstra = new DijkstraPathFinder();
        var (pathDij, visitedDij) = dijkstra.FindPath(map, start, end);
        PrintResult("Dijkstra", map, pathDij, visitedDij);
        
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