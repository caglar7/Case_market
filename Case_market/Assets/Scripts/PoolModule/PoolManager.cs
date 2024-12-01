

using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class PoolManager : MonoBehaviour, IModuleInit
    {
        [Header("Pool Items")]
        public PoolObject[] poolObjects;

        [Header("Fields")]
        public PoolingPattern[] poolingPatterns;

        private List<PoolObject> allPoolObjects;

        public void Init()
        {
            allPoolObjects = new List<PoolObject>();
            allPoolObjects.AddRange(poolObjects);

            poolingPatterns = new PoolingPattern[allPoolObjects.Count];

            for (int i = 0; i < allPoolObjects.Count; i++)
            {
                PoolingPattern pattern = new PoolingPattern(allPoolObjects[i].obj);
                pattern.FillPool(allPoolObjects[i].poolCount);
                poolingPatterns[i] = pattern;
                allPoolObjects[i].poolingPattern = pattern;
            }
        }

        public bool TryGetPool(string poolName, PoolingPattern cashedPool, out PoolingPattern pool)
        {
            bool didGetPool = false;

            if(cashedPool != null)
            {
                pool = cashedPool;
                return true;
            }
            else pool = null;

            for (int i = 0; i < allPoolObjects.Count; i++)
            {
                if(poolName == allPoolObjects[i].poolName)
                {
                    pool = poolingPatterns[i];
                    didGetPool = true;
                    break;
                }
            }

            if (didGetPool == false) Debug.LogWarning("No Pool found with given name => " + poolName);

            return didGetPool;
        }

        public PoolingPattern GetPool(string poolName)
        {
            for (int i = 0; i < allPoolObjects.Count; i++)
            {
                if(poolName == allPoolObjects[i].poolName)
                {
                    return poolingPatterns[i];
                }
            }
            return null;
        }
    }
}
