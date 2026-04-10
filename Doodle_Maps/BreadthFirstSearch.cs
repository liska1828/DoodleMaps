namespace Doodle_Maps;

using Doodle_Maps.MapGeneration;
public class BreadthFirstSearch : IPathFinder
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
    {
        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        var origins = new Dictionary<Point, Point>();

        int visitedCount = 0;

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visitedCount++;

            if (current.Equals(destination))
            {
                return (Reconstruct(origins, start, destination), visitedCount);
            }

            foreach (var next in GetNeighbours(current, map))
            {
                if (visited.Contains(next))
                    continue;

                visited.Add(next);
                origins[next] = current;
                queue.Enqueue(next);
            }
        }

        return (new List<Point>(), visitedCount);
    }

    private List<Point> GetNeighbours(Point p, string[,] map)
    {
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        var result = new List<Point>();

        for (int i = 0; i < 8; i++)
        {
            int x = p.Column + dx[i];
            int y = p.Row + dy[i];

            if (x >= 0 && y >= 0 &&
                x < map.GetLength(1) &&
                y < map.GetLength(0))
            {
                if (map[y, x] != "#")
                    result.Add(new Point(x, y));
            }
        }

        return result;
    }

    private List<Point> Reconstruct(Dictionary<Point, Point> origins, Point start, Point end)
    {
        var path = new List<Point>();
        var current = end;

        while (!current.Equals(start))
        {
            path.Add(current);

            if (!origins.ContainsKey(current))
                return new List<Point>();

            current = origins[current];
        }

        path.Add(start);
        path.Reverse();

        return path;
    }

    public List<Point> FindPathThroughMiddle(string[,] map, Point start, Point middle, Point end)
    {
        var first = FindPath(map, start, middle);
        var second = FindPath(map, middle, end);

        if (first.Item1.Count == 0 || second.Item1.Count == 0)
            return new List<Point>();

        first.Item1.AddRange(second.Item1.Skip(1));
        return first.Item1;
    }
}