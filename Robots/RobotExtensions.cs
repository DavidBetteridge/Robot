namespace Robots
{
    public static class RobotExtensions
    {
        internal static Direction? DirectionOfTreasure(this Robot robot)
        {
            if (robot.Look(Direction.Up) == Content.Treasure) return Direction.Up;
            if (robot.Look(Direction.Down) == Content.Treasure) return Direction.Down;
            if (robot.Look(Direction.Left) == Content.Treasure) return Direction.Left;
            if (robot.Look(Direction.Right) == Content.Treasure) return Direction.Right;

            return null;
        }
    }
}
