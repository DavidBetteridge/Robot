namespace Robots
{
    class Robot
    {
        private readonly World _world;
        private Location _currentLocation;

        public Robot(World world, Location currentLocation)
        {
            _world = world;
            _currentLocation = currentLocation;
        }

        public Content Look(Direction direction)
        {
            return direction switch
            {
                Direction.Left => _world.CellContent(_currentLocation.Left),
                Direction.Right => _world.CellContent(_currentLocation.Right),
                Direction.Up => _world.CellContent(_currentLocation.Up),
                Direction.Down => _world.CellContent(_currentLocation.Down),
                _ => Content.Empty
            };
        }

        public Location Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    _currentLocation = _currentLocation.Left;
                    break;
                case Direction.Right:
                    _currentLocation = _currentLocation.Right;
                    break;
                case Direction.Up:
                    _currentLocation = _currentLocation.Up;
                    break;
                case Direction.Down:
                    _currentLocation = _currentLocation.Down;
                    break;
                default:
                    break;
            }

            return _currentLocation;
        }
    }
}
