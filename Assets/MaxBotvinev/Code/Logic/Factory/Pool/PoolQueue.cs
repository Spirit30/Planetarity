using System.Collections.Generic;

namespace Game.Logic.Factory.Pooling
{
    class PoolQueue : Queue<PoolItem>
    {
        #region CONSTRUCTION

        public PoolQueue(PoolItem poolItem) : base(new PoolItem[] { poolItem }) { }

        #endregion

        #region GETTERS

        public bool NotEmpty => Count > 0;

        #endregion
    }
}