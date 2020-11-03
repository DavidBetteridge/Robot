namespace Robots
{
    internal class FindRightWall : IStrategy
    {
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (robot.Look(Direction.Right) == Content.Empty)
            {
                sharedState.DistanceFromLeftEdge++;
                return Direction.Right;
            }
            else
            {
                sharedState.Width = sharedState.DistanceFromLeftEdge + 1;
                return null;
            }
        }
    }

}
