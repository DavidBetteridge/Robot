namespace Robots
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = 15;
            var n = 10;
            var treasureLocation = new Location(6, 5);
            var robotLocation = new Location(2, 3);

            var display = new Display();
            var solver = new Solver(m, n, treasureLocation, robotLocation, display.RobotMoved);

            display.DisplayWorldAndRobot(m, n, treasureLocation, robotLocation);
            solver.Solve();
        }
    }
}
