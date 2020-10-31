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
            else if (robot.LookInDirection(Direction.Left) == Content.Empty)
            {
                return Direction.Left;
            }
            else if (robot.LookInDirection(Direction.Up) == Content.Empty)
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
