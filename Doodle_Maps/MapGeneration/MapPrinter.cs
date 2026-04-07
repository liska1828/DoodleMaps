namespace Doodle_Maps.MapGeneration;

public class MapPrinter
{
    public void Print(string[,] maze, List<Point> path)
    {
        var pathSet = new HashSet<Point>(path ?? new List<Point>());

        int width = maze.GetLength(0);
        int height = maze.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var point = new Point(x, y);
                string toPrint;

                if (pathSet.Contains(point))
                    toPrint = "**"; 
                else
                    toPrint = maze[x, y] + maze[x, y];

                Console.Write(toPrint);
            }
            Console.WriteLine();
        }
    }
}
