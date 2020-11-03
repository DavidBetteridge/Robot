namespace Robots
{
    internal class WalkLeft : IStrategy
    {
        private int _distance;

        public WalkLeft(int distance)
        {
            _distance = distance;
        }
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (_distance > 0)
            {
                sharedState.DistanceFromLeftEdge--;
                _distance--;
                return Direction.Left;
            }
            else
            {
                return null;
            }
        }
    }

}
