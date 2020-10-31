namespace Robots
{
    struct Location
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Location Left => new Location(X - 1, Y);
        public Location Right => new Location(X + 1, Y);
        public Location Up => new Location(X, Y - 1);
        public Location Down => new Location(X, Y + 1);
    }

}
