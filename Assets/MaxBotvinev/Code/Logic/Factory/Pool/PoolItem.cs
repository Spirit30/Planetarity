using UnityEngine;

namespace Game.Logic.Factory.Pooling
{
    class PoolItem : MonoBehaviour
    {
        #region REFERENCES

        Pool pool;

        #endregion

        #region DATA

        public PoolItemType Type { get; private set; }
        public Vector3 SpawnPosition { get; set; }

        #endregion

        #region POOL EVENTS

        public void Init(Pool pool, PoolItemType type)
        {
            this.pool = pool;
            Type = type;
        }

        public virtual void OnGetFromPool()
        {
            Show(true);
            ParentToPool(false);
        }

        public virtual void OnPutToPool()
        {
            Show(false);
            ParentToPool(true);
        }

        #endregion

        #region INTERFACE

        public void PutToPool()
        {
            pool.Put(this);
        }

        #endregion

        #region IMPLEMENTATION

        void ParentToPool(bool flag)
        {
            transform.SetParent(flag ? pool.transform : null, true);
        }

        void Show(bool flag)
        {
            gameObject.SetActive(flag);
        }

        #endregion
    }
}