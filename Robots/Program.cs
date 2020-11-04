using System;

namespace Robots
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 10;
            const int height = 10;

            var treasureLocation = new Location(0, 7);
            var robotLocation = new Location(0,0);

            var display = new Display();
            var solver = new Solver(width, height, treasureLocation, robotLocation, display.RobotMoved);

            display.DisplayWorldAndRobot(width, height, treasureLocation, robotLocation);
            solver.Solve();
        }

        static void AllLocations()
        {
            const int width = 10;
            const int height = 10;

            for (int treasureX = 0; treasureX < width; treasureX++)
            {
                for (int treasureY = 0; treasureY < height; treasureY++)
                {
                    Console.WriteLine($"treasureX={treasureX}, treasureY={treasureY}");

                    for (int robotX = 0; robotX < width; robotX++)
                    {
                        for (int robotY = 0; robotY < height; robotY++)
                        {
                     //       Console.WriteLine($"robotX={robotX}, robotY={robotY}");

                            var treasureLocation = new Location(treasureX, treasureY);
                            var robotLocation = new Location(robotX, robotY);

                            var previousLocation = robotLocation;
                            var solver = new Solver(width, height, treasureLocation, robotLocation, newLocation =>
                            {
                                previousLocation = newLocation;
                            });
                            solver.Solve();

                            if (treasureLocation.X != previousLocation.X || treasureLocation.Y != previousLocation.Y)
                            {
                                Console.WriteLine("Oh");
                            }
                        }
                    }
                }
            }
        }
    }
}
