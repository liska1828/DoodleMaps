namespace Doodle_Maps.MapGeneration;

public class MapPrinter
{
    public void Print(string[,] maze, List<Point> path)
    {
        var pathSet = new HashSet<Point>(path ?? new List<Point>());

        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var point = new Point(x, y);
                string toPrint;

                if (pathSet.Contains(point))
                    toPrint = "**"; // маршрут виділяємо зірочками
                else
                    toPrint = maze[y, x] + "" + maze[y, x]; // подвоюємо символи

                Console.Write(toPrint);
            }
            Console.WriteLine();
        }
    }
}
