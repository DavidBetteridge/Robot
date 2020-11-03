using System;

namespace Robots
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 15;
            const int height = 15;

            var treasureLocation = new Location(6, 11);
            var robotLocation = new Location(2, 3);

            var display = new Display();
            var solver = new Solver(width, height, treasureLocation, robotLocation, display.RobotMoved);

            display.DisplayWorldAndRobot(width, height, treasureLocation, robotLocation);
            solver.Solve();
        }
    }
}
