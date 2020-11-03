namespace Robots
{
    interface IStrategy
    {
        public Direction? SuggestDirection(SharedState sharedState, Robot robot);
    }
}
