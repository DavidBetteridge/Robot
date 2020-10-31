using System;

namespace Robots
{
    internal class Solver
    {
        private Location _robotLocation;
        private readonly Action<Location> _robotMoved;
        private readonly World _world;
        private readonly Robot _robot;

        public Solver(int width, int height, Location treasureLocation, Location robotLocation, Action<Location> robotMoved)
        {
            _robotLocation = robotLocation;
            _robotMoved = robotMoved;
            _world = new World(width, height, treasureLocation);
            _robot = new Robot(_world, _robotLocation);
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
            _robotMoved(_robotLocation);
        }
    }
}
