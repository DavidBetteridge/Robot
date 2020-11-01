using System;
using System.Threading;

namespace Robots
{
    internal class Display
    {
        private Location _currentLocation;

        public void DisplayWorldAndRobot(int width, int height, Location treasureLocation, Location robotLocation)
        {
            _currentLocation = robotLocation;

            Console.Clear();
            Console.WriteLine(new String('-', width + 2));
            for (int r = 0; r < height; r++)
            {
                Console.WriteLine('|' + new String(' ', width) + '|');
            }
            Console.WriteLine(new String('-', width + 2));

            Console.SetCursorPosition(robotLocation.X + 1, robotLocation.Y + 1);
            Console.Write("R");

            Console.SetCursorPosition(treasureLocation.X + 1, treasureLocation.Y + 1);
            Console.Write("T");
        }

        public void RobotMoved(Location newLocation)
        {
            Console.SetCursorPosition(_currentLocation.X + 1, _currentLocation.Y + 1);
            Console.Write(" ");

            Console.SetCursorPosition(newLocation.X + 1, newLocation.Y + 1);
            Console.Write("R");
            Console.SetCursorPosition(0, 20);

            _currentLocation = newLocation;

            Thread.Sleep(TimeSpan.FromSeconds(.25));
        }
    }
}
