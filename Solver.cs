using System;
using System.Threading;

namespace Robots
{
    internal class Solver
    {
        private Location _robotLocation;
        private readonly World _world;
        private readonly Robot _robot;

        public Solver()
        {
            var m = 15;
            var n = 10;
            var treasureLocation = new Location(6, 5);
            _robotLocation = new Location(2, 3);
            _world = new World(m, n, treasureLocation);
            _robot = new Robot(_world, _robotLocation);

            DisplayWorldAndRobot(m, n, treasureLocation, _robotLocation);
        }

        public void Solve()
        {
            var foundTreasure = FollowStrategy(_world, _robot, new MoveToTopLeftStrategy());
            if (!foundTreasure)
            {
                foundTreasure = FollowStrategy(_world, _robot, new MoveToBottomRightStrategy());
            }
        }

        private bool FollowStrategy(World world, Robot robot, IStrategy strategy)
        {
            if (world.CellContent(_robotLocation) == Content.Treasure) return true;

            var newDirection = strategy.SuggestDirection(robot);
            if (newDirection == null) return false;

            MoveRobot(robot, newDirection.Value);

            return FollowStrategy(world, robot, strategy);
        }

        private void MoveRobot(Robot robot, Direction direction)
        {
            Console.SetCursorPosition(_robotLocation.X + 1, _robotLocation.Y + 1);
            Console.Write(" ");

            switch (direction)
            {
                case Direction.Left:
                    _robotLocation = _robotLocation.Left;
                    robot.MoveLeft();
                    break;
                case Direction.Right:
                    _robotLocation = _robotLocation.Right;
                    robot.MoveRight();
                    break;
                case Direction.Up:
                    _robotLocation = _robotLocation.Up;
                    robot.MoveUp();
                    break;
                case Direction.Down:
                    _robotLocation = _robotLocation.Down;
                    robot.MoveDown();
                    break;
                default:
                    break;
            }

            Console.SetCursorPosition(_robotLocation.X + 1, _robotLocation.Y + 1);
            Console.Write("R");
            Console.SetCursorPosition(0, 20);

            Thread.Sleep(TimeSpan.FromSeconds(.25));
        }



        private static void DisplayWorldAndRobot(int width, int height, Location treasureLocation, Location robotLocation)
        {
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
    }

}
