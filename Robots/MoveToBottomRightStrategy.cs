namespace Robots
{
    internal class MoveToBottomRightStrategy : IStrategy
    {
        private Direction xDirection = Direction.Right;
        private int downLeftToMove = 0;
        private int xOffset = 0;
        private int maxXOffset = int.MaxValue;
        public Direction? SuggestDirection(Robot robot)
        {
            var direction = robot.DirectionOfTreasure();
            if (direction is object)
            {
                return direction.Value;
            }
            else if (downLeftToMove > 0 && robot.Look(Direction.Down) == Content.Empty)
            {
                downLeftToMove--;
                return Direction.Down;
            }

            else if (xDirection == Direction.Left && xOffset > 0)
            {
                xOffset--;
                return Direction.Left;
            }

            else if (xDirection == Direction.Right && xOffset < maxXOffset && robot.Look(Direction.Right) == Content.Empty)
            {
                xOffset++;
                return Direction.Right;
            }

            else if (xDirection == Direction.Right && robot.Look(Direction.Right) != Content.Empty)
            {
                xOffset--;
                maxXOffset = xOffset;
                ToggleLeftRight();
                downLeftToMove = 3;
                return Direction.Left;
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
