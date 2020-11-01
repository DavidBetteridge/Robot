using System;

namespace Robots
{
    public class Solver
    {
        private Location _robotLocation;
        private readonly Action<Location> _publishRobotHasMoved;
        private readonly World _world;
        private readonly Robot _robot;

        public Solver(int width, int height, Location treasureLocation, Location robotLocation, Action<Location> publishRobotHasMoved)
        {
            _robotLocation = robotLocation;
            _publishRobotHasMoved = publishRobotHasMoved;
            _world = new World(width, height, treasureLocation);
            _robot = new Robot(_world, _robotLocation);
        }

        public void Solve()
        {
            var foundTreasure = FollowStrategy(_world, _robot, new MoveToTopLeftStrategy());
            if (!foundTreasure)
            {
                _robotLocation = _robot.Move(Direction.Down);
                _publishRobotHasMoved(_robotLocation);

                foundTreasure = FollowStrategy(_world, _robot, new MoveToBottomRightStrategy());
            }
        }

        private bool FollowStrategy(World world, Robot robot, IStrategy strategy)
        {
            if (world.CellContent(_robotLocation) == Content.Treasure) return true;

            var newDirection = strategy.SuggestDirection(robot);
            if (newDirection == null) return false;

            _robotLocation = robot.Move(newDirection.Value);
            _publishRobotHasMoved(_robotLocation);

            return FollowStrategy(world, robot, strategy);
        }
    }
}
