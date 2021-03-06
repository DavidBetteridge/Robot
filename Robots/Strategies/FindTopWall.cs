﻿namespace Robots
{
    internal class FindTopWall : IStrategy
    {
        public Direction? SuggestDirection(SharedState sharedState, Robot robot)
        {
            if (robot.Look(Direction.Up) == Content.Empty)
            {
                sharedState.VerticalDistanceFromStart++;
                return Direction.Up;
            }
            else
            {
                return null;
            }
        }
    }

}
