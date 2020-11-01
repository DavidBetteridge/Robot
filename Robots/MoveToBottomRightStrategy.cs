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
            else if (robot.Look(xDirection) == Content.Empty)
            {
                return xDirection;
            }
            else if (robot.Look(Direction.Down) == Content.Empty)
            {
                ToggleLeftRight();
                return Direction.Down;
            }
            else
            {
                return null;
            }
        }

        private void ToggleLeftRight()
        {
            xDirection = xDirection == Direction.Left ? Direction.Right : Direction.Left;
        }
    }

}
