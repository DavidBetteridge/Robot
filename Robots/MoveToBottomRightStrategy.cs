namespace Robots
{
    internal class MoveToBottomRightStrategy : IStrategy
    {
        private Direction xDirection = Direction.Right;
        private int downLeftToMove = 0;
        public Direction? SuggestDirection(Robot robot)
        {
            var direction = robot.DirectionOfTreasure();
            if (direction is object)
            {
                return direction.Value;
            }
            else if (downLeftToMove > 0)
            {
                downLeftToMove--;
                return Direction.Down;
            }
            else if (robot.Look(xDirection) == Content.Empty)
            {
                return xDirection;
            }
            else if (robot.Look(Direction.Down) == Content.Empty)
            {
                ToggleLeftRight();
                downLeftToMove = 2;
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
