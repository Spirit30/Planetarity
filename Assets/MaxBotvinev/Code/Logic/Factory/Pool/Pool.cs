using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Factory.Pooling
{
    class Pool : MonoBehaviour
    {
        #region REFERENCES

        [SerializeField] PoolItem[] prefabs;
        Dictionary<PoolItemType, PoolQueue> instances = new Dictionary<PoolItemType, PoolQueue>();

        #endregion

        #region GETTERS

        public PoolItem Get(PoolItemType poolItemType, Vector3 point)
        {
            if (instances.ContainsKey(poolItemType))
            {
                var queue = instances[poolItemType];

                if (queue.NotEmpty)
                {
                    var poolItem = queue.Dequeue();
                    poolItem.transform.position = point;
                    poolItem.SpawnPosition = point;
                    poolItem.OnGetFromPool();
                    return poolItem;
                }
            }

            var newPoolItem = Create(poolItemType, point);
            newPoolItem.SpawnPosition = point;
            newPoolItem.OnGetFromPool();
            return newPoolItem;
        }

        public bool Contains(PoolItem poolItem)
        {
            return instances.ContainsKey(poolItem.Type) && instances[poolItem.Type].Contains(poolItem);
        }

        PoolItem Create(PoolItemType poolItemType, Vector3 point)
        {
            int index = poolItemType.ToIndex();
            var prefab = prefabs[index];
            PoolItem instance = Instantiate(prefab, point, prefab.transform.rotation);
            instance.Init(this, poolItemType);
            return instance;
        }

        #endregion

        #region SETTERS

        public void Put(PoolItem poolItem)
        {
            poolItem.OnPutToPool();

            if(instances.ContainsKey(poolItem.Type))
            {
                instances[poolItem.Type].Enqueue(poolItem);
            }
            else
            {
                instances.Add(poolItem.Type, new PoolQueue(poolItem));
            }
        }

        #endregion
    }
}