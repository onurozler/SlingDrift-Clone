using Game.HighwaySystem.Base;

namespace Game.HighwaySystem.HighwayTypes
{
    public class StraightHighway : HighwayBase
    {
        public override HighwayType HighwayType => HighwayType.STRAIGHT;
        public StraightDirection Direction;
    }

    public enum StraightDirection
    {
        LEFT,
        RIGHT,
        UP
    }
}
