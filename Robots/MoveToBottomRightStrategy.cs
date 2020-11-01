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
            var direction = robot.DirectionOfTreasure();
            if (direction is object)
            {
                return direction.Value;
            }
            else
            {
                switch (_currentState)
                {
                    case State.FindingRightEdge:
                        if (AtRightEdge(robot))
                        {
                            RecordLocationOfRightEdge();
                            ChangeToHeadingDownThenLeft(numberToGoDown: 3);
                            return MoveLeft();
                        }
                        else
                        {
                            return MoveRight();
                        }

                    case State.HeadingRight:
                        if (CanHeadRight())
                        {
                            return MoveRight();
                        }
                        else
                        {
                            ChangeToHeadingDownThenLeft(numberToGoDown: 2);
                            return MoveDownIfPossible(robot);
                        }

                    case State.HeadingDownThenLeft:
                        if (ShouldContinueHeadingDown(robot))
                        {
                            return MoveDown();
                        }
                        else
                        {
                            ChangeToHeadingLeft();
                            return MoveLeft();
                        }

                    case State.HeadingLeft:
                        if (CanHeadLeft())
                        {
                            return MoveLeft();
                        }
                        else
                        {
                            ChangeToHeadingDownThenRight(2);
                            return MoveDownIfPossible(robot);
                        }

                    case State.HeadingDownThenRight:
                        if (ShouldContinueHeadingDown(robot))
                        {
                            return MoveDown();
                        }
                        else
                        {
                            ChangeToHeadingRight();
                            return MoveRight();
                        }

                    default:
                        break;
                }
            }

            return null;
        }

        private void ChangeToHeadingRight()
        {
            _currentState = State.HeadingRight;
        }

        private void ChangeToHeadingLeft()
        {
            _currentState = State.HeadingLeft;
        }

        private void RecordLocationOfRightEdge()
        {
            maxXOffset = xOffset - 1;
        }

        private void ChangeToHeadingDownThenLeft(int numberToGoDown)
        {
            yOffset = numberToGoDown;
            _currentState = State.HeadingDownThenLeft;
        }

        private void ChangeToHeadingDownThenRight(int numberToGoDown)
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
