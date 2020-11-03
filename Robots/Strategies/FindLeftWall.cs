namespace Robots
{
    internal class FindLeftWall : IStrategy
    {
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (robot.Look(Direction.Left) == Content.Empty)
            {
                return Direction.Left;
            }
            else
            {
                return null;
            }
        }
    }

}
