namespace Robots
{
    internal class WalkUp : IStrategy
    {
        private int _distance;

        public WalkUp(int distance)
        {
            _distance = distance;
        }
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (_distance > 0 && (robot.Look(Direction.Up) == Content.Empty))
            {
                _distance--;
                sharedState.VerticalDistanceFromStart++;
                return Direction.Up;
            }
            else
            {
                return null;
            }
        }
    }

}
