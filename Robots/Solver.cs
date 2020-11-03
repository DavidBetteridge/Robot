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
            var sharedState = new SharedState();

            Do(sharedState,
                   new FindLeftWall(),
                   new WalkRight(1),
                   new FindTopWall(),
                   new WalkDown(1),
                   new FindRightWall(),
                   new WalkLeft(1)
            );

            Repeat(sharedState,
                   () => new WalkDown(3),
                   () => new WalkLeft(sharedState.Width - 3),
                   () => new WalkDown(3),
                   () => new WalkRight(sharedState.Width - 3)
                );
        }

        private void Do(SharedState sharedState, params IStrategy[] strategies)
        {
            foreach (var strategy in strategies)
            {
                if (FollowStrategy(sharedState, strategy))
                    return;
            }
        }

        private void Repeat(SharedState sharedState, params Func<IStrategy>[] strategyCreators)
        {
            while (true)
            {
                foreach (var creator in strategyCreators)
                {
                    if (FollowStrategy(sharedState, creator()))
                        return;
                }
            }
        }

        private bool FollowStrategy(SharedState sharedState, IStrategy strategy)
        {
            if (_world.CellContent(_robotLocation) == Content.Treasure) return true;

            var direction = _robot.DirectionOfTreasure();
            if (direction is null)
            {
                direction = strategy.SuggestDirection(sharedState, _robot);
                if (direction == null) return false;
            }

            _robotLocation = _robot.Move(direction.Value);
            _publishRobotHasMoved(_robotLocation);

            return FollowStrategy(sharedState, strategy);
        }
    }
}
