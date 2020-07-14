namespace Game.Logic.Factory.Pooling
{
    public enum PoolItemType
    {
        Planet,
        Missile,
        HitEffect
    }

    public static class PoolItemTypeExtensions
    {
        public static int ToIndex(this PoolItemType poolItemType)
        {
            return (int)poolItemType;
        }

        public static PoolItemType ToPoolItemType(this int integer)
        {
            return (PoolItemType)integer;
        }
    }
}