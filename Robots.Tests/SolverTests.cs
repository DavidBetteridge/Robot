using Robots;
using System;
using Xunit;

namespace Robot.Tests
{
    public class SolverTests
    {
        [Fact(DisplayName = "The robot can only move one cell at a time")]
        public void RobotCanOnlyMoveOneSquareAtATime()
        {
            const int width = 15;
            const int height = 10;

            var treasureLocation = new Location(6, 5);
            var robotLocation = new Location(2, 3);

            var previousLocation = robotLocation;
            var solver = new Solver(width, height, treasureLocation, robotLocation, newLocation =>
            {
                var xDiff = Math.Abs(newLocation.X - previousLocation.X);
                var yDiff = Math.Abs(newLocation.Y - previousLocation.Y);
                Assert.True((xDiff + yDiff) <= 1);
                previousLocation = newLocation;
            });

            solver.Solve();
        }

        [Fact(DisplayName = "The robot ends up on the cell containing the treasure")]
        public void TheRobotGetsTheTreasure()
        {
            const int width = 15;
            const int height = 10;

            var random = new Random();
            var treasureLocation = new Location(random.Next(0, width), random.Next(0, height));
            var robotLocation = new Location(random.Next(0, width), random.Next(0, height));

            var previousLocation = robotLocation;
            var solver = new Solver(width, height, treasureLocation, robotLocation, newLocation => { previousLocation = newLocation; });
            solver.Solve();

            Assert.Equal(treasureLocation.X, previousLocation.X);
            Assert.Equal(treasureLocation.Y, previousLocation.Y);
        }
    }
}
