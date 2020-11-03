namespace Robots
{
    internal class MoveToBottomRightStrategy : IStrategy
    {
        private IState _currentState = new FindingRightEdgeState();
        private SharedState _sharedState = new SharedState();

        public Direction? SuggestDirection(Robot robot)
        {
            var (SharedState, NewDirection, NextState) = _currentState.Next(_sharedState, robot);

            _sharedState = SharedState;
            _currentState = NextState;

            return NewDirection;

        }

        class SharedState
        {
            public int DistanceFromLeftEdge { get; set; }
            public int Width { get; set; }
        }

        interface IState
        {
            (SharedState SharedState, Direction? NewDirection, IState NextState) Next(SharedState state, Robot robot);
        }

        class FindingRightEdgeState : IState
        {
            public (SharedState, Direction?, IState) Next(SharedState state, Robot robot)
            {
                if (AtRightEdge(robot))
                {
                    state.Width = state.DistanceFromLeftEdge - 1;
                    var nextState = new HeadingDownThenTurnState(numberToGoDown: 3, Direction.Left);
                    return (state, Direction.Left, nextState);
                }
                else
                {
                    state.DistanceFromLeftEdge++;
                    return (state, Direction.Right, this);
                }
            }

            private static bool AtRightEdge(Robot robot)
            {
                return robot.Look(Direction.Right) == Content.Wall;
            }
        }

        class HeadingDownThenTurnState : IState
        {
            private int _numberToGoDown;
            private readonly Direction _directionToTurn;

            public HeadingDownThenTurnState(int numberToGoDown, Direction directionToTurn)
            {
                _numberToGoDown = numberToGoDown;
                _directionToTurn = directionToTurn;
            }

            public (SharedState, Direction?, IState) Next(SharedState state, Robot robot)
            {
                if (ShouldContinueHeadingDown(robot))
                {
                    _numberToGoDown--;
                    return (state, Direction.Down, this);
                }
                else
                {
                    if (_directionToTurn == Direction.Right)
                    {
                        state.DistanceFromLeftEdge++;
                        var nextState = new HeadingRightState();
                        return (state, Direction.Right, nextState);
                    }
                    else
                    {
                        state.DistanceFromLeftEdge--;
                        var nextState = new HeadingLeftState();
                        return (state, Direction.Left, nextState);
                    }
                }
            }

            private bool ShouldContinueHeadingDown(Robot robot)
            {
                return _numberToGoDown > 0 && robot.Look(Direction.Down) == Content.Empty;
            }
        }

        class HeadingRightState : IState
        {
            public (SharedState, Direction?, IState) Next(SharedState state, Robot robot)
            {
                var canHeadRight = state.DistanceFromLeftEdge < state.Width;
                if (canHeadRight)
                {
                    state.DistanceFromLeftEdge++;
                    return (state, Direction.Right, this);
                }
                else
                {
                    if (robot.Look(Direction.Down) != Content.Empty)
                        return (state, null, null);
                    else
                    {
                        var nextState = new HeadingDownThenTurnState(numberToGoDown: 2, Direction.Left);
                        return (state, Direction.Down, nextState);
                    }
                }
            }
        }

        class HeadingLeftState : IState
        {
            public (SharedState, Direction?, IState) Next(SharedState state, Robot robot)
            {
                var canHeadLeft = state.DistanceFromLeftEdge > 1;
                if (canHeadLeft)
                {
                    state.DistanceFromLeftEdge--;
                    return (state, Direction.Left, this);
                }
                else
                {
                    if (robot.Look(Direction.Down) != Content.Empty)
                        return (state, null, null);
                    else
                    {
                        var nextState = new HeadingDownThenTurnState(numberToGoDown: 2, Direction.Right);
                        return (state, Direction.Down, nextState);
                    }
                }
            }
        }
    }
}