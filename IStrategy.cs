namespace Robots
{
    interface IStrategy
    {
        public Direction? SuggestDirection(Robot robot);
    }

}
