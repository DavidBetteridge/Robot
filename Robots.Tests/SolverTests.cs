using Robots;
using System;
using Xunit;

namespace Robot.Tests
{
    public class SolverTests
    {
        [Fact(DisplayName = "The robot can only move one cell at a time and the robot ends up on the cell containing the treasure")]
        public void KnownValues()
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

            Assert.Equal(treasureLocation.X, previousLocation.X);
            Assert.Equal(treasureLocation.Y, previousLocation.Y);
        }

        [Fact(DisplayName = "The robot and treasure are placed at random")]
        public void RandomValues()
        {
            const int width = 15;
            const int height = 10;

            var random = new Random();
            var treasureLocation = new Location(random.Next(0, width), random.Next(0, height));
            var robotLocation = new Location(random.Next(0, width), random.Next(0, height));

            var previousLocation = robotLocation;
            var solver = new Solver(width, height, treasureLocation, robotLocation, newLocation =>
            {
                var xDiff = Math.Abs(newLocation.X - previousLocation.X);
                var yDiff = Math.Abs(newLocation.Y - previousLocation.Y);
                Assert.True((xDiff + yDiff) <= 1);
                previousLocation = newLocation;
            });
            solver.Solve();

            Assert.Equal(treasureLocation.X, previousLocation.X);
            Assert.Equal(treasureLocation.Y, previousLocation.Y);
        }


        [Theory(DisplayName = "All locations")]
        [InlineData(10,10)]
        [InlineData(10,11)]
        [InlineData(10,12)]
        [InlineData(10,13)]
        public void AllLocations(int width, int height)
        {
            for (int treasureX = 0; treasureX < width; treasureX++)
            {
                for (int treasureY = 0; treasureY < height; treasureY++)
                {
                    for (int robotX = 0; robotX < width; robotX++)
                    {
                        for (int robotY = 0; robotY < height; robotY++)
                        {
                            var treasureLocation = new Location(treasureX, treasureY);
                            var robotLocation = new Location(robotX, robotY);

                            var previousLocation = robotLocation;
                            var solver = new Solver(width, height, treasureLocation, robotLocation, newLocation =>
                            {
                                previousLocation = newLocation;
                            });
                            solver.Solve();

                            Assert.Equal(treasureLocation.X, previousLocation.X);
                            Assert.Equal(treasureLocation.Y, previousLocation.Y);
                        }
                    }
                }
            }
        }

    }
}
