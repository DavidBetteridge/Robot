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

        public Content LookInDirection(Direction direction)
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

        public void MoveLeft()
        {
            _currentLocation = _currentLocation.Left;
        }
        public void MoveRight()
        {
            _currentLocation = _currentLocation.Right;
        }
        public void MoveUp()
        {
            _currentLocation = _currentLocation.Up;
        }
        public void MoveDown()
        {
            _currentLocation = _currentLocation.Down;
        }
    }

}
