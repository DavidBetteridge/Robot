using System;
using System.Linq;

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
                   new FindRightWall()
            );

            Repeat(sharedState,
                   () => new WalkDown(DistanceToDescend()),
                   () => new WalkLeft(sharedState.Width - 1),
                   () => new WalkDown(3),
                   () => new WalkRight(sharedState.Width - 1)
                );

            int DistanceToDescend()
            {
                if (sharedState.FirstSweep)
                {
                    sharedState.FirstSweep = false;
                    return 1;
                }
                else
                    return 3;
            }
        }

        private void Do(SharedState sharedState, params IStrategy[] strategies)
        {
            strategies.Any(strategy => FollowStrategy(sharedState, strategy));
        }

        private void Repeat(SharedState sharedState, params Func<IStrategy>[] strategyCreators)
        {
            if (strategyCreators.Any(creator => FollowStrategy(sharedState, creator())))
                return;

            Repeat(sharedState, strategyCreators);
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
