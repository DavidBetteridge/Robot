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
            return _world.CellContent(_currentLocation.OffsetByDirection(direction));
        }

        public Location Move(Direction direction)
        {
            _currentLocation = _currentLocation.OffsetByDirection(direction);
            return _currentLocation;
        }
    }
}
