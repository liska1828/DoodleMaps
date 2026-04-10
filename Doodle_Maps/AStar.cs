namespace Doodle_Maps;
using Doodle_Maps.MapGeneration;

public class AStar : IPathFinder
{
    public (List<Point>, int) FindPath(string[,] map, Point start, Point destination)
    {
        var gScore = new Dictionary<Point, int>();
        var parent = new Dictionary<Point, Point>();
        var queue = new PriorityQueue<Point, int>();

        gScore[start] = 0;
        queue.Enqueue(start, Heuristic(start, destination));

        int visited = 0;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            visited++;

            if (current.Equals(destination))
                break;

            foreach (var neighbor in GetNeighbors(current, map))
            {
                int tentative = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                {
                    gScore[neighbor] = tentative;
                    parent[neighbor] = current;

                    int f = tentative + Heuristic(neighbor, destination);
                    queue.Enqueue(neighbor, f);
                }
            }
        }

        var path = RestorePath(parent, start, destination);
        return (path, visited);
    }

    private int Heuristic(Point a, Point b)
    {
        return (Math.Abs(a.Row - b.Row) + Math.Abs(a.Column - b.Column)) * 600;
    }

    private List<Point> GetNeighbors(Point p, string[,] map)
    {
        var neighbors = new List<Point>();
        int[,] dirs = { {1,0}, {-1,0}, {0,1}, {0,-1} };

        for (int i = 0; i < 4; i++)
        {
            int r = p.Row + dirs[i, 0];
            int c = p.Column + dirs[i, 1];

            if (c >= 0 && r >= 0 &&
                c < map.GetLength(0) &&
                r < map.GetLength(1) &&
                map[c, r] != "█")
            {
                neighbors.Add(new Point(c, r));
            }
        }

        return neighbors;
    }

    private List<Point> RestorePath(Dictionary<Point, Point> parent, Point start, Point end)
    {
        var path = new List<Point>();

        if (!parent.ContainsKey(end))
            return path;

        var current = end;

        while (!current.Equals(start))
        {
            path.Add(current);
            current = parent[current];
        }

        path.Add(start);
        path.Reverse();

        return path;
    }
}
