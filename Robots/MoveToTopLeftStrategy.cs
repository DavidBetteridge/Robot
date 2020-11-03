namespace Robots
{
    internal class MoveToTopLeftStrategy : IStrategy
    {
        private bool _leftWallFound = false;

        public Direction? SuggestDirection(Robot robot)
        {
            if (!_leftWallFound && robot.Look(Direction.Left) == Content.Empty)
            {
                return Direction.Left;
            }
            else if (robot.Look(Direction.Left) == Content.Wall)
            {
                _leftWallFound = true;
                return Direction.Right;
            }
            else if (robot.Look(Direction.Up) == Content.Empty)
            {
                return Direction.Up;
            }
            else
            {
                return null;
            }
        }
    }

}
