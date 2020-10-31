namespace Robots
{
    internal class MoveToBottomRightStrategy : IStrategy
    {
        private Direction xDirection = Direction.Right;
        public Direction? SuggestDirection(Robot robot)
        {
            var direction = robot.DirectionOfTreasure();
            if (direction is object)
            {
                return direction.Value;
            }
            else if (robot.LookInDirection(xDirection) == Content.Empty)
            {
                return xDirection;
            }
            else if (robot.LookInDirection(Direction.Down) == Content.Empty)
            {
                xDirection = xDirection == Direction.Left ? Direction.Right : Direction.Left;
                return Direction.Down;
            }
            else
            {
                return null;
            }
        }
    }

}
