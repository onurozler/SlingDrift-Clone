using Game.HighwaySystem.Base;

namespace Game.HighwaySystem.HighwayTypes
{
    public class CornerHighway : HighwayBase
    {
        public override HighwayType HighwayType => HighwayType.CORNER;
        public CornerDirection CornerDirection;
        
    }
    public enum CornerDirection
    {
        RIGHT = 0,
        LEFT = 180
    }
}
