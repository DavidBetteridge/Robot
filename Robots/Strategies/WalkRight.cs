namespace Robots
{
    internal class WalkRight : IStrategy
    {
        private int _distance;

        public WalkRight(int distance)
        {
            _distance = distance;
        }
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (_distance > 0)
            {
                sharedState.DistanceFromLeftEdge++;
                _distance--;
                return Direction.Right;
            }
            else
            {
                return null;
            }
        }
    }

}
