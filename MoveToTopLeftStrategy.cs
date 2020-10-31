namespace Robots
{
    internal class MoveToTopLeftStrategy : IStrategy
    {
        public Direction? SuggestDirection(Robot robot)
        {
            var direction = robot.DirectionOfTreasure();
            if (direction is object)
            {
                return direction.Value;
            }
            else if (robot.Look(Direction.Left) == Content.Empty)
            {
                return Direction.Left;
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
