namespace Robots
{
    class World
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Location _treasureLocation;

        public World(int width, int height, Location treasureLocation)
        {
            _width = width;
            _height = height;
            _treasureLocation = treasureLocation;
        }

        public Content CellContent(Location location)
        {
            if (location.X == -1) return Content.Wall;
            if (location.Y == -1) return Content.Wall;
            if (location.X == _width) return Content.Wall;
            if (location.Y == _height) return Content.Wall;

            if (location.X < -1) return Content.OutOfWorld;
            if (location.Y < -1) return Content.OutOfWorld;
            if (location.X > _width) return Content.OutOfWorld;
            if (location.Y > _height) return Content.OutOfWorld;

            if (location.X == _treasureLocation.X && location.Y == _treasureLocation.Y) return Content.Treasure;

            return Content.Empty;
        }
    }

}
