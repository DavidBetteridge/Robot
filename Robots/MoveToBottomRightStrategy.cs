namespace Robots
{
    internal class MoveToBottomRightStrategy : IStrategy
    {
        private enum State
        {
            FindingRightEdge,
            HeadingRight,
            HeadingDownThenLeft,
            HeadingDownThenRight,
            HeadingLeft
        }

        private State _currentState = State.FindingRightEdge;

        private int xOffset = 0;
        private int yOffset = 0;
        private int maxXOffset = int.MaxValue;

        public Direction? SuggestDirection(Robot robot)
        {
            return _currentState switch
            {
                State.FindingRightEdge => FromStateFindingRightEdge(robot),
                State.HeadingRight => FromStateHeadingRight(robot),
                State.HeadingDownThenLeft => FromStateHeadingDownTheLeft(robot),
                State.HeadingLeft => FromStateHeadingLeft(robot),
                State.HeadingDownThenRight => FromStateHeadingDownThenRight(robot),
                _ => null,
            };
        }

        private Direction? FromStateHeadingDownThenRight(Robot robot)
        {
            if (ShouldContinueHeadingDown(robot))
            {
                return MoveDown();
            }
            else
            {
                ChangeStateToHeadingRight();
                return MoveRight();
            }
        }

        private Direction? FromStateHeadingLeft(Robot robot)
        {
            if (CanHeadLeft())
            {
                return MoveLeft();
            }
            else
            {
                ChangeStateToHeadingDownThenRight(2);
                return MoveDownIfPossible(robot);
            }
        }

        private Direction? FromStateHeadingDownTheLeft(Robot robot)
        {
            if (ShouldContinueHeadingDown(robot))
            {
                return MoveDown();
            }
            else
            {
                ChangeStateToHeadingLeft();
                return MoveLeft();
            }
        }

        private Direction? FromStateHeadingRight(Robot robot)
        {
            if (CanHeadRight())
            {
                return MoveRight();
            }
            else
            {
                ChangeStateToHeadingDownThenLeft(numberToGoDown: 2);
                return MoveDownIfPossible(robot);
            }
        }

        private Direction FromStateFindingRightEdge(Robot robot)
        {
            if (AtRightEdge(robot))
            {
                RecordLocationOfRightEdge();
                ChangeStateToHeadingDownThenLeft(numberToGoDown: 3);
                return MoveLeft();
            }
            else
            {
                return MoveRight();
            }
        }

        private void ChangeStateToHeadingRight()
        {
            _currentState = State.HeadingRight;
        }

        private void ChangeStateToHeadingLeft()
        {
            _currentState = State.HeadingLeft;
        }

        private void RecordLocationOfRightEdge()
        {
            maxXOffset = xOffset - 1;
        }

        private void ChangeStateToHeadingDownThenLeft(int numberToGoDown)
        {
            yOffset = numberToGoDown;
            _currentState = State.HeadingDownThenLeft;
        }

        private void ChangeStateToHeadingDownThenRight(int numberToGoDown)
        {
            yOffset = numberToGoDown;
            _currentState = State.HeadingDownThenRight;
        }


        private bool CanHeadRight()
        {
            return xOffset < maxXOffset;
        }

        private bool CanHeadLeft()
        {
            return xOffset > 0;
        }

        private bool ShouldContinueHeadingDown(Robot robot)
        {
            return yOffset > 0 && robot.Look(Direction.Down) == Content.Empty;
        }

        private static bool AtRightEdge(Robot robot)
        {
            return robot.Look(Direction.Right) == Content.Wall;
        }

        private static Direction? MoveDownIfPossible(Robot robot)
        {
            if (robot.Look(Direction.Down) != Content.Empty)
                return null;
            else
                return Direction.Down;
        }

        private Direction MoveRight()
        {
            xOffset++;
            return Direction.Right;
        }

        private Direction MoveLeft()
        {
            xOffset--;
            return Direction.Left;
        }

        private Direction MoveDown()
        {
            yOffset--;
            return Direction.Down;
        }
    }
}