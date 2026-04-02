namespace Doodle_Maps;
using Doodle_Maps.MapGeneration;

public interface IPathFinder
{
    (List<Point> path, int visitedCount) FindPath(string[,] map, Point start, Point end);
}