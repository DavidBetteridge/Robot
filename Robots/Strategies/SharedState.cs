namespace Robots
{
    public class SharedState
    {
        public int DistanceFromLeftEdge { get; set; }
        public int Width { get; set; }

        public int VerticalDistanceFromStart { get; set; }

        public bool FirstSweep { get; set; } = true;
    }
}