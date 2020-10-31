using System;
using System.Threading;

namespace Robots
{
    internal class Solver
    {
        private Location _robotLocation;

        public void Solve()
        {
            var m = 15;
            var n = 10;
            var treasureLocation = new Location(6, 5);
            _robotLocation = new Location(2, 3);
            var world = new World(m, n, treasureLocation);
            var robot = new Robot(world, _robotLocation);

            DisplayWorldAndRobot(m, n, treasureLocation, _robotLocation);

            var foundTreasure = MoveToTopLeft(robot);
            if (!foundTreasure)
            {
                foundTreasure = MoveToBottomRight(robot);
            }
        }
        private bool MoveToBottomRight(Robot robot)
        {
            var xDirection = Direction.Right;
            while (true)
            {
                var direction = DirectionOfTreasure(robot);
                if (direction is object)
                {
                    MoveRobot(robot, direction.Value);
                    return true;
                }
                else if (robot.LookInDirection(xDirection) == Content.Empty)
                {
                    MoveRobot(robot, xDirection);
                }
                else if (robot.LookInDirection(Direction.Down) == Content.Empty)
                {
                    MoveRobot(robot, Direction.Down);
                    xDirection = xDirection == Direction.Left ? Direction.Right : Direction.Left;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool MoveToTopLeft(Robot robot)
        {
            while (true)
            {
                var direction = DirectionOfTreasure(robot);
                if (direction is object)
                {
                    MoveRobot(robot, direction.Value);
                    return true;
                }
                else if (robot.LookInDirection(Direction.Left) == Content.Empty)
                {
                    MoveRobot(robot, Direction.Left);
                }
                else if (robot.LookInDirection(Direction.Up) == Content.Empty)
                {
                    MoveRobot(robot, Direction.Up);
                }
                else
                {
                    return false;
                }
            }
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

        private static Direction? DirectionOfTreasure(Robot robot)
        {
            if (robot.LookInDirection(Direction.Up) == Content.Treasure) return Direction.Up;
            if (robot.LookInDirection(Direction.Down) == Content.Treasure) return Direction.Down;
            if (robot.LookInDirection(Direction.Left) == Content.Treasure) return Direction.Left;
            if (robot.LookInDirection(Direction.Right) == Content.Treasure) return Direction.Right;

            return null;
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
