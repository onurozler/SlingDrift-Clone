using Game.HighwaySystem.Base;

namespace Game.HighwaySystem.HighwayTypes
{
    public class StraightHighway : HighwayBase
    {
        public StraightDirection Direction;
    }

    public enum StraightDirection
    {
        LEFT,
        RIGHT,
        UP
    }
}
