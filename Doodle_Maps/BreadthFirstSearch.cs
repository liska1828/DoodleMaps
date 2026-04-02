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
                return (ReconstructPath(origins, start, destination), visitedCount);
            }

            var neighbours = MapGenerator.GetNeighbours(current.Column, current.Row, map, 1, true);

            foreach (var next in neighbours)
            {
                if (visited.Contains(next)) continue;

                visited.Add(next);
                origins[next] = current;
                queue.Enqueue(next);
            }
        }

        return (new List<Point>(), visitedCount);
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