

using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Template
{
    public class PoolingPattern
    {
        private UnityEngine.GameObject prefab;
        private Stack<UnityEngine.GameObject> objPool = new Stack<UnityEngine.GameObject>();
        private MonoBehaviour _type;

        public PoolingPattern(UnityEngine.GameObject prefab)
        {
            this.prefab = prefab;
        }

        public void FillPool(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                UnityEngine.GameObject obje = Object.Instantiate(prefab);
                obje.hideFlags = HideFlags.HideInHierarchy;
                AddObjToPool(obje);
            }
        }

        public UnityEngine.GameObject PullObjFromPool(HideFlags flags = HideFlags.None)
        {
            if (objPool.Count > 0)
            {
                UnityEngine.GameObject obje = objPool.Pop();

                if(obje != null) 
                {
                    obje.gameObject.SetActive(true);
                    obje.hideFlags = flags;

                    return obje;  
                }
            }

            UnityEngine.GameObject objeIns = Object.Instantiate(prefab);
            objeIns.hideFlags = flags;
            return objeIns;
        }

        public UnityEngine.GameObject PullObjFromPool(Vector3 scale, HideFlags flags = HideFlags.None)
        {
            UnityEngine.GameObject obj = PullObjFromPool();
            obj.transform.localScale = scale;
            return obj;
        }

        public T PullObjFromPool<T>(HideFlags flags = HideFlags.None) where T : MonoBehaviour
        {
            UnityEngine.GameObject obj = PullObjFromPool(flags);
            
            if (obj.TryGetComponent(out _type))
            {
                return _type as T;
            }

            return null;
        }

        public UnityEngine.GameObject PullForDuration(float duration = 2f, HideFlags flags = HideFlags.None)
        {
            UnityEngine.GameObject obj = PullObjFromPool(flags);

            GeneralUtils.Delay(duration, () => {
                AddObjToPool(obj);
            });

            return obj;
        }

        public T PullForDuration<T>(float duration = 2f, HideFlags flags = HideFlags.None) where T : MonoBehaviour
        {
            UnityEngine.GameObject obj = PullObjFromPool(flags);

            if (obj.TryGetComponent(out _type))
            {
                GeneralUtils.Delay(duration, () => {
                    AddObjToPool(obj);
                });

                return _type as T;
            }

            return null;
        }

        public void AddObjToPool(UnityEngine.GameObject obje)
        {
            if (obje == null) return;

            if (objPool.Contains(obje))
            {
                Debug.LogWarning("Object is already in the pool");
                return;
            }

            obje.hideFlags = HideFlags.HideInHierarchy;
            obje.transform.DOKill();
            obje.transform.SetParent(null);
            obje.gameObject.SetActive(false);
            objPool.Push(obje);
        }
    }
}
