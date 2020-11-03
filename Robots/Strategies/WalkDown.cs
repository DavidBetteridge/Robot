namespace Robots
{
    internal class WalkDown : IStrategy
    {
        private int _distance;

        public WalkDown(int distance)
        {
            _distance = distance;
        }
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (_distance > 0 && (robot.Look(Direction.Down) == Content.Empty))
            {
                _distance--;
                return Direction.Down;
            }
            else
            {
                return null;
            }
        }
    }

}
