using Game.Logic.Factory.Pooling;
using UnityEngine;

namespace Game.Logic.Factory
{
    abstract class Factory : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] Pool pool;

        #endregion

        #region DATA

        [SerializeField] PoolItemType poolItemType;

        #endregion

        #region GETTERS

        protected PoolItem Get(Vector3 point)
        {
            return pool.Get(poolItemType, point);
        }

        #endregion
    }
}