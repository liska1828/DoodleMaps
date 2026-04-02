namespace Doodle_Maps;
using Doodle_Maps.MapGeneration;

public class DijkstraPathFinder : IPathFinder
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
    {
        var distances = new Dictionary<Point, int>();
        var origins = new Dictionary<Point, Point>();
        var visited = new HashSet<Point>();

        var queue = new PriorityQueue<Point, int>();

        distances[start] = 0;
        queue.Enqueue(start, 0);

        int visitedCount = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (visited.Contains(current)) continue;
            visited.Add(current);
            visitedCount++;

            if (current.Equals(destination))
            {
                return (ReconstructPath(origins, start, destination), visitedCount);
            }

            var neighbours = MapGenerator.GetNeighbours(current.Column, current.Row, map, 1, true);

            foreach (var next in neighbours)
            {
                int weight = GetWeight(map, next);
                int newDist = distances[current] + weight;

                if (!distances.ContainsKey(next) || newDist < distances[next])
                {
                    distances[next] = newDist;
                    origins[next] = current;
                    queue.Enqueue(next, newDist);
                }
            }
        }

        return (new List<Point>(), visitedCount);
    }

    private int GetWeight(string[,] map, Point point)
    {
        var value = map[point.Column, point.Row];

        if (int.TryParse(value, out int traffic))
        {
            return traffic;
        }

        return 1;
    }

    private List<Point> ReconstructPath(Dictionary<Point, Point> origins, Point start, Point end)
    {
        var path = new List<Point>();
        var current = end;

        while (!current.Equals(start))
        {
            path.Add(current);
            current = origins[current];
        }

        path.Add(start);
        path.Reverse();

        return path;
    }
}
